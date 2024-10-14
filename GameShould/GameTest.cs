using ToyRobot;
using Telerik.JustMock;

namespace ToyRobotTests
{
    public class GameTest
    {
        private IConsola mockedConsola;
        private ICommandFactory mockedCommandFactory;
        private IPlay mockedBoard;        
        private ICommand mockedPlaceRobotCommand;
        private string mockedValidString;
        private string mockedInvalidString;
        private string mockedMessage;
        IGameUI game;

       [SetUp]
        public void Setup()
        {
            mockedConsola = Mock.Create<IConsola>();
            mockedBoard = Mock.Create<IPlay>();
            mockedCommandFactory = Mock.Create<ICommandFactory>();
            mockedPlaceRobotCommand = Mock.Create<ICommand>();
            mockedValidString = Data.validPlaceRobotStringCommand();
            mockedInvalidString = StringCommand.PlaceRobot + " " + Data.validX().ToString() + "," + Data.invalidY().ToString() + "," + Data.validDirection();
            mockedMessage = "Invalid coordinate or direction";
            Mock.Arrange(() => mockedConsola.print(Arg.AnyString)).DoNothing().InSequence();
            Mock.Arrange(() => mockedConsola.readInput()).Returns(mockedValidString).InSequence();
            Mock.Arrange(() => mockedConsola.readInput()).Returns(mockedInvalidString).InSequence();
            Mock.Arrange(() => mockedConsola.readInput()).Returns("").InSequence();
            Mock.Arrange(() => mockedCommandFactory.getCommand(mockedValidString, mockedBoard, game)).Returns(mockedPlaceRobotCommand).InSequence();
            Mock.Arrange(() => mockedCommandFactory.getCommand(mockedInvalidString, mockedBoard, game)).Throws(new Exception(mockedMessage)).InSequence();

            game = new Game(mockedConsola, mockedBoard, mockedCommandFactory);
        }
        [Test]
        public void pass_the_messages_to_the_console()
        {
            Mock.Assert(() => mockedConsola.print(Arg.AnyString), Occurs.Exactly(1));
        }
        // the valid command, the invalid command, and the empty command
        [Test]
        public void get_the_console_input()
        {
            Mock.Assert(() => mockedConsola.readInput(), Occurs.Exactly(3));
        }

        [Test]
        public void set_and_execute_the_command_when_is_valid()
        {
            Mock.Assert(() => mockedCommandFactory.getCommand(mockedValidString, mockedBoard, game), Occurs.Once());
        }

        [Test]
        public void ignore_the_command_when_is_not_valid_and_pass_the_exception_message_the_consola()
        {
            Mock.Assert(() => mockedCommandFactory.getCommand(mockedInvalidString, mockedBoard, game), Occurs.Once());
        }
    }
}
