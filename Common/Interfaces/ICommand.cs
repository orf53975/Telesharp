using System;
using Telesharp.Common.Types;

namespace Telesharp.Common.Interfaces
{
	/// <summary>
	///	 Interface of command for comparer (your comparer can have other properties)
	/// </summary>
	public interface ICommand
	{
		/// <summary>
		///	 Prototype of message
		/// </summary>
		/// <example>Message with text "Hello"</example>
		Message Prototype { get; set; }

		/// <summary>
		///	 Helpful (yes, helpful) text.
		/// </summary>
		[Obsolete("I want delete it")]
		string HelpText { get; set; }

		/// <summary>
		///	 If comparer believes that  message correspond to the prototype of this command. This command will be invoked (if
		///	 call will not refused).
		/// </summary>
		/// <param name="message">Message, which comparer believes similar with prototype</param>
		void Executed(Message message);
	}
}