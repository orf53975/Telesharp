using Newtonsoft.Json;

namespace Telesharp.Common.Types
{
    /// <summary>
    ///     This object represents a file ready to be downloaded. The file can be downloaded via the link
    ///     https://api.telegram.org/file/bot&lt;token&gt;/&lt;file_path&gt;.
    ///     It is guaranteed that the link will be valid for at least 1 hour. When the link expires, a new one
    ///     can be requested by calling getFile.
    /// </summary>
    public class File
    {
        /// <summary>
        ///     Unique identifier for this file
        /// </summary>
        [JsonProperty(PropertyName = "file_id")]
        public string FileId { get; set; }

        /// <summary>
        ///     File size, if known
        /// </summary>
        [JsonProperty(PropertyName = "file_size")]
        public int FileSize { get; set; }

        /// <summary>
        ///     File path. Use https://api.telegram.org/file/bot&lt;token&gt;/&lt;file_path&gt; to get the file.
        /// </summary>
        [JsonProperty(PropertyName = "file_path")]
        public string FilePath { get; set; }
    }
}