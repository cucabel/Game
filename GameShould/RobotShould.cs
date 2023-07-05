using CoordinateNS;
using FacingNS;
using RobotNS;

namespace RobotShouldNS
{
    public class RobotShould
    {
        Coordinate coordinate;
        Facing facing;
        Robot instance;

        [SetUp]
        public void Setup()
        {
            coordinate = new Coordinate(2, 3);
            facing = Facing.NORTH;
            instance = Robot.getInstance(coordinate, facing);
        }

        [Test]
        public void place_one_robot_on_the_board_if_there_are_none()
        {
            Assert.That(instance.Coordinate, Is.EqualTo(coordinate));
            Assert.That(instance.Facing, Is.EqualTo(facing));
        }

        [Test]
        public void move_the_robot_to_the_new_location_if_there_is_already_a_robot_on_the_board()
        {
            Coordinate coordinate = new Coordinate(1, 1);
            Facing facing = Facing.SOUTH;
            Robot.getInstance(coordinate, facing);

            Assert.That(instance.Coordinate, Is.EqualTo(coordinate));
            Assert.That(instance.Facing, Is.EqualTo(facing));
        }
    }
}
