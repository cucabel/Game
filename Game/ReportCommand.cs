using ICommandNS;
using IPlayNS;

namespace ReportCommandNS
{
    public class ReportCommand : ICommand
    {
        private IPlay board;
        public ReportCommand(IPlay board)
        {
            this.board = board;
        }
        public void execute()
        {
            board.getRobotLocation();
        }
    }
}
