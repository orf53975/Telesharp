using Newtonsoft.Json;

namespace Telesharp.Common.Types {

	/// <summary>
	///		This object represents one result of an inline query. This is base class
	/// </summary>
	public class InlineQueryResultVideo : InlineQueryResult {
		/// <summary>
		/// 	Type of the result
		/// </summary>
		[JsonProperty(PropertyName = "type")]
		public new string Type { get; private set; } = "mpeg4_gif"; 

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
		[JsonProperty(PropertyName = "video_url")]
		public string VideoUrl { get; set; }

		/// <summary>
		/// 	Video width
		/// </summary>
		[JsonProperty(PropertyName = "video_width")]
		public int VideoWidth { get; set; }

		/// <summary>
		/// 	Video height
		/// </summary>
		[JsonProperty(PropertyName = "video_height")]
		public int VideoHeight { get; set; }

		/// <summary>
		/// 	Video duration in seconds
		/// </summary>
		[JsonProperty(PropertyName = "video_duration")]
		public int VideoDuration { get; set;}

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

		/// <summary>
		/// 	URL of the static thumbnail for the result (jpeg or gif)
		/// </summary>
		[JsonProperty(PropertyName = "thumb_url")]
		public string ThumbUrl { get; set; }


		/// <summary>
		/// 		Mime type of the content of video url, “text/html” or “video/mp4”
		/// </summary>
		[JsonProperty(PropertyName = "mime_type")]
		public string MimeType { get; set; }

	}
}