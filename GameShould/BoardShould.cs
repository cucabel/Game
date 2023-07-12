using BoardNS;
using CoordinateNS;

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
        public void place_one_robot_on_the_board_or_move_the_existing_one()
        {
            
        }
    }
}
