namespace ToyRobot
{
    public interface ICommandFactory
    {
        public ICommand getCommand(string input, IPlay board, IGameUI game);
    }
}
