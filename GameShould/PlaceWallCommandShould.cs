using ToyRobot;
using Telerik.JustMock;

namespace ToyRobotShould
{
    public class PlaceWallCommandShould
    {
        private IPlay mockedBoard;
        private Coordinate coordinate;
        private PlaceWallCommand command;

        [SetUp]
        public void Setup()
        {
            mockedBoard = Mock.Create<IPlay>();
            coordinate = new Coordinate(Board.MIN_WIDTH1, Board.MIN_HEIGHT1);
            command = new PlaceWallCommand(mockedBoard, coordinate);
        }

        [Test]
        public void should_pass_the_request_to_the_board()
        {
            Mock.Arrange(() => mockedBoard.placeWall(coordinate)).DoNothing();

            command.execute();

            Mock.Assert(() => mockedBoard.placeWall(coordinate), Occurs.Once());
        }
    }
}
