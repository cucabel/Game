using ToyRobot;
using Telerik.JustMock;

namespace ToyRobotShould
{
    public class CommandFactoryShould
    {
        private IValidation mockedValidation;
        private ICommandFactory commandFactory;
        private IPlay mockedBoard;

        [SetUp]
        public void Setup()
        {
            mockedValidation = Mock.Create<IValidation>();
            commandFactory = new CommandFactory(mockedValidation);
            mockedBoard = Mock.Create<IPlay>();
        }

        [Test]
        public void fabricate_a_place_robot_command_with_valid_coordinate_and_direction()
        {
            string mockedInput = StringCommand.PLACE_ROBOT.ToString() + " " + validX().ToString() + "," + validY().ToString() + "," + validDirection();
            Mock.Arrange(() => mockedValidation.validateLocation(validX(), validY(), validDirection())).Returns(true);

            ICommand command = commandFactory.getCommand(mockedInput, mockedBoard);

            Mock.Assert(() => mockedValidation.validateLocation(validX(), validY(), validDirection()), Occurs.Once());
            Assert.IsInstanceOf(typeof(PlaceRobotCommand), command);
        }

        [Test]
        public void ignore_the_place_robot_command_with_invalid_coordinate()
        {
            string mockedInput = StringCommand.PLACE_ROBOT.ToString() + " " + validX().ToString() + "," + invalidY().ToString() + "," + validDirection();
            Mock.Arrange(() => mockedValidation.validateLocation(validX(), invalidY(), validDirection())).Returns(false);

            ICommand command = commandFactory.getCommand(mockedInput, mockedBoard);

            Mock.Assert(() => mockedValidation.validateLocation(validX(), invalidY(), validDirection()), Occurs.Once());
            Assert.IsNull(command);
        }

        [Test]
        public void ignore_the_place_robot_command_with_invalid_direction()
        {
            string mockedInput = StringCommand.PLACE_ROBOT.ToString() + " " + validX().ToString() + "," + validY().ToString() + "," + invalidDirection();
            Mock.Arrange(() => mockedValidation.validateLocation(validX(), validY(), invalidDirection())).Returns(false);

            ICommand command = commandFactory.getCommand(mockedInput, mockedBoard);

            Mock.Assert(() => mockedValidation.validateLocation(validX(), validY(), invalidDirection()), Occurs.Once());
            Assert.IsNull(command);
        }

        [Test]
        public void fabricate_a_place_wall_command_with_valid_and_not_occupied_coordinate()
        {
            string mockedinput = validPlaceWallStringCommand();
            Mock.Arrange(() => mockedValidation.validateCoordinate(validX(), validY())).Returns(true);
            Coordinate coordinate = new Coordinate(validX(), validY());
            Mock.Arrange(() => mockedValidation.isOccupiedCoordinate(coordinate, mockedBoard)).Returns(false);

            ICommand command = commandFactory.getCommand(mockedinput, mockedBoard);

            Mock.Assert(() => mockedValidation.validateCoordinate(validX(), validY()), Occurs.Once());
            Mock.Assert(() => mockedValidation.isOccupiedCoordinate(coordinate, mockedBoard), Occurs.Once());
            Assert.IsInstanceOf(typeof(PlaceWallCommand), command);
        }

        [Test]
        public void ignore_place_wall_command_with_invalid_coordinate()
        {
            string mockedinput = StringCommand.PLACE_WALL.ToString() + " " + validX().ToString() + "," + invalidY().ToString();
            Mock.Arrange(() => mockedValidation.validateCoordinate(validX(), invalidY())).Returns(false);

            ICommand command = commandFactory.getCommand(mockedinput, mockedBoard);

            Mock.Assert(() => mockedValidation.validateCoordinate(validX(), invalidY()), Occurs.Once());
            Assert.IsNull(command);
        }

        [Test]
        public void ignore_the_place_wall_command_with_valid_coordinate_but_occupied_target_location()
        {
            string mockedinput = validPlaceWallStringCommand();
            Mock.Arrange(() => mockedValidation.validateCoordinate(validX(), validY())).Returns(true);
            Coordinate coordinate = new Coordinate(validX(), validY());
            Mock.Arrange(() => mockedValidation.isOccupiedCoordinate(coordinate, mockedBoard)).Returns(true);

            ICommand command = commandFactory.getCommand(mockedinput, mockedBoard);

            Mock.Assert(() => mockedValidation.validateCoordinate(validX(), validY()), Occurs.Once());
            Mock.Assert(() => mockedValidation.isOccupiedCoordinate(coordinate, mockedBoard), Occurs.Once());
            Assert.IsNull(command);
        }

        [Test]
        public void fabricate_a_report_command_when_there_is_a_robot_on_the_board()
        {
            Mock.Arrange(() => mockedValidation.isRobot()).Returns(true);

            ICommand command = commandFactory.getCommand(StringCommand.REPORT.ToString(), mockedBoard);

            Mock.Assert(() => mockedValidation.isRobot(), Occurs.Once());
            Assert.IsInstanceOf(typeof(ReportCommand), command);
        }

        [Test]
        public void ignore_the_report_command_when_there_is_not_a_robot_on_the_board()
        {
            Mock.Arrange(() => mockedValidation.isRobot()).Returns(false);

            ICommand command = commandFactory.getCommand(StringCommand.REPORT.ToString(), mockedBoard);

            Mock.Assert(() => mockedValidation.isRobot(), Occurs.Once());
            Assert.IsNull(command);
        }

        //Static Mocking, Elevated Feature
        [Test]
        public void fabricate_a_move_command_when_there_is_a_robot_on_the_board_and_there_is_not_a_wall_in_front_of_the_robot()
        {
            Robot robot = Robot.getInstance(bottomLeftCoordinate(), northCardinal());
            Coordinate nextCoordinate = new Coordinate(Board.MIN_WIDTH1, Board.MIN_HEIGHT1 + Board.UNIT_SPACE1);
            Mock.Arrange(() => mockedValidation.isRobot()).Returns(true);
            Mock.Arrange(() => mockedBoard.moveOneSpaceForward(Robot.Instance.Coordinate, Robot.Instance.Cardinal)).Returns(nextCoordinate);
            Mock.Arrange(() => mockedValidation.isOccupiedCoordinate(nextCoordinate, mockedBoard)).Returns(false);

            ICommand command = commandFactory.getCommand(StringCommand.MOVE.ToString(), mockedBoard);

            Mock.Assert(() => mockedValidation.isRobot(), Occurs.Once());
            Mock.Assert(() => mockedBoard.moveOneSpaceForward(Robot.Instance.Coordinate, Robot.Instance.Cardinal), Occurs.Once());
            Mock.Assert(() => mockedValidation.isOccupiedCoordinate(nextCoordinate, mockedBoard), Occurs.Once());
            Assert.IsInstanceOf(typeof(MoveCommand), command);
        }

        [Test]
        public void ignore_the_move_command_when_there_is_not_a_robot_on_the_board()
        {
            Mock.Arrange(() => mockedValidation.isRobot()).Returns(false);

            ICommand command = commandFactory.getCommand(StringCommand.MOVE.ToString(), mockedBoard);

            Mock.Assert(() => mockedValidation.isRobot(), Occurs.Once());
            Assert.IsNull(command);
        }

        //Static Mocking, Elevated Feature
        [Test]
        public void ignore_the_move_command_when_there_is_a_robot_on_the_board_but_there_is_a_wall_in_front_of_the_robot()
        {
            Robot robot = Robot.getInstance(bottomLeftCoordinate(), northCardinal());
            Coordinate nextCoordinate = new Coordinate(Board.MIN_WIDTH1, Board.MIN_HEIGHT1 + Board.UNIT_SPACE1);
            Mock.Arrange(() => mockedValidation.isRobot()).Returns(true);
            Mock.Arrange(() => mockedBoard.moveOneSpaceForward(Robot.Instance.Coordinate, Robot.Instance.Cardinal)).Returns(nextCoordinate);
            Mock.Arrange(() => mockedValidation.isOccupiedCoordinate(nextCoordinate, mockedBoard)).Returns(true);

            ICommand command = commandFactory.getCommand(StringCommand.MOVE.ToString(), mockedBoard);

            Mock.Assert(() => mockedValidation.isRobot(), Occurs.Once());
            Mock.Assert(() => mockedBoard.moveOneSpaceForward(Robot.Instance.Coordinate, Robot.Instance.Cardinal), Occurs.Once());
            Mock.Assert(() => mockedValidation.isOccupiedCoordinate(nextCoordinate, mockedBoard), Occurs.Once());
            Assert.IsNull(command);
        }

        [Test]
        public void fabricate_a_left_command_when_there_is_a_robot_on_the_board()
        {
            Mock.Arrange(() => mockedValidation.isRobot()).Returns(true);

            ICommand command = commandFactory.getCommand(StringCommand.LEFT.ToString(), mockedBoard);

            Mock.Assert(() => mockedValidation.isRobot(), Occurs.Once());
            Assert.IsInstanceOf(typeof(LeftCommand), command);
        }

        [Test]
        public void ignore_the_left_command_when_there_is_not_a_robot_on_the_board()
        {
            Mock.Arrange(() => mockedValidation.isRobot()).Returns(false);

            ICommand command = commandFactory.getCommand(StringCommand.LEFT.ToString(), mockedBoard);

            Mock.Assert(() => mockedValidation.isRobot(), Occurs.Once());
            Assert.IsNull(command);
        }

        [Test]
        public void fabricate_a_right_command_when_there_is_a_robot_on_the_board()
        {
            Mock.Arrange(() => mockedValidation.isRobot()).Returns(true);

            ICommand command = commandFactory.getCommand(StringCommand.RIGHT.ToString(), mockedBoard);

            Mock.Assert(() => mockedValidation.isRobot(), Occurs.Once());
            Assert.IsInstanceOf(typeof(RightCommand), command);
        }

        [Test]
        public void ignore_the_right_command_when_there_is_not_a_robot_on_the_board()
        {
            Mock.Arrange(() => mockedValidation.isRobot()).Returns(false);

            ICommand command = commandFactory.getCommand(StringCommand.RIGHT.ToString(), mockedBoard);

            Mock.Assert(() => mockedValidation.isRobot(), Occurs.Once());
            Assert.IsNull(command);
        }
        public int validX() { return Board.MIN_WIDTH1; }
        public int validY() { return Board.MIN_HEIGHT1; }
        public int invalidY() { return Board.MAX_HEIGHT1 + Board.UNIT_SPACE1; }
        public string validDirection() { return Direction.NORTH.ToString(); }
        public string invalidDirection() { return "CENTER"; }
        public Coordinate bottomLeftCoordinate() { return new Coordinate(Board.MIN_WIDTH1, Board.MIN_HEIGHT1); }
        public ICardinal northCardinal() { return new North(); }
        public string validPlaceWallStringCommand()
        {
            return StringCommand.PLACE_WALL.ToString() + " " + validX().ToString() + "," + validY().ToString();
        }
    }
}
