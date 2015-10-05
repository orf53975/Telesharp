using Newtonsoft.Json;

namespace Telesharp.Common.Types
{
    /// <summary>
    ///     This object represents a base chat class.
    /// </summary>
    public class Chat
    {
        /// <summary>
        ///     Unique identifier for this user, bot or chat.
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public int Id { get; set; }
    }
}