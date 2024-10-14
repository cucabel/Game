namespace ToyRobot
{
    public class LeftCommand : Command
    {
        public LeftCommand(IPlay board)
        {
            this.board = board;
        }
        public override void execute()
        {
            board.turnRobotLeft();
        }
    }
}
