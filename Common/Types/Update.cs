using Newtonsoft.Json;

namespace Telesharp.Common.Types
{
    public class Update
    {
        public Update() : this(-1, null)
        {
        }

        public Update(int updateId, Message message)
        {
            Id = updateId;
            Message = message;
        }

        /// <summary>
        ///     The update‘s unique identifier. Update identifiers start from a certain positive number and increase sequentially.
        ///     This ID becomes especially handy if you’re using Webhooks, since it allows you to ignore repeated updates or to
        ///     restore the correct update sequence, should they get out of order.
        /// </summary>
        [JsonProperty(PropertyName = "update_id")]
        public int Id { get; set; }

        /// <summary>
        ///     New incoming message of any kind — text, photo, sticker, etc.
        /// </summary>
        [JsonProperty(PropertyName = "message")]
        public Message Message { get; set; }
    }
}