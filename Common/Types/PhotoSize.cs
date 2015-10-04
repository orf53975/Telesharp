using Newtonsoft.Json;

namespace Telesharp.Common.Types
{
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