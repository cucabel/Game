using ICommandNS;
using IPlayNS;

namespace ICommandFactoryNS
{
    public interface ICommandFactory
    {
        public ICommand getCommand(string input, IPlay board);
    }
}
