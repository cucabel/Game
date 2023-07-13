using BoardNS;
using CoordinateNS;
using GameNS;

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
        public void place_one_robot_on_the_board_if_there_are_no_robots_on_the_board() { }

        //Static Mocking, Elevated Feature
        [Test]
        public void move_the_existing_robot_if_there_is_already_a_robot_on_the_board() { }
    }
}
