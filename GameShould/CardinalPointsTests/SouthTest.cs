using ToyRobot;

namespace ToyRobotTests
{
    public class SouthTest
    {
        private ICardinal southCardinal;

        [SetUp]
        public void Setup()
        {
            southCardinal = new South();
        }

        [Test]
        public void return_the_east_cardinal_point_when_turning_left()
        {
            ICardinal eastCardinal = southCardinal.turnLeft();

            Assert.IsInstanceOf(typeof(East), eastCardinal);
        }

        [Test]
        public void return_the_west_cardinal_point_when_turning_right()
        {
            ICardinal westCardinal = southCardinal.turnRight();

            Assert.IsInstanceOf(typeof(West), westCardinal);
        }

        [Test]
        public void return_the_south_direction()
        {
            Direction direction = southCardinal.getDirection();

            Assert.That(direction, Is.EqualTo(Direction.SOUTH));
        }
    }
}
