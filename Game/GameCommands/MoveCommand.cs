namespace ToyRobot
{
    public class MoveCommand : Command
    {
        private Coordinate nextCoordinate;
        private ICardinal cardinal;
        public MoveCommand(IPlay board, Coordinate nextCoordinate, ICardinal cardinal)
        {
            this.board = board;
            this.nextCoordinate = nextCoordinate;
            this.cardinal = cardinal;
        }
        public override void execute()
        {
            board.placeRobot(nextCoordinate, cardinal);
        }
    }
}
