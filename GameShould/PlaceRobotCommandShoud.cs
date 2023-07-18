using ToyRobot;
using Telerik.JustMock;

namespace ToyRobotShould
{
    public class PlaceRobotCommandShoud
    {
        private IPlay mockedBoard;
        private Coordinate coordinate;
        private ICardinal cardinal;
        private PlaceRobotCommand command;

        [SetUp]
        public void Setup()
        {
            mockedBoard = Mock.Create<IPlay>();
            coordinate = new Coordinate(Board.MIN_WIDTH1, Board.MIN_HEIGHT1);
            cardinal = new North();
            command = new PlaceRobotCommand(mockedBoard, coordinate, cardinal);
        }

        [Test]
        public void should_pass_the_request_to_the_board()
        {
            Mock.Arrange(() => mockedBoard.placeRobot(coordinate, cardinal)).DoNothing();

            command.execute();

            Mock.Assert(() => mockedBoard.placeRobot(coordinate, cardinal), Occurs.Once());
        }
    }
}
