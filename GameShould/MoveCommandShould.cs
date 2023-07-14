using CoordinateNS;
using IPlayNS;
using MoveCommandNS;
using Telerik.JustMock;

namespace MoveCommandShouldNS
{
    public class MoveCommandShould
    {
        private IPlay mockedBoard;
        private Coordinate nextCoordinate;
        private MoveCommand command;

        [SetUp]
        public void Setup()
        {
            mockedBoard = Mock.Create<IPlay>();
            nextCoordinate = new Coordinate(2, 3);
            command = new MoveCommand(mockedBoard, nextCoordinate);
        }

        [Test]
        public void should_pass_the_request_to_the_board()
        {
            Mock.Arrange(() => mockedBoard.moveRobot(nextCoordinate)).DoNothing();

            command.execute();

            Mock.Assert(() => mockedBoard.moveRobot(nextCoordinate), Occurs.Once());

        }
    }
}
