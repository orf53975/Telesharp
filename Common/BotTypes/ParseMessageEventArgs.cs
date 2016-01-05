using System;
using Telesharp.Common.Types;

namespace Telesharp.Common.BotTypes
{
	/// <summary>
	///	 Arguments for event, which invoked when command will be ready to parse (find, destr... invoke)
	/// </summary>
	public class ParseMessageEventArgs : EventArgs
	{
		/// <summary>
		///	 ParseMessageEventArgs constructor
		/// </summary>
		/// <param name="message"></param>
		public ParseMessageEventArgs(Message message)
		{
			Message = message;
		}

		/// <summary>
		///	 Message
		/// </summary>
		public Message Message { get; private set; }
	}
}