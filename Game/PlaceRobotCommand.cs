using CoordinateNS;
using FacingNS;
using ICommandNS;
using IPlayNS;

namespace PlaceRobotCommandNS
{
    public class PlaceRobotCommand : ICommand
    {
        private IPlay board;
        private Coordinate coordinate;
        private Facing facing;
        public PlaceRobotCommand(IPlay board, Coordinate coordinate, Facing facing)
        {
            this.board = board;
            this.coordinate = coordinate;
            this.facing = facing;
        }
        public void execute()
        {
            board.placeRobot(coordinate, facing);
        }
    }
}
