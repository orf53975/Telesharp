using Newtonsoft.Json;

namespace Telesharp.Common.Types
{
    public class GroupChat : Chat
    {
        public GroupChat(int id) : this(id, null)
        {
        }

        public GroupChat() : this(-1, null)
        {
        }

        /// <summary>
        ///     GroupChat constructor
        /// </summary>
        public GroupChat(int id, string title)
        {
            Id = id;
            Title = title;
        }

        /// <summary>
        ///     Group name
        /// </summary>
        [JsonProperty(PropertyName = "title")]
        public string Title { get; set; }
    }
}