using ToyRobot;
using Telerik.JustMock;

namespace ToyRobotTests
{
    public class CommandFactoryTest
    {
        private IValidation mockedValidation;
        private ICommandFactory commandFactory;
        private IPlay mockedBoard;
        private IGameUI mockedGameUI;

        [SetUp]
        public void Setup()
        {
            mockedValidation = Mock.Create<IValidation>();
            commandFactory = new CommandFactory(mockedValidation);
            mockedBoard = Mock.Create<IPlay>();
            mockedGameUI = Mock.Create<IGameUI>();
        }

        [Test]
        public void fabricate_a_command_from_a_lower_cased_user_command()
        {
            Mock.Arrange(() => mockedValidation.isRobot(mockedBoard)).Returns(true);

            ICommand command = commandFactory.getCommand("report", mockedBoard, mockedGameUI);

            Mock.Assert(() => mockedValidation.isRobot(mockedBoard), Occurs.Once());
            Assert.IsInstanceOf(typeof(ReportCommand), command);
        }

        [Test]
        public void fabricate_a_place_robot_command_with_valid_and_not_occupied_coordinate_and_valid_direction()
        {
            string mockedInput = StringCommand.PlaceRobot + " " + validX().ToString() + "," + validY().ToString() + "," + validDirection();
            Coordinate coordinate = new Coordinate(validX(), validY());
            Mock.Arrange(() => mockedValidation.validateLocation(validX(), validY(), validDirection())).Returns(true);
            Mock.Arrange(() => mockedValidation.isOccupiedCoordinate(coordinate, mockedBoard)).Returns(false);

            ICommand command = commandFactory.getCommand(mockedInput, mockedBoard, mockedGameUI);

            Mock.Assert(() => mockedValidation.validateLocation(validX(), validY(), validDirection()), Occurs.Once());
            Mock.Assert(() => mockedValidation.isOccupiedCoordinate(coordinate, mockedBoard), Occurs.Once());
            Assert.IsInstanceOf(typeof(PlaceRobotCommand), command);
        }

        [Test]
        public void ignore_the_place_robot_command_with_the_wrong_number_of_parameters()
        {
            string mockedInput = StringCommand.PlaceRobot + " " + validX().ToString() + "," + invalidY().ToString();

            Exception e = Assert.Throws<Exception>(() => commandFactory.getCommand(mockedInput, mockedBoard, mockedGameUI));

            Assert.That(e.Message, Is.EqualTo("Wrong number of parameters"));
        }

        [Test]
        public void ignore_the_place_robot_command_when_the_coordinate_is_not_expressed_in_numbers()
        {
            string mockedInput = StringCommand.PlaceRobot + " " + "ONE" + "," + validY().ToString() + "," + validDirection();

            Exception e = Assert.Throws<Exception>(() => commandFactory.getCommand(mockedInput, mockedBoard, mockedGameUI));

            Assert.That(e.Message, Is.EqualTo("Row and col must be numbers"));
        }

        [Test]
        public void ignore_the_place_robot_command_with_invalid_coordinate()
        {
            string mockedInput = StringCommand.PlaceRobot + " " + validX().ToString() + "," + invalidY().ToString() + "," + validDirection();
            Mock.Arrange(() => mockedValidation.validateLocation(validX(), invalidY(), validDirection())).Returns(false);

            Exception e = Assert.Throws<Exception>(() => commandFactory.getCommand(mockedInput, mockedBoard, mockedGameUI));

            Mock.Assert(() => mockedValidation.validateLocation(validX(), invalidY(), validDirection()), Occurs.Once());
            Assert.That(e.Message, Is.EqualTo("Invalid coordinate or direction"));
        }

        [Test]
        public void ignore_the_place_robot_command_with_invalid_direction()
        {
            string mockedInput = StringCommand.PlaceRobot + " " + validX().ToString() + "," + validY().ToString() + "," + invalidDirection();
            Mock.Arrange(() => mockedValidation.validateLocation(validX(), validY(), invalidDirection())).Returns(false);

            Exception e = Assert.Throws<Exception>(() => commandFactory.getCommand(mockedInput, mockedBoard, mockedGameUI));

            Mock.Assert(() => mockedValidation.validateLocation(validX(), validY(), invalidDirection()), Occurs.Once());
            Assert.That(e.Message, Is.EqualTo("Invalid coordinate or direction"));
        }

        [Test]
        public void ignore_the_place_robot_command_with_valid_and_occupied_coordinate_and_valid_direction()
        {
            string mockedInput = StringCommand.PlaceRobot + " " + validX().ToString() + "," + validY().ToString() + "," + validDirection();
            Coordinate coordinate = new Coordinate(validX(), validY());
            Mock.Arrange(() => mockedValidation.validateLocation(validX(), validY(), validDirection())).Returns(true);
            Mock.Arrange(() => mockedValidation.isOccupiedCoordinate(coordinate, mockedBoard)).Returns(true);

            Exception e = Assert.Throws<Exception>(() => commandFactory.getCommand(mockedInput, mockedBoard, mockedGameUI));

            Mock.Assert(() => mockedValidation.validateLocation(validX(), validY(), validDirection()), Occurs.Once());
            Mock.Assert(() => mockedValidation.isOccupiedCoordinate(coordinate, mockedBoard), Occurs.Once());
            Assert.That(e.Message, Is.EqualTo("The location of the coordinate provided is occupied"));
        }

        [Test]
        public void fabricate_a_place_wall_command_with_valid_and_not_occupied_coordinate()
        {
            string mockedinput = validPlaceWallStringCommand();
            Mock.Arrange(() => mockedValidation.validateCoordinate(validX(), validY())).Returns(true);
            Coordinate coordinate = new Coordinate(validX(), validY());
            Mock.Arrange(() => mockedValidation.isOccupiedCoordinate(coordinate, mockedBoard)).Returns(false);

            ICommand command = commandFactory.getCommand(mockedinput, mockedBoard, mockedGameUI);

            Mock.Assert(() => mockedValidation.validateCoordinate(validX(), validY()), Occurs.Once());
            Mock.Assert(() => mockedValidation.isOccupiedCoordinate(coordinate, mockedBoard), Occurs.Once());
            Assert.IsInstanceOf(typeof(PlaceWallCommand), command);
        }

        [Test]
        public void ignore_the_place_wall_command_with_the_wrong_number_of_parameters()
        {
            string mockedInput = StringCommand.PlaceWall + " " + validX().ToString();

            Exception e = Assert.Throws<Exception>(() => commandFactory.getCommand(mockedInput, mockedBoard, mockedGameUI));

            Assert.That(e.Message, Is.EqualTo("Wrong number of parameters"));
        }

        [Test]
        public void ignore_the_place_wall_command_when_the_coordinate_is_not_expressed_in_numbers()
        {
            string mockedInput = StringCommand.PlaceWall + " " + "ONE" + "," + validY().ToString();

            Exception e = Assert.Throws<Exception>(() => commandFactory.getCommand(mockedInput, mockedBoard, mockedGameUI));

            Assert.That(e.Message, Is.EqualTo("Row and col must be numbers"));
        }
        [Test]
        public void ignore_place_wall_command_with_invalid_coordinate()
        {
            string mockedinput = StringCommand.PlaceWall + " " + validX().ToString() + "," + invalidY().ToString();
            Mock.Arrange(() => mockedValidation.validateCoordinate(validX(), invalidY())).Returns(false);

            Exception e = Assert.Throws<Exception>(() => commandFactory.getCommand(mockedinput, mockedBoard, mockedGameUI));

            Mock.Assert(() => mockedValidation.validateCoordinate(validX(), invalidY()), Occurs.Once());
            Assert.That(e.Message, Is.EqualTo("Invalid coordinate"));
        }

        [Test]
        public void ignore_the_place_wall_command_with_valid_coordinate_but_occupied_target_location()
        {
            string mockedinput = validPlaceWallStringCommand();
            Mock.Arrange(() => mockedValidation.validateCoordinate(validX(), validY())).Returns(true);
            Coordinate coordinate = new Coordinate(validX(), validY());
            Mock.Arrange(() => mockedValidation.isOccupiedCoordinate(coordinate, mockedBoard)).Returns(true);

            Exception e = Assert.Throws<Exception>(() => commandFactory.getCommand(mockedinput, mockedBoard, mockedGameUI));

            Mock.Assert(() => mockedValidation.validateCoordinate(validX(), validY()), Occurs.Once());
            Mock.Assert(() => mockedValidation.isOccupiedCoordinate(coordinate, mockedBoard), Occurs.Once());
            Assert.That(e.Message, Is.EqualTo("The location of the coordinate provided is occupied"));
        }

        [Test]
        public void fabricate_a_report_command_when_there_is_a_robot_on_the_board()
        {
            Mock.Arrange(() => mockedValidation.isRobot(mockedBoard)).Returns(true);

            ICommand command = commandFactory.getCommand(StringCommand.Report, mockedBoard, mockedGameUI);

            Mock.Assert(() => mockedValidation.isRobot(mockedBoard), Occurs.Once());
            Assert.IsInstanceOf(typeof(ReportCommand), command);
        }

        [Test]
        public void ignore_the_report_command_when_there_is_not_a_robot_on_the_board()
        {
            Mock.Arrange(() => mockedValidation.isRobot(mockedBoard)).Returns(false);

            Exception e = Assert.Throws<Exception>(() => commandFactory.getCommand(StringCommand.Report, mockedBoard, mockedGameUI));

            Mock.Assert(() => mockedValidation.isRobot(mockedBoard), Occurs.Once());
            Assert.That(e.Message, Is.EqualTo("There is no robot on the board"));
        }

        [Test]
        public void fabricate_a_move_command_when_there_is_a_robot_on_the_board_and_there_is_not_a_wall_in_front_of_the_robot()
        {
            Coordinate nextCoordinate = new Coordinate((int)Parameters.MinWidth, (int)Parameters.MinHeight + (int)Parameters.UnitSpace);
            Mock.Arrange(() => mockedValidation.isRobot(mockedBoard)).Returns(true);
            Mock.Arrange(() => mockedBoard.Robot).Returns(Robot.getInstance(bottomLeftCoordinate(), northCardinal()));
            Mock.Arrange(() => mockedBoard.moveOneSpaceForward(mockedBoard.Robot.Coordinate, mockedBoard.Robot.Cardinal)).Returns(nextCoordinate);
            Mock.Arrange(() => mockedValidation.isOccupiedCoordinate(nextCoordinate, mockedBoard)).Returns(false);

            ICommand command = commandFactory.getCommand(StringCommand.Move, mockedBoard, mockedGameUI);

            Mock.Assert(() => mockedValidation.isRobot(mockedBoard), Occurs.Once());
            Mock.Assert(() => mockedBoard.moveOneSpaceForward(mockedBoard.Robot.Coordinate, mockedBoard.Robot.Cardinal), Occurs.Once());
            Mock.Assert(() => mockedValidation.isOccupiedCoordinate(nextCoordinate, mockedBoard), Occurs.Once());
            Assert.IsInstanceOf(typeof(MoveCommand), command);
        }

        [Test]
        public void ignore_the_move_command_when_there_is_not_a_robot_on_the_board()
        {
            Mock.Arrange(() => mockedValidation.isRobot(mockedBoard)).Returns(false);

            Exception e = Assert.Throws<Exception>(() => commandFactory.getCommand(StringCommand.Move, mockedBoard, mockedGameUI));

            Mock.Assert(() => mockedValidation.isRobot(mockedBoard), Occurs.Once());
            Assert.That(e.Message, Is.EqualTo("There is no robot on the board"));
        }

        [Test]
        public void ignore_the_move_command_when_there_is_a_robot_on_the_board_but_there_is_a_wall_in_front_of_the_robot()
        {
            Coordinate nextCoordinate = new Coordinate((int)Parameters.MinWidth, (int)Parameters.MinHeight + (int)Parameters.UnitSpace);
            Mock.Arrange(() => mockedValidation.isRobot(mockedBoard)).Returns(true);
            Mock.Arrange(() => mockedBoard.Robot).Returns(Robot.getInstance(bottomLeftCoordinate(), northCardinal()));
            Mock.Arrange(() => mockedBoard.moveOneSpaceForward(mockedBoard.Robot.Coordinate, mockedBoard.Robot.Cardinal)).Returns(nextCoordinate);
            Mock.Arrange(() => mockedValidation.isOccupiedCoordinate(nextCoordinate, mockedBoard)).Returns(true);

            Exception e = Assert.Throws<Exception>(() => commandFactory.getCommand(StringCommand.Move, mockedBoard, mockedGameUI));

            Mock.Assert(() => mockedValidation.isRobot(mockedBoard), Occurs.Once());
            Mock.Assert(() => mockedBoard.moveOneSpaceForward(Robot.Instance.Coordinate, Robot.Instance.Cardinal), Occurs.Once());
            Mock.Assert(() => mockedValidation.isOccupiedCoordinate(nextCoordinate, mockedBoard), Occurs.Once());
            Assert.That(e.Message, Is.EqualTo("The location of the coordinate provided is occupied"));
        }

        [Test]
        public void fabricate_a_left_command_when_there_is_a_robot_on_the_board()
        {
            Mock.Arrange(() => mockedValidation.isRobot(mockedBoard)).Returns(true);

            ICommand command = commandFactory.getCommand(StringCommand.Left, mockedBoard, mockedGameUI);
            
            Mock.Assert(() => mockedValidation.isRobot(mockedBoard), Occurs.Once());
            Assert.IsInstanceOf(typeof(LeftCommand), command);
        }

        [Test]
        public void ignore_the_left_command_when_there_is_not_a_robot_on_the_board()
        {
            Mock.Arrange(() => mockedValidation.isRobot(mockedBoard)).Returns(false);

            Exception e = Assert.Throws<Exception>(() => commandFactory.getCommand(StringCommand.Left, mockedBoard, mockedGameUI));

            Mock.Assert(() => mockedValidation.isRobot(mockedBoard), Occurs.Once());
            Assert.That(e.Message, Is.EqualTo("There is no robot on the board"));
        }

        [Test]
        public void fabricate_a_right_command_when_there_is_a_robot_on_the_board()
        {
            Mock.Arrange(() => mockedValidation.isRobot(mockedBoard)).Returns(true);

            ICommand command = commandFactory.getCommand(StringCommand.Right, mockedBoard, mockedGameUI);

            Mock.Assert(() => mockedValidation.isRobot(mockedBoard), Occurs.Once());
            Assert.IsInstanceOf(typeof(RightCommand), command);
        }

        [Test]
        public void ignore_the_right_command_when_there_is_not_a_robot_on_the_board()
        {
            Mock.Arrange(() => mockedValidation.isRobot(mockedBoard)).Returns(false);

            Exception e = Assert.Throws<Exception>(() => commandFactory.getCommand(StringCommand.Right, mockedBoard, mockedGameUI));

            Mock.Assert(() => mockedValidation.isRobot(mockedBoard), Occurs.Once());
            Assert.That(e.Message, Is.EqualTo("There is no robot on the board"));
        }

        [Test]
        public void not_fabricate_a_command_when_the_user_command_is_wrong()
        {
            string mockedInput = "PLACE_CAR";

            ICommand command = commandFactory.getCommand(mockedInput, mockedBoard, mockedGameUI);

            Assert.IsNull(command);
        }
        private int validX() { return (int)Parameters.MinWidth; }
        private int validY() { return (int)Parameters.MinHeight; }
        private int invalidY() { return (int)Parameters.MaxHeight + (int)Parameters.UnitSpace; }
        private string validDirection() { return "NORTH"; }
        private string invalidDirection() { return "CENTER"; }
        private Coordinate bottomLeftCoordinate() { return new Coordinate((int)Parameters.MinWidth, (int)Parameters.MinHeight); }
        private ICardinal northCardinal() { return new North(); }
        private string validPlaceWallStringCommand()
        {
            return StringCommand.PlaceWall + " " + validX().ToString() + "," + validY().ToString();
        }
    }
}
