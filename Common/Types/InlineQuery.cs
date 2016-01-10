using Newtonsoft.Json;

namespace Telesharp.Common.Types
{
	public class InlineQuery
	{
		/// <summary>
		/// 	Unique identifier for this query
		/// </summary>
		[JsonProperty(PropertyName = "id")]
		public string Id { get; set; }

		/// <summary>
		/// 	Sender
		/// </summary>
		[JsonProperty(PropertyName = "from")]
		public User From { get;set; }

		/// <summary>
		///		Text of the query
		/// </summary>
		[JsonProperty(PropertyName = "query")]
		public string Query { get; set; }

		/// <summary>
		/// 	Offset of the results to be returned, can be controlled by the bot
		/// </summary>
		[JsonProperty(PropertyName = "offset")]
		public string Offset { get; set; }
	}
}

