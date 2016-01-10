using System;
using Telesharp.Common.Types;

namespace Telesharp.Common.BotTypes
{
	/// <summary>
	///	 Arguments for event, which invoked when bot got chosen by user inline result
	/// </summary>
	public class GotChosenResultEventArgs : EventArgs
	{
		/// <summary>
		///	 GotChosenInlineResultEventArgs constructor
		/// </summary>
		/// <param name="chosenResult"></param>
		public GotChosenResultEventArgs(ChosenInlineResult chosenResult)
		{
			ChosenInlineResult = chosenResult;
		}

		/// <summary>
		///	 Message
		/// </summary>
		public ChosenInlineResult ChosenInlineResult { get; private set; }
	}
}