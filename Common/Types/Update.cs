using Newtonsoft.Json;

namespace Telesharp.Common.Types
{
	/// <summary>
	///	 This object represents an incoming update.
	/// </summary>
	public class Update
	{
		/// <summary>
		///	 Update constructor
		/// </summary>
		public Update() : this(-1, null)
		{
		}

		/// <summary>
		///	 Update constructor
		/// </summary>
		/// <param name="updateId">
		///	 The update‘s unique identifier. Update identifiers start from a certain positive number and
		///	 increase sequentially. This ID becomes especially handy if you’re using Webhooks, since it allows you to ignore
		///	 repeated updates or to restore the correct update sequence, should they get out of order.
		/// </param>
		/// <param name="message">New incoming message of any kind — text, photo, sticker, etc.</param>
		public Update(long updateId, Message message)
		{
			Id = updateId;
			Message = message;
		}

		/// <summary>
		///	 The update‘s unique identifier. Update identifiers start from a certain positive number and increase sequentially.
		///	 This ID becomes especially handy if you’re using Webhooks, since it allows you to ignore repeated updates or to
		///	 restore the correct update sequence, should they get out of order.
		/// </summary>
		[JsonProperty(PropertyName = "update_id")]
		public long Id { get; set; }

		/// <summary>
		///	 New incoming message of any kind — text, photo, sticker, etc.
		/// </summary>
		[JsonProperty(PropertyName = "message")]
		public Message Message { get; set; }

		/// <summary>
		/// 	New incoming inline query.
		/// </summary>
		[JsonProperty(PropertyName = "inline_query")]
		public InlineQuery InlineQuery { get; set; }

		/// <summary>
		/// 	The result of a inline query that was chosen by a user and sent to their chat partner.
		/// </summary>
		[JsonProperty(PropertyName = "chosen_inline_result")]
		public ChosenInlineResult ChosenInlineResult { get; set; }
	}
}