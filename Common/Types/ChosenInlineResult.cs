using Newtonsoft.Json;

namespace Telesharp.Common.Types {

	/// <summary>
	///		This object represents a result of an inline query that was chosen by the user and sent to their chat partner.
	/// </summary>
	public class ChosenInlineResult {
		/// <summary>
		/// 	The unique identifier for the result that was chosen.
		/// </summary>
		[JsonProperty(PropertyName = "result_id")]
		public string ResultId { get; set; }

		/// <summary>
		/// 	The user that chose the result.
		/// </summary>
		[JsonProperty(PropertyName = "from")]
		public User From { get; set; }

		/// <summary>
		/// 	The query that was used to obtain the result.
		/// </summary>
		[JsonProperty(PropertyName = "query")]
		public string Query { get; set; }
	}
}