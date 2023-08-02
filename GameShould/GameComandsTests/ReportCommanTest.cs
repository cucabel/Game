using Telerik.JustMock;
using ToyRobot;

namespace ToyRobotTests
{
    public class ReportCommanTest
    {
        private IPlay mockedBoard;
        private IGameUI mockedGame;
        private ReportCommand command;

        [SetUp]
        public void Setup()
        {
            mockedBoard = Mock.Create<IPlay>();
            mockedGame = Mock.Create<IGameUI>();
            command = new ReportCommand(mockedBoard, mockedGame);
        }

        [Test]
        public void should_pass_the_request_to_the_board()
        {
            string message = "1,1,NORTH";
            Mock.Arrange(() => mockedBoard.getRobotLocation()).Returns(message);
            Mock.Arrange(() => mockedGame.setOutput(message)).DoNothing();

            command.execute();

            Mock.Assert(() => mockedBoard.getRobotLocation(), Occurs.Once());
            Mock.Assert(() => mockedGame.setOutput(message), Occurs.Once());

        }
    }
}
