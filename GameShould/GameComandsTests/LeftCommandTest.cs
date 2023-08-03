using Telerik.JustMock;
using ToyRobot;

namespace ToyRobotTests
{
    public class LeftCommandTest
    {
        private IPlay mockedBoard;
        private LeftCommand command;

        [SetUp]
        public void Setup()
        {
            mockedBoard = Mock.Create<IPlay>();
            command = new LeftCommand(mockedBoard);
        }

        [Test]
        public void should_pass_the_request_to_the_board()
        {
            Mock.Arrange(() => mockedBoard.turnRobotLeft()).DoNothing();

            command.execute();

            Mock.Assert(() => mockedBoard.turnRobotLeft(), Occurs.Once());
        }
    }
}
