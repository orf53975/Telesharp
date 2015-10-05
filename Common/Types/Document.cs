using Newtonsoft.Json;

namespace Telesharp.Common.Types
{
    /// <summary>
    ///     This object represents a general file (as opposed to <see cref="PhotoSize" />, <see cref="Audio" /> messages and
    ///     audio files).
    /// </summary>
    public class Document : FileBase
    {
        /// <summary>
        ///     Document thumbnail as defined by sender.
        /// </summary>
        [JsonProperty(PropertyName = "thumb")]
        public PhotoSize Thumb { get; set; }

        /// <summary>
        ///     MIME type of the file as defined by sender
        /// </summary>
        [JsonProperty(PropertyName = "mime_type")]
        public string MimeType { get; set; }
    }
}