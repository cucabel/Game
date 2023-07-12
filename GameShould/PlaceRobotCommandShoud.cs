using CoordinateNS;
using FacingNS;
using IPlayNS;
using PlaceRobotCommandNS;
using Telerik.JustMock;

namespace PlaceRobotCommandShoudNS
{
    public class PlaceRobotCommandShoud
    {
        private IPlay mockedBoard;
        private Coordinate coordinate;
        private Facing facing;
        private PlaceRobotCommand command;

        [SetUp]
        public void Setup()
        {
            mockedBoard = Mock.Create<IPlay>();
            coordinate = new Coordinate(2, 3);
            facing = Facing.NORTH;
            command = new PlaceRobotCommand(mockedBoard, coordinate, facing);
        }

        [Test]
        public void should_pass_the_request_to_the_board()
        {
            Mock.Arrange(() => mockedBoard.placeRobot(coordinate, facing)).DoNothing();

            command.execute();

            Mock.Assert(() => mockedBoard.placeRobot(coordinate, facing), Occurs.Once());

        }
    }
}
