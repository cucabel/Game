using ToyRobot;

namespace ToyRobotTests
{
    public class EastTest
    {
        private ICardinal eastCardinal;

        [SetUp]
        public void Setup()
        {
            eastCardinal = new East();
        }

        [Test]
        public void return_the_north_cardinal_point_when_turning_left()
        {
            ICardinal northCardinal = eastCardinal.turnLeft();

            Assert.IsInstanceOf(typeof(North), northCardinal);
        }

        [Test]
        public void return_the_south_cardinal_point_when_turning_right()
        {
            ICardinal southCardinal = eastCardinal.turnRight();

            Assert.IsInstanceOf(typeof(South), southCardinal);
        }

        [Test]
        public void return_the_east_direction()
        {
            Direction direction = eastCardinal.getDirection();

            Assert.That(direction, Is.EqualTo(Direction.EAST));
        }
    }
}
