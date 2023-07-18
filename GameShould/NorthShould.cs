using ToyRobot;

namespace ToyRobotShould
{
    public class NorthShould
    {
        private ICardinal northCardinal;

        [SetUp]
        public void Setup()
        {
            northCardinal = new North();
        }

        [Test]
        public void return_the_west_cardinal_point_when_turning_left()
        {
            ICardinal westCardinal = northCardinal.turnLeft();

            Assert.IsInstanceOf(typeof(West), westCardinal);
        }

        [Test]
        public void return_the_east_cardinal_point_when_turning_right()
        {
            ICardinal eastCardinal = northCardinal.turnRight();

            Assert.IsInstanceOf(typeof(East), eastCardinal);
        }

        [Test]
        public void return_the_north_direction()
        {
            Direction direction = northCardinal.getDirection();

            Assert.That(direction, Is.EqualTo(Direction.NORTH));
        }
    }
}
