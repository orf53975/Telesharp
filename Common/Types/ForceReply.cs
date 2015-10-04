using Newtonsoft.Json;
using Telesharp.Common.Interfaces;

namespace Telesharp.Common.Types
{
    public class ForceReply : IReplyMarkup
    {
        [JsonProperty(PropertyName = "force_reply")]
        public bool Force { get; } = true;

        [JsonProperty(PropertyName = "selective")]
        public bool Selective { get; set; }
    }
}