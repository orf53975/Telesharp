using System;
using Newtonsoft.Json;

namespace Telesharp.Common.Types {

	/// <summary>
	///		This object represents one result of an inline query. This is base class
	/// </summary>
	public class InlineQueryResultArticle : InlineQueryResult {
		/// <summary>
		/// 	Type of the result
		/// </summary>
		[JsonProperty(PropertyName = "type")]
		public new string Type { get; private set; } = "article"; 

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
		/// 	Text of the message to be sent
		/// </summary>
		[JsonProperty(PropertyName = "message_text")]
		public string MessageText { get; set; }

		/// <summary>
		/// 	Send “Markdown”, if you want Telegram apps to show bold, italic and inline URLs in your bot's message.
		/// </summary>
		[JsonProperty(PropertyName = "parse_mode")]
		public string ParseMode { get; set; }

		[JsonProperty(PropertyName = "url")]
		public string Url { get; set; }

		/// <summary>
		/// 	Pass <c>true</c>, if you don't want the URL to be shown in the message
		/// </summary>
		[JsonProperty(PropertyName = "hide_url")]
		public bool HideUrl { get; set; }

		/// <summary>
		/// 	Short description of the result
		/// </summary>
		[JsonProperty(PropertyName = "description")]
		public string Description { get; set; }

		/// <summary>
		/// 	Url of the thumbnail for the result
		/// </summary>
		[JsonProperty(PropertyName = "description")]
		public string ThumbUrl { get; set; }

		/// <summary>
		/// 	Thumbnail width
		/// </summary>
		[JsonProperty(PropertyName = "description")]
		public int ThumbWidth { get; set; }

		/// <summary>
		/// 	Thumbnail height
		/// </summary>
		[JsonProperty(PropertyName = "description")]
		public int ThumbHeight { get; set; }
	}
}