using Telesharp.Common.Interfaces;
using Telesharp.Common.Types;

namespace Telesharp.Common.BotTypes
{
	/// <summary>
	///	 Default command comparer
	/// </summary>
	public class CommandComparer
	{
		/// <summary>
		///	 Invoked for compare command
		/// </summary>
		/// <param name="command">Command</param>
		/// <param name="message">Message</param>
		/// <returns></returns>
		public virtual bool Compare(ICommand command, Message message)
		{
			return message.Text != null && command.Prototype.Text == message.Text;
		}
	}
}