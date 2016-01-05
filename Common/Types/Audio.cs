using Newtonsoft.Json;

namespace Telesharp.Common.Types
{
		/// <summary>
		///		 This object represents an audio file to be treated as music by the Telegram clients.
		/// </summary>
		public class Audio : FileBase
		{
				/// <summary>
				///		 Duration of the audio in seconds as defined by sender
				/// </summary>
				[JsonProperty(PropertyName = "duration")]
				public int Duration { get; set; }

				/// <summary>
				///		 MIME type of the file as defined by sender
				/// </summary>
				[JsonProperty(PropertyName = "mime_type")]
				public string MimeType { get; set; }
		}
}