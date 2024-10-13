using ToyRobot;

namespace ToyRobotTests
{
    public class ConsolaTest
    {
        private Consola consola;

        [SetUp]
        public void Setup()
        {
            consola = new Consola();
        }

        [Test]
        public void read_the_user_command()
        {
            string mockedString = StringCommand.PlaceWall + " " + Data.validX().ToString() + "," + Data.validY().ToString();
            StringReader mockedInput = new StringReader(mockedString);
            Console.SetIn(mockedInput);

            string input = consola.readInput();

            Assert.That(mockedString, Is.EqualTo(input));
        }
        [Test]
        public void print_the_message_to_the_user()
        {
            string mockedString = "Introduce the commands";
            StringWriter output = new StringWriter();
            Console.SetOut(output);

            consola.print(mockedString);

            Assert.That(output.ToString(), Is.EqualTo(mockedString + "\r\n"));
        }
    }
}
