using ToyRobot;
using NUnit.Framework.Internal;

namespace RobotShouldNS
{
    public class RobotShould
    {
        private Coordinate coordinate;
        private ICardinal cardinal;
        private Robot instance;

        [SetUp]
        public void Setup()
        {
            coordinate = new Coordinate(Board.MIN_WIDTH1, Board.MIN_HEIGHT1);
            cardinal = new North();
            instance = Robot.getInstance(coordinate, cardinal);
        }

        [Test]
        public void place_one_robot_on_the_board_if_there_are_none()
        {
            Assert.That(instance.Coordinate, Is.EqualTo(coordinate));
            Assert.That(instance.Cardinal, Is.EqualTo(cardinal));
        }

        [Test]
        public void move_the_robot_to_the_new_location_if_there_is_already_a_robot_on_the_board()
        {
            Coordinate coordinate = new Coordinate(Board.MIN_WIDTH1, Board.MIN_HEIGHT1 + Board.UNIT_SPACE1);
            ICardinal cardinal = new South();
            Robot.getInstance(coordinate, cardinal);

            Assert.That(instance.Coordinate, Is.EqualTo(coordinate));
            Assert.That(instance.Cardinal, Is.EqualTo(cardinal));
        }

        [Test]
        public void turn_90_degrees_to_its_left()
        {
            ICardinal nextCardinal = new East();

            instance.turnLeft();

            Assert.IsInstanceOf(typeof(East), nextCardinal);
        }

        [Test]
        public void turn_90_degrees_to_its_right()
        {
            ICardinal nextCardinal = new West();

            instance.turnRight();

            Assert.IsInstanceOf(typeof(West), nextCardinal);
        }
    }
}
