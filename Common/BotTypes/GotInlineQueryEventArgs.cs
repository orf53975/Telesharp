using System;
using Telesharp.Common.Types;

namespace Telesharp.Common.BotTypes
{
	/// <summary>
	///	 Arguments for event, which invoked when bot got inline query
	/// </summary>
	public class GotInlineQueryEventArgs : EventArgs
	{
		/// <summary>
		///	 GotInlineQueryEventArgs constructor
		/// </summary>
		/// <param name="inlineQuery"></param>
		public GotInlineQueryEventArgs(InlineQuery inlineQuery)
		{
			InlineQuery = inlineQuery;
		}

		/// <summary>
		///	 Message
		/// </summary>
		public InlineQuery InlineQuery { get; private set; }
	}
}