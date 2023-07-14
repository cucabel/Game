using BoardNS;
using CoordinateNS;
using FacingNS;
using RobotNS;

namespace BoardShouldNS
{
    public class BoardShould
    {
        private Board board;

        [SetUp]
        public void Setup()
        {
            board = new Board();
        }

        [Test]
        public void be_empty_when_the_game_initialises()
        {
            List<Coordinate> boardItems = board.Items;

            Assert.IsTrue(boardItems.Count == 0);
        }

        //Static Mocking, Elevated Feature
        [Test]
        public void place_one_robot_on_the_board_if_there_are_no_robots_on_the_board() 
        {
            Coordinate coordinate = new Coordinate(2, 3);
            Facing facing = Facing.NORTH;

            board.placeRobot(coordinate, facing);

            Assert.NotNull(Robot.Instance);
            Assert.That(board.Items.Contains(coordinate));
        }

        //Static Mocking, Elevated Feature
        [Test]
        public void move_the_existing_robot_if_there_is_already_a_robot_on_the_board() 
        {
            Coordinate existingCoordinate = new Coordinate(2, 3);
            Coordinate coordinate = new Coordinate(1, 1);
            Facing facing = Facing.NORTH;
            Robot robot = Robot.getInstance(existingCoordinate, facing);

            board.placeRobot(coordinate, facing);

            Assert.That(Robot.Instance.Coordinate, Is.EqualTo(coordinate));
        }

        [Test]
        public void get_the_coordinate_of_moving_one_space_up() 
        {
            Coordinate coordinate = new Coordinate(1, 1);
            Coordinate expectedCoordinate = new Coordinate(1, 2);
            Facing facing = Facing.NORTH;

            Coordinate newCoordinate = board.moveOneSpaceForward(coordinate, facing);

            Assert.That(newCoordinate, Is.EqualTo(expectedCoordinate));
        }

        [Test]
        public void get_the_coordinate_of_moving_one_space_right()
        {
            Coordinate coordinate = new Coordinate(1, 1);
            Coordinate expectedCoordinate = new Coordinate(2, 1);
            Facing facing = Facing.EAST;

            Coordinate newCoordinate = board.moveOneSpaceForward(coordinate, facing);

            Assert.That(newCoordinate, Is.EqualTo(expectedCoordinate));
        }

        [Test]
        public void get_the_coordinate_of_moving_one_space_down()
        {
            Coordinate coordinate = new Coordinate(5, 5);
            Coordinate expectedCoordinate = new Coordinate(5, 4);
            Facing facing = Facing.SOUTH;

            Coordinate newCoordinate = board.moveOneSpaceForward(coordinate, facing);

            Assert.That(newCoordinate, Is.EqualTo(expectedCoordinate));
        }

        [Test]
        public void get_the_coordinate_of_moving_one_space_left()
        {
            Coordinate coordinate = new Coordinate(5, 5);
            Coordinate expectedCoordinate = new Coordinate(4, 5);
            Facing facing = Facing.WEST;

            Coordinate newCoordinate = board.moveOneSpaceForward(coordinate, facing);

            Assert.That(newCoordinate, Is.EqualTo(expectedCoordinate));
        }

        [Test]
        public void get_the_coordinate_of_wrapping_from_top_to_bottom_when_moving_one_space_up()
        {
            Coordinate coordinate = new Coordinate(5, 5);
            Coordinate expectedCoordinate = new Coordinate(5, 1);
            Facing facing = Facing.NORTH;

            Coordinate newCoordinate = board.moveOneSpaceForward(coordinate, facing);

            Assert.That(newCoordinate, Is.EqualTo(expectedCoordinate));
        }

        [Test]
        public void get_the_coordinate_of_wrapping_from_bottom_to_top_when_moving_one_space_down()
        {
            Coordinate coordinate = new Coordinate(1, 1);
            Coordinate expectedCoordinate = new Coordinate(1, 5);
            Facing facing = Facing.SOUTH;

            Coordinate newCoordinate = board.moveOneSpaceForward(coordinate, facing);

            Assert.That(newCoordinate, Is.EqualTo(expectedCoordinate));
        }

        [Test]
        public void get_the_coordinate_of_wrapping_from_right_to_left_when_moving_one_space_right()
        {
            Coordinate coordinate = new Coordinate(5, 5);
            Coordinate expectedCoordinate = new Coordinate(1, 5);
            Facing facing = Facing.EAST;

            Coordinate newCoordinate = board.moveOneSpaceForward(coordinate, facing);

            Assert.That(newCoordinate, Is.EqualTo(expectedCoordinate));
        }

        [Test]
        public void get_the_coordinate_of_wrapping_from_left_to_right_when_moving_one_space_left()
        {
            Coordinate coordinate = new Coordinate(1, 1);
            Coordinate expectedCoordinate = new Coordinate(5, 1);
            Facing facing = Facing.WEST;

            Coordinate newCoordinate = board.moveOneSpaceForward(coordinate, facing);

            Assert.That(newCoordinate, Is.EqualTo(expectedCoordinate));
        }
        //Static Mocking, Elevated Feature
        [Test]
        public void move_the_robot_one_space_forward_in_its_facing_direction() 
        {
            Coordinate coordinate = new Coordinate(1, 1);
            Coordinate nextCoordinate = new Coordinate(1, 2);
            Facing facing = Facing.NORTH;
            Robot robot = Robot.getInstance(coordinate, facing);
            Robot.Instance.Coordinate = nextCoordinate;

            board.moveRobot(nextCoordinate);

            Assert.That(Robot.Instance.Coordinate, Is.EqualTo(nextCoordinate));
        }
    }
}
