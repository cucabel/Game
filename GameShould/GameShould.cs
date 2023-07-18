using ToyRobot;
using Telerik.JustMock;

namespace ToyRobotShould
{
    public class GameShould
    {
        private ICommandFactory mockedCommandFactory;
        private IPlay board;
        private ICommand mockedPlaceRobotCommand;
        private Game game;

        [SetUp]
        public void Setup()
        {
            mockedCommandFactory = Mock.Create<ICommandFactory>();
            board = new Board();
            mockedPlaceRobotCommand = Mock.Create<ICommand>();
            game = new Game();
            game.Board = board;
            game.CommandFactory = mockedCommandFactory;
        }

        [Test]
        public void read_the_user_command()
        {
            string mockedString = validPlaceRobotStringCommand();
            StringReader mockedInput = new StringReader(mockedString);
            Console.SetIn(mockedInput);

            string input = game.getInput();

            Assert.That(mockedString, Is.EqualTo(input));
        }

        [Test]
        public void set_the_command_when_is_valid()
        {
            string mockedString = validPlaceRobotStringCommand();
            Coordinate coordinate = new Coordinate(validX(), validY());
            ICardinal cardinal = new North();
            ICommand placeRobotCommand = new PlaceRobotCommand(board, coordinate, cardinal);
            Mock.Arrange(() => mockedCommandFactory.getCommand(mockedString, board)).Returns(placeRobotCommand);

            game.setCommand(mockedString);

            Mock.Assert(() => mockedCommandFactory.getCommand(mockedString, board), Occurs.Once());
            Assert.IsNotNull(game.Command);
        }

        [Test]
        public void ignore_the_command_when_is_not_valid()
        {
            string mockedString = StringCommand.PLACE_ROBOT.ToString() + " " + validX().ToString() + "," + invalidY().ToString() + "," + validDirection();
            Mock.Arrange(() => mockedCommandFactory.getCommand(mockedString, board)).Returns(game.Command);

            game.setCommand(mockedString);

            Mock.Assert(() => mockedCommandFactory.getCommand(mockedString, board), Occurs.Once());
            Assert.IsNull(game.Command);
        }

        [Test]
        public void executes_the_valid_command()
        {
            Mock.Arrange(() => mockedPlaceRobotCommand.execute()).DoNothing();
            game.Command = mockedPlaceRobotCommand;

            game.executeCommand();

            Mock.Assert(() => mockedPlaceRobotCommand.execute(), Occurs.Once());
        }

        [Test]
        public void ignores_the_invalid_command()
        {
            Mock.Arrange(() => mockedPlaceRobotCommand.execute()).DoNothing();
            game.Command = null;

            game.executeCommand();

            Mock.Assert(() => mockedPlaceRobotCommand.execute(), Occurs.Never());
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
