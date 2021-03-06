using System;
using Telesharp.Common.Interfaces;
using Telesharp.Common.Types;

namespace Telesharp.Common.BotTypes
{
	/// <summary>
	///	 Command for <see cref="SimpleComparer" />
	/// </summary>
	public class SimpleComparerCommand : ICommand
	{
		public enum Mode
		{
			/// <summary>
			///	 Performs if property same with original message
			/// </summary>
			IsSame,

			/// <summary>
			///	 Performs if message for comparison contains text from original message
			/// </summary>
			ContainsTextFromOriginalMessage,

			/// <summary>
			///	 Performs if message for comparison text begins with text in original message
			/// </summary>
			BeginsWithTextFromOriginalMessage
		}

		/// <summary>
		///	 Command constructor
		/// </summary>
		public SimpleComparerCommand()
			: this(
				new Message(-1, "/on"), "Nothing information, what can help you", Mode.BeginsWithTextFromOriginalMessage
				)
		{
		}

		/// <summary>
		///	 Command consturctor
		/// </summary>
		/// <param name="prototype">Prototype</param>
		/// <param name="helpText">Helpful text</param>
		/// <param name="compareMode">Compare mode</param>
		public SimpleComparerCommand(Message prototype, string helpText = null, Mode compareMode = Mode.IsSame)
		{
			Prototype = prototype;
			CompareMode = compareMode;
			HelpText = helpText;
		}

		// /// <summary>
		// /// Original message for compare with getted message
		// /// </summary>
		// public Message OriginalMessage { get; set; }

		/// <summary>
		///	 Compare mode
		/// </summary>
		public Mode CompareMode { get; set; }

		/// <summary>
		///	 Helpful text
		/// </summary>
		public string HelpText { get; set; }

		/// <summary>
		///	 Prototype of message
		/// </summary>
		public Message Prototype { get; set; }

		/// <summary>
		///	 <see cref="ICommand.Executed" />
		/// </summary>
		/// <param name="message"></param>
		public virtual void Executed(Message message)
		{
			throw new NotImplementedException();
		}
	}
}