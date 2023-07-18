namespace ToyRobot
{
    public class RightCommand : ICommand
    {
        IPlay board;
        public RightCommand(IPlay board)
        {
            this.board = board;
        }
        public void execute()
        {
            board.turnRobotRight();
        }
    }
}
