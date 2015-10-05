using Newtonsoft.Json;

namespace Telesharp.Common.Types
{
    /// <summary>
    ///     This object represents one size of a photo or a file / sticker thumbnail.
    /// </summary>
    public class PhotoSize : FileBase
    {
        /// <summary>
        ///     Photo width
        /// </summary>
        [JsonProperty(PropertyName = "width")]
        public int Width { get; set; }

        /// <summary>
        ///     Photo width
        /// </summary>
        [JsonProperty(PropertyName = "height")]
        public int Heigth { get; set; }
    }
}