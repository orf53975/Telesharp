using Newtonsoft.Json;

namespace Telesharp.Common.Types
{
    /// <summary>
    ///     This object represents a video file.
    /// </summary>
    public class Video : FileBase
    {
        /// <summary>
        ///     Video width as defined by sender
        /// </summary>
        [JsonProperty(PropertyName = "height")]
        public int Height { get; set; }

        /// <summary>
        ///     Video height as defined by sender
        /// </summary>
        [JsonProperty(PropertyName = "width")]
        public int Width { get; set; }

        /// <summary>
        ///     Duration of the video in seconds as defined by sender
        /// </summary>
        [JsonProperty(PropertyName = "duration")]
        public int Duration { get; set; }

        /// <summary>
        ///     Video thumbnail
        /// </summary>
        [JsonProperty(PropertyName = "thumb")]
        public PhotoSize Thumb { get; set; }

        /// <summary>
        ///     Mime type of a file as defined by sender
        /// </summary>
        [JsonProperty(PropertyName = "mime_type")]
        public string MimeType { get; set; }
    }
}