using CoordinateNS;
using FacingNS;
using IPlayNS;
using RobotNS;
using Telerik.JustMock;
using ValidationNS;

namespace ValidationShouldNS
{
    public class ValidationShould
    {
        private Validation validation;
        private IPlay mockedBoard;

        [SetUp]
        public void Setup()
        {
            validation = new Validation();
            mockedBoard = Mock.Create<IPlay>();
        }

        [Test]
        public void validate_location()
        {
            Boolean validCommand0 = validation.validateLocation(2, 3, "NORTH");
            Boolean validCommand1 = validation.validateLocation(2, 3, "CENTER");
            Boolean validCommand2 = validation.validateLocation(2, 6, "EAST");

            Assert.IsTrue(validCommand0);
            Assert.IsFalse(validCommand1);
            Assert.IsFalse(validCommand2);
        }

        [Test]
        public void validate_coordinate()
        {
            Boolean validCoordinate0 = validation.validateCoordinate(2, 3);
            Boolean validCoordinate1 = validation.validateCoordinate(2, 6);

            Assert.IsTrue(validCoordinate0);
            Assert.IsFalse(validCoordinate1);
        }

        [Test]
        public void validate_if_the_location_is_not_occupied()
        {
            List<Coordinate> coordinates = new List<Coordinate>();
            Coordinate coordinate = new Coordinate(2, 3);
            Mock.Arrange(() => mockedBoard.Items).Returns(coordinates);

            Boolean isOccupiedLocation = validation.isOccupiedLocation(coordinate, mockedBoard);

            Assert.IsFalse(isOccupiedLocation);
        }

        [Test]
        public void validate_if_the_location_is_occupied()
        {
            List<Coordinate> coordinates = new List<Coordinate>();
            Coordinate coordinate = new Coordinate(2, 3);
            coordinates.Add(coordinate);
            Mock.Arrange(() => mockedBoard.Items).Returns(coordinates);

            Boolean isOccupiedLocation = validation.isOccupiedLocation(coordinate, mockedBoard);

            Assert.IsTrue(isOccupiedLocation);
        }

        //Static Mocking, Elevated Feature
        [Test]
        public void validate_if_there_is_a_robot_on_the_board()
        {
            Coordinate coordinate = new Coordinate(2, 3);
            Facing facing = Facing.NORTH;
            Robot robot = Robot.getInstance(coordinate, facing);

            Boolean isRobot = validation.isRobot();

            Assert.IsTrue(isRobot);
        }

        //Static Mocking, Elevated Feature
        [Test]
        public void validate_if_there_is_not_a_robot_on_the_board()
        {
            Robot.Instance = null;

            Boolean isRobot = validation.isRobot();

            Assert.IsFalse(isRobot);
        }
    }
}
