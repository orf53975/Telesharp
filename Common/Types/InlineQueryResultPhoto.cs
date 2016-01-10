using Newtonsoft.Json;

namespace Telesharp.Common.Types {

	/// <summary>
	///		This object represents one result of an inline query. This is base class
	/// </summary>
	public class InlineQueryResultPhoto : InlineQueryResult {
		/// <summary>
		/// 	Type of the result
		/// </summary>
		[JsonProperty(PropertyName = "type")]
		public new string Type { get; private set; } = "photo"; 

		/// <summary>
		/// 	Unique identifier for this result, 1-64 Bytes
		/// </summary>
		[JsonProperty(PropertyName = "id")]
		public new string Id { get; set; }

		/// <summary>
		/// 	Title of the result
		/// </summary>
		[JsonProperty(PropertyName = "title")]
		public string Title { get; set; }

		/// <summary>
		/// 	Short description of the result
		/// </summary>
		[JsonProperty(PropertyName = "description")]
		public string Description { get; set; }

		/// <summary>
		/// 	Url of the thumbnail for the result
		/// </summary>
		[JsonProperty(PropertyName = "photo_url")]
		public string PhotoUrl { get; set; }

		/// <summary>
		/// 	Thumbnail width
		/// </summary>
		[JsonProperty(PropertyName = "photo_width")]
		public int PhotoWidth { get; set; }

		/// <summary>
		/// 	Thumbnail height
		/// </summary>
		[JsonProperty(PropertyName = "photo_height")]
		public int PhotoHeight { get; set; }

		/// <summary>
		/// 	Text of the message to be sent
		/// </summary>
		[JsonProperty(PropertyName = "caption")]
		public string Caption { get; set; }

		/// <summary>
		/// 	Send “Markdown”, if you want Telegram apps to show bold, italic and inline URLs in your bot's message.
		/// </summary>
		[JsonProperty(PropertyName = "parse_mode")]
		public string ParseMode { get; set; }

	}
}