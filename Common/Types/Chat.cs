using Newtonsoft.Json;

namespace Telesharp.Common.Types
{
    public class Chat
    {
        /// <summary>
        ///     Unique identifier for this user, bot or chat
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
    }
}