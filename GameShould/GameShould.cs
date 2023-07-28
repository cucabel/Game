using ToyRobot;
using Telerik.JustMock;

namespace ToyRobotShould
{
    public class GameShould
    {
        private IConsola mockedConsola;
        private ICommandFactory mockedCommandFactory;
        private IPlay board;
        private ICommand mockedPlaceRobotCommand;
        private Game game;

        [SetUp]
        public void Setup()
        {
            board = new Board();
            mockedConsola = Mock.Create<IConsola>();
            mockedCommandFactory = Mock.Create<ICommandFactory>();
            mockedPlaceRobotCommand = Mock.Create<ICommand>();
            game = new Game();
            game.Board = board;
            game.Consola = mockedConsola;
            game.CommandFactory = mockedCommandFactory;
        }

        [Test]
        public void pass_the_messages_to_the_console()
        {
            Mock.Arrange(() => mockedConsola.print(Arg.AnyString)).DoNothing();

            game.setOutput();

            Mock.Assert(() => mockedConsola.print(Arg.AnyString), Occurs.Exactly(6));
        }

        [Test]
        public void get_the_console_input()
        {
            string mockedString = validPlaceRobotStringCommand();
            Mock.Arrange(() => mockedConsola.readInput()).Returns(mockedString);

            string input = game.getInput();

            Mock.Assert(() => mockedConsola.readInput(), Occurs.Once());
            Assert.That(mockedString, Is.EqualTo(input));
        }

        [Test]
        public void set_the_command_when_is_valid()
        {
            string mockedString = validPlaceRobotStringCommand();
            Mock.Arrange(() => mockedCommandFactory.getCommand(mockedString, board)).Returns(mockedPlaceRobotCommand);

            game.setCommand(mockedString);

            Mock.Assert(() => mockedCommandFactory.getCommand(mockedString, board), Occurs.Once());
        }

        [Test]
        public void ignore_the_command_when_is_not_valid()
        {
            string mockedString = StringCommand.PLACE_ROBOT.ToString() + " " + validX().ToString() + "," + invalidY().ToString() + "," + validDirection();
            string mockedMessage = "Invalid coordinate or direction";
            Mock.Arrange(() => mockedCommandFactory.getCommand(mockedString, board)).Throws(new Exception(mockedMessage));
            Mock.Arrange(() => mockedConsola.print(mockedMessage)).DoNothing();

            game.setCommand(mockedString);

            Mock.Assert(() => mockedCommandFactory.getCommand(mockedString, board), Occurs.Once());
            Mock.Assert(() => mockedConsola.print(mockedMessage), Occurs.Once());
        }

        [Test]
        public void executes_the_valid_command()
        {
            Mock.Arrange(() => mockedPlaceRobotCommand.execute()).DoNothing();

            game.executeCommand(mockedPlaceRobotCommand);

            Mock.Assert(() => mockedPlaceRobotCommand.execute(), Occurs.Once());
        }
        public int validX() { return Board.MIN_WIDTH1; }
        public int validY() { return Board.MIN_HEIGHT1; }
        public int invalidY() { return Board.MAX_HEIGHT1 + Board.UNIT_SPACE1; }
        public string validDirection() { return Direction.NORTH.ToString(); }
        public string validPlaceRobotStringCommand()
        {
            return StringCommand.PLACE_WALL.ToString() + " " + validX().ToString() + "," + validY().ToString();
        }
    }
}
