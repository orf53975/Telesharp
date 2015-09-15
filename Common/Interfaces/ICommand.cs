using Telesharp.Common.Types;

namespace Telesharp.Common.Interfaces
{
    public interface ICommand
    {
        Message Prototype { get; set; }
        string HelpText { get; set; }
        void Executed(Message message);
    }
}