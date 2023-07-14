using CoordinateNS;
using ICommandNS;
using IPlayNS;

namespace MoveCommandNS
{
    public class MoveCommand : ICommand
    {
        private IPlay board;
        private Coordinate nextCoordinate;
        public MoveCommand(IPlay board, Coordinate nextCoordinate)
        {
            this.board = board;
            this.nextCoordinate = nextCoordinate;
        }
        public void execute()
        {
            board.moveRobot(nextCoordinate);
        }
    }
}
