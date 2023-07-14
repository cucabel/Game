using CommandFactoryNS;
using CoordinateNS;
using FacingNS;
using ICommandFactoryNS;
using ICommandNS;
using IPlayNS;
using IValidationNS;
using MoveCommandNS;
using PlaceRobotCommandNS;
using PlaceWallCommandNS;
using ReportCommandNS;
using RobotNS;
using Telerik.JustMock;

namespace CommandFactoryShouldNS
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
        public void get_the_place_robot_command_with_valid_coordinate_and_facing()
        {
            string mockedString = "PLACE_ROBOT 2,3,NORTH";
            Mock.Arrange(() => mockedValidation.validateLocation(2, 3, "NORTH")).Returns(true);

            ICommand command = commandFactory.getCommand(mockedString, mockedBoard);

            Mock.Assert(() => mockedValidation.validateLocation(2, 3, "NORTH"), Occurs.Once());
            Assert.IsInstanceOf(typeof(PlaceRobotCommand), command);
        }

        [Test]
        public void ignore_the_place_robot_command_with_invalid_coordinate()
        {
            string mockedString = "PLACE_ROBOT 2,6,NORTH";
            Mock.Arrange(() => mockedValidation.validateLocation(2, 6, "NORTH")).Returns(false);

            ICommand command = commandFactory.getCommand(mockedString, mockedBoard);

            Mock.Assert(() => mockedValidation.validateLocation(2, 6, "NORTH"), Occurs.Once());
            Assert.IsNull(command);
        }

        [Test]
        public void ignore_the_place_robot_command_with_invalid_facing()
        {
            string mockedString = "PLACE_ROBOT 2,3,CENTER";
            Mock.Arrange(() => mockedValidation.validateLocation(2, 3, "CENTER")).Returns(false);

            ICommand command = commandFactory.getCommand(mockedString, mockedBoard);

            Mock.Assert(() => mockedValidation.validateLocation(2, 3, "CENTER"), Occurs.Once());
            Assert.IsNull(command);
        }

        [Test]
        public void get_the_place_wall_command_with_valid_and_not_occupied_coordinate()
        {
            string mockedString = "PLACE_WALL 2,3";
            Mock.Arrange(() => mockedValidation.validateCoordinate(2, 3)).Returns(true);
            Coordinate coordinate = new Coordinate(2, 3);
            Mock.Arrange(() => mockedValidation.isOccupiedLocation(coordinate, mockedBoard)).Returns(false);

            ICommand command = commandFactory.getCommand(mockedString, mockedBoard);

            Mock.Assert(() => mockedValidation.validateCoordinate(2, 3), Occurs.Once());
            Mock.Assert(() => mockedValidation.isOccupiedLocation(coordinate, mockedBoard), Occurs.Once());
            Assert.IsInstanceOf(typeof(PlaceWallCommand), command);
        }

        [Test]
        public void ignore_place_wall_command_with_invalid_coordinate()
        {
            string mockedString = "PLACE_WALL 2,6";
            Mock.Arrange(() => mockedValidation.validateCoordinate(2, 6)).Returns(false);

            ICommand command = commandFactory.getCommand(mockedString, mockedBoard);

            Mock.Assert(() => mockedValidation.validateCoordinate(2, 6), Occurs.Once());
            Assert.IsNull(command);
        }

        [Test]
        public void ignore_the_place_wall_command_with_valid_coordinate_but_occupied_target_location()
        {
            string mockedString = "PLACE_WALL 2,3";
            Mock.Arrange(() => mockedValidation.validateCoordinate(2, 3)).Returns(true);
            Coordinate coordinate = new Coordinate(2, 3);
            Mock.Arrange(() => mockedValidation.isOccupiedLocation(coordinate, mockedBoard)).Returns(true);

            ICommand command = commandFactory.getCommand(mockedString, mockedBoard);

            Mock.Assert(() => mockedValidation.validateCoordinate(2, 3), Occurs.Once());
            Mock.Assert(() => mockedValidation.isOccupiedLocation(coordinate, mockedBoard), Occurs.Once());
            Assert.IsNull(command);
        }

        [Test]
        public void get_the_report_command_when_there_is_a_robot_on_the_board()
        {
            string mockedString = "REPORT";
            Mock.Arrange(() => mockedValidation.isRobot()).Returns(true);

            ICommand command = commandFactory.getCommand(mockedString, mockedBoard);

            Mock.Assert(() => mockedValidation.isRobot(), Occurs.Once());
            Assert.IsInstanceOf(typeof(ReportCommand), command);
        }

        [Test]
        public void ignore_the_report_command_when_there_is_not_a_robot_on_the_board()
        {
            string mockedString = "REPORT";
            Mock.Arrange(() => mockedValidation.isRobot()).Returns(false);

            ICommand command = commandFactory.getCommand(mockedString, mockedBoard);

            Mock.Assert(() => mockedValidation.isRobot(), Occurs.Once());
            Assert.IsNull(command);
        }

        //Static Mocking, Elevated Feature
        [Test]
        public void get_the_move_command_when_there_is_a_robot_on_the_board_and_there_is_not_a_wall_in_front_of_the_robot()
        {
            string mockedString = "MOVE";
            Coordinate coordinate = new Coordinate(1, 1);
            Facing facing = Facing.NORTH;
            Robot robot = Robot.getInstance(coordinate, facing);
            Coordinate nextCoordinate = new Coordinate(1, 2);
            Mock.Arrange(() => mockedValidation.isRobot()).Returns(true);
            Mock.Arrange(() => mockedBoard.moveOneSpaceForward(Robot.Instance.Coordinate, Robot.Instance.Facing)).Returns(nextCoordinate);
            Mock.Arrange(() => mockedValidation.isOccupiedLocation(nextCoordinate, mockedBoard)).Returns(false);

            ICommand command = commandFactory.getCommand(mockedString, mockedBoard);

            Mock.Assert(() => mockedValidation.isRobot(), Occurs.Once());
            Mock.Assert(() => mockedBoard.moveOneSpaceForward(Robot.Instance.Coordinate, Robot.Instance.Facing), Occurs.Once());
            Mock.Assert(() => mockedValidation.isOccupiedLocation(nextCoordinate, mockedBoard), Occurs.Once());
            Assert.IsInstanceOf(typeof(MoveCommand), command);
        }

        [Test]
        public void ignore_the_move_command_when_there_is_not_a_robot_on_the_board()
        {
            string mockedString = "MOVE";
            Mock.Arrange(() => mockedValidation.isRobot()).Returns(false);

            ICommand command = commandFactory.getCommand(mockedString, mockedBoard);

            Mock.Assert(() => mockedValidation.isRobot(), Occurs.Once());
            Assert.IsNull(command);
        }

        //Static Mocking, Elevated Feature
        [Test]
        public void ignore_the_move_command_when_there_is_a_robot_on_the_board_but_there_is_a_wall_in_front_of_the_robot()
        {
            string mockedString = "MOVE";
            Coordinate coordinate = new Coordinate(1, 1);
            Facing facing = Facing.NORTH;
            Robot robot = Robot.getInstance(coordinate, facing);
            Coordinate nextCoordinate = new Coordinate(1, 2);
            Mock.Arrange(() => mockedValidation.isRobot()).Returns(true);
            Mock.Arrange(() => mockedBoard.moveOneSpaceForward(Robot.Instance.Coordinate, Robot.Instance.Facing)).Returns(nextCoordinate);
            Mock.Arrange(() => mockedValidation.isOccupiedLocation(nextCoordinate, mockedBoard)).Returns(true);

            ICommand command = commandFactory.getCommand(mockedString, mockedBoard);

            Mock.Assert(() => mockedValidation.isRobot(), Occurs.Once());
            Mock.Assert(() => mockedBoard.moveOneSpaceForward(Robot.Instance.Coordinate, Robot.Instance.Facing), Occurs.Once());
            Mock.Assert(() => mockedValidation.isOccupiedLocation(nextCoordinate, mockedBoard), Occurs.Once());
            Assert.IsNull(command);
        }
    }
}
