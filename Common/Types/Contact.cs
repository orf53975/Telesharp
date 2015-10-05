using Newtonsoft.Json;

namespace Telesharp.Common.Types
{
    /// <summary>
    ///     This object represents a phone contact.
    /// </summary>
    public class Contact
    {
        /// <summary>
        ///     Contact's phone number
        /// </summary>
        [JsonProperty(PropertyName = "phone_number")]
        public string PhoneNumber { get; set; }

        /// <summary>
        ///     Contact's first name
        /// </summary>
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        /// <summary>
        ///     Contact's last name
        /// </summary>
        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        /// <summary>
        ///     Contact's user identifier in Telegram
        /// </summary>
        [JsonProperty(PropertyName = "user_id")]
        public int UserId { get; set; }
    }
}