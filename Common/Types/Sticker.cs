using Newtonsoft.Json;

namespace Telesharp.Common.Types
{
    public class Sticker : PhotoSize
    {
        /// <summary>
        ///     Sticker thumbnail in .webp or .jpg format
        /// </summary>
        [JsonProperty(PropertyName = "thumb")]
        public PhotoSize Thumb { get; set; }
    }
}