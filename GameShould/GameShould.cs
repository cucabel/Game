using Telerik.JustMock;
using GameNS;
using PlaceRobotCommandNS;
using ICommandNS;

namespace GameShould
{
    public class GameShould
    {
        private Game game;
        ICommand mockedPlaceRobotCommand;

        [SetUp]
        public void Setup()
        {
            game = new Game();
            mockedPlaceRobotCommand = Mock.Create<ICommand>();
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
        public void validate_the_user_command()
        {
            Boolean validCommand = game.validateCommand(2, 3, "NORTH");

            Assert.IsTrue(validCommand);
        }

        [TestCase(2, 3, "CENTER")]
        [TestCase(2, 6, "EAST")]
        public void invalidate_the_user_command(int row, int col, string facing)
        {
            Boolean invalidCommand = game.validateCommand(row, col, facing);

            Assert.IsFalse(invalidCommand);
        }

        [Test]
        public void set_the_valid_command()
        {
            string mockedString = "PLACE_ROBOT 2,3,NORTH";

            game.setCommand(mockedString);

            Assert.IsInstanceOf(typeof(PlaceRobotCommand), game.Command);
        }

        [TestCase("PLACE_ROBOT 2,3,CENTER")]
        [TestCase("PLACE_ROBOT 2,6,NORTH")]
        public void ignore_the_invalid_command(string mockedString)
        {
            game.setCommand(mockedString);

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
