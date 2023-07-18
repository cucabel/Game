namespace ToyRobot
{
    public class LeftCommand : ICommand
    {
        IPlay board;
        public LeftCommand(IPlay board) 
        {
            this.board = board;
        }
        public void execute()
        {
            board.turnRobotLeft();
        }
    }
}
