namespace ToyRobot
{
    public class RightCommand : Command
    {
        public RightCommand(IPlay board)
        {
            this.board = board;
        }
        public override void execute()
        {
            board.turnRobotRight();
        }
    }
}
