using Newtonsoft.Json;

namespace Telesharp.Common.Types
{
    public class File
    {
        /// <summary>
        ///     File size
        /// </summary>
        [JsonProperty(PropertyName = "file_size")]
        public int FileSize { get; set; }

        /// <summary>
        ///     Original filename as defined by sender
        /// </summary>
        [JsonProperty(PropertyName = "file_name")]
        public string FileName { get; set; }

        /// <summary>
        ///     Unique file identifier
        /// </summary>
        [JsonProperty(PropertyName = "file_id")]
        public string FileId { get; set; }
    }
}