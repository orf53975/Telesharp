using Newtonsoft.Json;
using Telesharp.Common.Interfaces;

namespace Telesharp.Common.Types
{
    public class ReplyKeyboardHide : IReplyMarkup
    {
        [JsonProperty(PropertyName = "hide_keyboard")]
        public bool HideKeyboard { get; } = true;

        [JsonProperty(PropertyName = "selective")]
        public bool Selective { get; set; }
    }
}