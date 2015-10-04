using Newtonsoft.Json;
using Telesharp.Common.Interfaces;

namespace Telesharp.Common.Types
{
    public class ReplyKeyboardMarkup : IReplyMarkup
    {
        [JsonProperty(PropertyName = "keyboard")]
        public string[][] Keyboard { get; set; } = {};

        [JsonProperty(PropertyName = "resize_keyboard")]
        public bool ResizeKeyboard { get; set; }

        [JsonProperty(PropertyName = "one_time_keyboard")]
        public bool OneTimeKeyboard { get; set; }

        [JsonProperty(PropertyName = "selective")]
        public bool Selective { get; set; }
    }
}