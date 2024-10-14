namespace ToyRobot
{
    public class PlaceRobotCommand : Command
    {
        private Coordinate coordinate;
        private ICardinal cardinal;
        public PlaceRobotCommand(IPlay board, Coordinate coordinate, ICardinal cardinal)
        {
            this.board = board;
            this.coordinate = coordinate;
            this.cardinal = cardinal;
        }
        public override void execute()
        {
            board.placeRobot(coordinate, cardinal);
        }
    }
}
