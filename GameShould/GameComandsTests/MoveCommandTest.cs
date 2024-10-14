using ToyRobot;
using Telerik.JustMock;

namespace ToyRobotTests
{
    public class MoveCommandTest
    {
        private IPlay mockedBoard;
        private Coordinate nextCoordinate;
        private ICardinal cardinal;
        private MoveCommand command;

        [SetUp]
        public void Setup()
        {
            mockedBoard = Mock.Create<IPlay>();
            nextCoordinate = new Coordinate((int)Parameters.MinWidth, (int)Parameters.MinHeight);
            cardinal = new North();
            command = new MoveCommand(mockedBoard, nextCoordinate, cardinal);
        }

        [Test]
        public void should_pass_the_request_to_the_board()
        {
            Mock.Arrange(() => mockedBoard.placeRobot(nextCoordinate, cardinal)).DoNothing();

            command.execute();

            Mock.Assert(() => mockedBoard.placeRobot(nextCoordinate, cardinal), Occurs.Once());

        }
    }
}
