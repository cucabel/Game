using CoordinateNS;
using ICommandNS;
using IPlayNS;

namespace PlaceWallCommandNS
{
    public class PlaceWallCommand : ICommand
    {
        private IPlay board;
        private Coordinate coordinate;
        public PlaceWallCommand(IPlay board, Coordinate coordinate) 
        {
            this.board = board;
            this.coordinate = coordinate;
        }
        public void execute()
        {
            board.Items.Add(coordinate);
        }
    }
}
