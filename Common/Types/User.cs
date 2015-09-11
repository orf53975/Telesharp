using Newtonsoft.Json;

namespace Telesharp.Common.Types
{
    public class User : Chat
    {
        public User(int id, string firstName, string lastName) : this(id, firstName, lastName, null)
        {
        }

        public User(int id) : this(id, null, null)
        {
        }

        public User() : this(-1, null, null, null)
        {
        }

        public User(int id, string firstName, string lastName, string username)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            UserName = username;
        }

        /// <summary>
        ///     User‘s or bot’s first name
        /// </summary>
        [JsonProperty(PropertyName = "first_name")]
        public string FirstName { get; set; }

        /// <summary>
        ///     User‘s or bot’s last name
        /// </summary>
        [JsonProperty(PropertyName = "last_name")]
        public string LastName { get; set; }

        /// <summary>
        ///     User‘s or bot’s username
        /// </summary>
        [JsonProperty(PropertyName = "username")]
        public string UserName { get; set; }
    }
}