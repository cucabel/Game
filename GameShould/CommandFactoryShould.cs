using BoardNS;
using CommandFactoryNS;
using CoordinateNS;
using FacingNS;
using ICommandFactoryNS;
using ICommandNS;
using IPlayNS;
using IValidationNS;
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
        private IPlay board;

        [SetUp]
        public void Setup()
        {
            mockedValidation = Mock.Create<IValidation>();
            commandFactory = new CommandFactory(mockedValidation);
            board = new Board();
        }

        [Test]
        public void get_the_place_robot_command_with_valid_coordinate_and_facing()
        {
            string mockedString = "PLACE_ROBOT 2,3,NORTH";
            Mock.Arrange(() => mockedValidation.validateLocation(2, 3, "NORTH")).Returns(true);

            ICommand command = commandFactory.getCommand(mockedString, board);

            Mock.Assert(() => mockedValidation.validateLocation(2, 3, "NORTH"), Occurs.Once());
            Assert.IsInstanceOf(typeof(PlaceRobotCommand), command);
        }

        [Test]
        public void ignore_the_place_robot_command_with_invalid_coordinate()
        {
            string mockedString = "PLACE_ROBOT 2,6,NORTH";
            Mock.Arrange(() => mockedValidation.validateLocation(2, 6, "NORTH")).Returns(false);

            ICommand command = commandFactory.getCommand(mockedString, board);

            Mock.Assert(() => mockedValidation.validateLocation(2, 6, "NORTH"), Occurs.Once());
            Assert.IsNull(command);
        }

        [Test]
        public void ignore_the_place_robot_command_with_invalid_facing()
        {
            string mockedString = "PLACE_ROBOT 2,3,CENTER";
            Mock.Arrange(() => mockedValidation.validateLocation(2, 3, "CENTER")).Returns(false);

            ICommand command = commandFactory.getCommand(mockedString, board);

            Mock.Assert(() => mockedValidation.validateLocation(2, 3, "CENTER"), Occurs.Once());
            Assert.IsNull(command);
        }

        [Test]
        public void get_the_place_wall_command_with_valid_and_not_occupied_coordinate()
        {
            string mockedString = "PLACE_WALL 2,3";
            Mock.Arrange(() => mockedValidation.validateCoordinate(2, 3)).Returns(true);
            Coordinate coordinate = new Coordinate(2, 3);
            Mock.Arrange(() => mockedValidation.isOccupiedLocation(coordinate, board)).Returns(false);

            ICommand command = commandFactory.getCommand(mockedString, board);

            Assert.IsInstanceOf(typeof(PlaceWallCommand), command);
        }

        [Test]
        public void ignore_place_wall_command_with_invalid_coordinate()
        {
            string mockedString = "PLACE_WALL 2,6";
            Mock.Arrange(() => mockedValidation.validateCoordinate(2, 6)).Returns(false);

            ICommand command = commandFactory.getCommand(mockedString, board);

            Mock.Assert(() => mockedValidation.validateCoordinate(2, 6), Occurs.Once());
            Assert.IsNull(command);
        }

        [Test]
        public void ignore_the_place_wall_command_with_valid_coordinate_but_occupied_target_location()
        {
            string mockedString = "PLACE_WALL 2,3";
            Mock.Arrange(() => mockedValidation.validateCoordinate(2, 3)).Returns(true);
            Coordinate coordinate = new Coordinate(2, 3);
            Mock.Arrange(() => mockedValidation.isOccupiedLocation(coordinate, board)).Returns(true);

            ICommand command = commandFactory.getCommand(mockedString, board);

            Assert.IsNull(command);
        }

        [Test]
        public void get_the_report_command_when_there_is_a_robot_on_the_board()
        {
            string mockedString = "REPORT";
            Coordinate coordinate = new Coordinate(2, 3);
            Facing facing = Facing.NORTH;
            Robot robot = Robot.getInstance(coordinate, facing);

            ICommand command = commandFactory.getCommand(mockedString, board);

            Assert.IsInstanceOf(typeof(ReportCommand), command);
        }

        [Test]
        public void ignore_the_report_command_when_there_is_not_a_robot_on_the_board()
        {
            Robot.Instance = null;
            string mockedString = "REPORT";

            ICommand command = commandFactory.getCommand(mockedString, board);

            Assert.IsNull(command);
        }

    }
}
