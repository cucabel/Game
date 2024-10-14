namespace ToyRobot
{
    public class PlaceWallCommand : Command
    {
        private Coordinate coordinate;
        public PlaceWallCommand(IPlay board, Coordinate coordinate)
        {
            this.board = board;
            this.coordinate = coordinate;
        }
        public override void execute()
        {
            board.placeWall(coordinate);
        }
    }
}
