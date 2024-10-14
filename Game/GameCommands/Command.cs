namespace ToyRobot
{
    public abstract class Command : ICommand
    {
        protected IPlay board;
        protected IGameUI game;
        public abstract void execute();
    }
}
