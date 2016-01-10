using Newtonsoft.Json;

namespace Telesharp.Common.Types {

	/// <summary>
	///		This object represents one result of an inline query. This is base class
	/// </summary>
	public class InlineQueryResult {
		/// <summary>
		/// 	Type of the result
		/// </summary>
		[JsonProperty(PropertyName = "type")]
		public string Type { get; set; }

		/// <summary>
		/// 	Unique identifier for this result, 1-64 Bytes
		/// </summary>
		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }

		/// <summary>
		/// 	Disables link previews for links in the sent message
		/// </summary>
		[JsonProperty(PropertyName = "disable_web_page_preview")]
		public bool DisableWebPagePreview { get; set; } = false;
	}
}