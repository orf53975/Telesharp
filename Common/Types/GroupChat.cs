using Newtonsoft.Json;

namespace Telesharp.Common.Types
{
    /// <summary>
    ///     This object represents a group chat.
    /// </summary>
    public class GroupChat : Chat
    {
        /// <summary>
        ///     GroupChat constructor
        /// </summary>
        public GroupChat(string id) : this(id, null)
        {
        }

        /// <summary>
        ///     GroupChat constructor
        /// </summary>
        public GroupChat() : this("-1", null)
        {
        }

        /// <summary>
        ///     GroupChat constructor
        /// </summary>
        public GroupChat(string id, string title)
        {
            Id = id;
            Title = title;
        }

        /// <summary>
        ///     Group name
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }

        /// <summary>
        ///     Unique identifier for this group chat
        /// </summary>
        [JsonProperty(PropertyName = "id")]
        public new string Id { get; set; }
    }
}