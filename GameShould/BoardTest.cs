using ToyRobot;
using NUnit.Framework.Internal;

namespace ToyRobotTests
{
    public class BoardTest
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
            board.placeRobot(Data.bottomLeftCoordinate(), Data.northCardinal());

            Assert.NotNull(board.Robot);
            Assert.That(board.Items.Contains(Data.bottomLeftCoordinate()));
        }
        //Static Mocking, Elevated Feature
        [Test]
        public void move_the_existing_robot_if_there_is_already_a_robot_on_the_board()
        {
            board.placeRobot(Data.topRightCoordinate(), Data.northCardinal());

            Assert.That(board.Robot.Coordinate, Is.EqualTo(Data.topRightCoordinate()));
        }

        [Test]
        public void get_the_coordinate_of_moving_one_space_up() 
        {
            Coordinate expectedCoordinate = new Coordinate((int)Parameters.MinWidth, (int)Parameters.MinHeight + (int)Parameters.UnitSpace);

            Coordinate newCoordinate = board.moveOneSpaceForward(Data.bottomLeftCoordinate(), Data.northCardinal());

            Assert.That(newCoordinate, Is.EqualTo(expectedCoordinate));
        }

        [Test]
        public void get_the_coordinate_of_moving_one_space_right()
        {
            Coordinate expectedCoordinate = new Coordinate((int)Parameters.MinWidth + (int)Parameters.UnitSpace, (int)Parameters.MinHeight);

            Coordinate newCoordinate = board.moveOneSpaceForward(Data.bottomLeftCoordinate(), Data.eastCardinal());

            Assert.That(newCoordinate, Is.EqualTo(expectedCoordinate));
        }

        [Test]
        public void get_the_coordinate_of_moving_one_space_down()
        {
            Coordinate expectedCoordinate = new Coordinate((int)Parameters.MaxWidth, (int)Parameters.MaxHeight - (int)Parameters.UnitSpace);

            Coordinate newCoordinate = board.moveOneSpaceForward(Data.topRightCoordinate(), Data.southCardinal());

            Assert.That(newCoordinate, Is.EqualTo(expectedCoordinate));
        }

        [Test]
        public void get_the_coordinate_of_moving_one_space_left()
        {
            Coordinate expectedCoordinate = new Coordinate((int)Parameters.MaxWidth - (int)Parameters.UnitSpace, (int)Parameters.MaxHeight);

            Coordinate newCoordinate = board.moveOneSpaceForward(Data.topRightCoordinate(), Data.westCardinal());

            Assert.That(newCoordinate, Is.EqualTo(expectedCoordinate));
        }

        [Test]
        public void get_the_coordinate_of_wrapping_from_top_to_bottom_when_moving_one_space_up()
        {
            Coordinate newCoordinate = board.moveOneSpaceForward(Data.topRightCoordinate(), Data.northCardinal());

            Assert.That(newCoordinate, Is.EqualTo(Data.bottomRightCoordinate()));
        }

        [Test]
        public void get_the_coordinate_of_wrapping_from_bottom_to_top_when_moving_one_space_down()
        {
            Coordinate newCoordinate = board.moveOneSpaceForward(Data.bottomLeftCoordinate(), Data.southCardinal());

            Assert.That(newCoordinate, Is.EqualTo(Data.topLeftCoordinate()));
        }

        [Test]
        public void get_the_coordinate_of_wrapping_from_right_to_left_when_moving_one_space_right()
        {
            Coordinate newCoordinate = board.moveOneSpaceForward(Data.topRightCoordinate(), Data.eastCardinal());

            Assert.That(newCoordinate, Is.EqualTo(Data.topLeftCoordinate()));
        }

        [Test]
        public void get_the_coordinate_of_wrapping_from_left_to_right_when_moving_one_space_left()
        {
            Coordinate newCoordinate = board.moveOneSpaceForward(Data.bottomLeftCoordinate(), Data.westCardinal());

            Assert.That(newCoordinate, Is.EqualTo(Data.bottomRightCoordinate()));
        }

        [Test]
        public void place_one_wall_on_the_board()
        {
            board.placeWall(Data.bottomLeftCoordinate());

            Assert.That(board.Items.Contains(Data.bottomLeftCoordinate()));
        }

        //Static Mocking, Elevated Feature
        [Test]
        public void move_the_robot_one_space_forward_in_its_direction() 
        {
            Coordinate nextCoordinate = new Coordinate((int)Parameters.MinWidth, (int)Parameters.MinHeight + (int)Parameters.UnitSpace);
            
            board.placeRobot(nextCoordinate, Data.northCardinal());

            Assert.That(board.Robot.Coordinate, Is.EqualTo(nextCoordinate));
        }
        //Static Mocking, Elevated Feature
        [Test]
        public void turn_the_robot_90_degrees_to_its_left()
        {
            board.placeRobot(Data.bottomLeftCoordinate(), Data.northCardinal());
            board.turnRobotLeft();

            Assert.IsInstanceOf(typeof(West), board.Robot.Cardinal);
        }
        //Static Mocking, Elevated Feature
        [Test]
        public void turn_the_robot_90_degrees_to_its_right()
        {
            board.placeRobot(Data.bottomLeftCoordinate(), Data.northCardinal());
            board.turnRobotRight();

            Assert.IsInstanceOf(typeof(East), board.Robot.Cardinal);
        }
    }
}
