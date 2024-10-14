using ToyRobot;

namespace ToyRobotTests
{
    public class WestTest
    {
        private ICardinal westCardinal;

        [SetUp]
        public void Setup()
        {
            westCardinal = new West();
        }

        [Test]
        public void return_the_south_cardinal_point_when_turning_left()
        {
            ICardinal southCardinal = westCardinal.turnLeft();

            Assert.IsInstanceOf(typeof(South), southCardinal);
        }

        [Test]
        public void return_the_north_cardinal_point_when_turning_right()
        {
            ICardinal northCardinal = westCardinal.turnRight();

            Assert.IsInstanceOf(typeof(North), northCardinal);
        }

        [Test]
        public void return_the_south_direction()
        {
            Direction direction = westCardinal.getDirection();

            Assert.That(direction, Is.EqualTo(Direction.WEST));
        }
    }
}
