using Newtonsoft.Json;

namespace Telesharp.Common.Types
{
    public class Document : FileBase
    {
        /// <summary>
        ///     Document thumbnail as defined by sender
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