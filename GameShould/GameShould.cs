using BoardNS;
using CoordinateNS;
using FacingNS;
using GameNS;
using ICommandFactoryNS;
using ICommandNS;
using IPlayNS;
using PlaceRobotCommandNS;
using Telerik.JustMock;

namespace GameShould
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
            string mockedString = "PLACE_ROBOT 2,3,NORTH";
            StringReader mockedInput = new StringReader(mockedString);
            Console.SetIn(mockedInput);

            string input = game.getInput();

            Assert.That(mockedString, Is.EqualTo(input));
        }

        [Test]
        public void set_the_command_when_is_valid()
        {
            string mockedString = "PLACE_ROBOT 2,3,NORTH";
            Coordinate coordinate = new Coordinate(2, 3);
            Facing facing = Facing.NORTH;
            ICommand placeRobotCommand = new PlaceRobotCommand(board, coordinate, facing);
            Mock.Arrange(() => mockedCommandFactory.getCommand(mockedString, board)).Returns(placeRobotCommand);

            game.setCommand(mockedString);

            Mock.Assert(() => mockedCommandFactory.getCommand(mockedString, board), Occurs.Once());
            Assert.IsNotNull(game.Command);
        }

        [Test]
        public void ignore_the_command_when_is_not_valid()
        {
            string mockedString = "PLACE_ROBOT 2,6,NORTH";
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
    }
}
