using ToyRobot;
using Telerik.JustMock;

namespace ToyRobotTests
{
    public class PlaceRobotCommandTest
    {
        private IPlay mockedBoard;
        private Coordinate coordinate;
        private ICardinal cardinal;
        private PlaceRobotCommand command;

        [SetUp]
        public void Setup()
        {
            mockedBoard = Mock.Create<IPlay>();
            coordinate = new Coordinate((int)Parameters.MinWidth, (int)Parameters.MinHeight);
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
