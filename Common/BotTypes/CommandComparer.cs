using Telesharp.Common.Interfaces;
using Telesharp.Common.Types;

namespace Telesharp.Common.BotTypes
{
    public class CommandComparer
    {
        public virtual bool Compare(ICommand command, Message message)
        {
            return message.Text != null && command.Prototype.Text == message.Text;
        }
    }
}