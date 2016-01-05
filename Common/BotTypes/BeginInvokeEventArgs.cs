using System;
using Telesharp.Common.Interfaces;
using Telesharp.Common.Types;

namespace Telesharp.Common.BotTypes
{
	/// <summary>
	///	 Arguments for event, which invoked when command ready to invoke
	/// </summary>
	public class BeginInvokeEventArgs : EventArgs
	{
		/// <summary>
		///	 Constuctor for class of arguments for event, which invoked when command ready to invoke (ARGH)
		/// </summary>
		/// <param name="command">Command</param>
		/// <param name="message">Message</param>
		public BeginInvokeEventArgs(ICommand command, Message message)
		{
			Command = command;
			Message = message;
			Refuse = false;
			RunSync = false;
		}

		/// <summary>
		///	 Message
		/// </summary>
		public Message Message { get; private set; }

		/// <summary>
		///	 Command
		/// </summary>
		public ICommand Command { get; set; }

		/// <summary>
		///	 Refuse the perform?
		/// </summary>
		public bool Refuse { get; set; }

		/// <summary>
		///	 Run this command in one thread?
		/// </summary>
		public bool RunSync { get; set; }
	}
}