using Telerik.JustMock;
using ToyRobot;

namespace ToyRobotTests
{
    public class RightCommandTest
    {
        private IPlay mockedBoard;
        private RightCommand command;

        [SetUp]
        public void Setup()
        {
            mockedBoard = Mock.Create<IPlay>();
            command = new RightCommand(mockedBoard);
        }

        [Test]
        public void should_pass_the_request_to_the_board()
        {
            Mock.Arrange(() => mockedBoard.turnRobotRight()).DoNothing();

            command.execute();

            Mock.Assert(() => mockedBoard.turnRobotRight(), Occurs.Once());
        }
    }
}
