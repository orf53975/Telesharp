using Newtonsoft.Json;

namespace Telesharp.Common.Types
{
    /// <summary>
    ///     This object represents a Telegram user or bot.
    /// </summary>
    public class User : Chat
    {
        /// <summary>
        ///     User constructor
        /// </summary>
        /// <param name="id">Unique identifier for this user or bot</param>
        /// <param name="firstName">User‘s or bot’s first name</param>
        /// <param name="lastName">User‘s or bot’s last name</param>
        public User(int id, string firstName, string lastName) : this(id, firstName, lastName, null)
        {
        }

        /// <summary>
        ///     User constructor
        /// </summary>
        /// <param name="id">Unique identifier for this user or bot</param>
        public User(int id) : this(id, null, null)
        {
        }

        /// <summary>
        ///     User constructor
        /// </summary>
        public User() : this(-1, null, null, null)
        {
        }

        /// <summary>
        ///     User construct
        /// </summary>
        /// <param name="id">Unique identifier for this user or bot</param>
        /// <param name="firstName">User‘s or bot’s first name</param>
        /// <param name="lastName">User‘s or bot’s last name</param>
        /// <param name="username">User‘s or bot’s username</param>
        public User(int id, string firstName, string lastName, string username)
        {
            Id = $"{id}";
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