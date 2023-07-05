using BoardNS;
using CoordinateNS;
using FacingNS;
using ICommandNS;
using RobotNS;

namespace PlaceRobotCommandNS
{
    public class PlaceRobotCommand : ICommand
    {
        private Board board;
        private Coordinate coordinate;
        private Facing facing;
        public PlaceRobotCommand(Board board, Coordinate coordinate, Facing facing)
        {
            this.board = board;
            this.coordinate = coordinate;
            this.facing = facing;
        }
        public void execute()
        {
            if (board.Robot != null)
            {
                Coordinate coordinate = board.Robot.Coordinate;
                board.Items.RemoveAll(c => c == coordinate);
            }
            board.Robot = Robot.getInstance(coordinate, facing);
            board.Items.Add(coordinate);
        }
    }
}
