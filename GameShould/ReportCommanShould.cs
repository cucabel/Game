using ToyRobot;
using Telerik.JustMock;

namespace ToyRobotShould
{
    public class ReportCommanShould
    {
        private IPlay mockedBoard;
        private ReportCommand command;

        [SetUp]
        public void Setup()
        {
            mockedBoard = Mock.Create<IPlay>();
            command = new ReportCommand(mockedBoard);
        }

        [Test]
        public void should_pass_the_request_to_the_board()
        {
            Mock.Arrange(() => mockedBoard.getRobotLocation()).DoNothing();

            command.execute();

            Mock.Assert(() => mockedBoard.getRobotLocation(), Occurs.Once());

        }
    }
}
