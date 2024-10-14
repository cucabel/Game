namespace ToyRobot
{
    public class ReportCommand : Command
    {
        public ReportCommand(IPlay board, IGameUI game)
        {
            this.board = board;
            this.game = game;
        }
        public override void execute()
        {
            string message = board.getRobotLocation();
            game.setOutput(message);
        }
    }
}
