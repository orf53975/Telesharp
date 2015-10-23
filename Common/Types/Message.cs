using System;
using Newtonsoft.Json;
using Telesharp.Common.JsonConverters;

namespace Telesharp.Common.Types
{
    /// <summary>
    ///     This object represents a message.
    /// </summary>
    public class Message
    {
        /// <summary>
        ///     Create simple message class
        /// </summary>
        public Message() : this(-1, null, null)
        {
        }

        /// <summary>
        ///     Message constructor
        /// </summary>
        /// <param name="id">ID of message</param>
        /// <param name="text">Text</param>
        public Message(int id, string text) : this(id, text, null)
        {
        }

        /// <summary>
        ///     Message constructor
        /// </summary>
        /// <param name="id">ID of message</param>
        /// <param name="text">Text</param>
        /// <param name="chat">Chat</param>
        public Message(int id, string text, Chat chat)
        {
            MessageId = id;
            Text = text;
            Chat = chat;
        }

        /// <summary>
        ///     Unique message identifier
        /// </summary>
        [JsonProperty(PropertyName = "message_id")]
        public int MessageId { get; set; }

        /// <summary>
        ///     Sender
        /// </summary>
        [JsonProperty(PropertyName = "from")]
        public User From { get; set; }

        /// <summary>
        ///     Date the message was sent
        /// </summary>
        [JsonConverter(typeof (UnixDateTimeConverter))]
        [JsonProperty(PropertyName = "date")]
        public DateTime Date { get; set; }

        /// <summary>
        ///     Conversation
        /// </summary>
        [JsonProperty(PropertyName = "chat")]
        public Chat Chat { get; set; }

        /// <summary>
        ///     For forwarded messages, sender of the original message
        /// </summary>
        [JsonProperty(PropertyName = "forward_from")]
        public User ForwardFrom { get; set; }

        /// <summary>
        ///     For forwarded messages, date the original message was sent
        /// </summary>
        [JsonConverter(typeof (UnixDateTimeConverter))]
        [JsonProperty(PropertyName = "forward_date")]
        public DateTime ForwardDate { get; set; }

        [JsonProperty(PropertyName = "reply_to_message")]
        public Message ReplyToMessage { get; set; }

        /// <summary>
        ///     For text messages, the actual UTF-8 text of the message
        /// </summary>
        [JsonProperty(PropertyName = "text")]
        public string Text { get; set; }

        /// <summary>
        ///     Message is an audio file, information about the file
        /// </summary>
        [JsonProperty(PropertyName = "audio")]
        public Audio Audio { get; set; }

        /// <summary>
        ///     Message is a general file, information about the file
        /// </summary>
        [JsonProperty(PropertyName = "document")]
        public Document Document { get; set; }

        /// <summary>
        ///     Message is a photo, available sizes of the photo
        /// </summary>
        [JsonProperty(PropertyName = "photo")]
        public PhotoSize[] Photo { get; set; }

        /// <summary>
        ///     Message is a sticker, information about the sticker
        /// </summary>
        [JsonProperty(PropertyName = "sticker")]
        public Sticker Sticker { get; set; }

        /// <summary>
        ///     Message is a video, information about the video
        /// </summary>
        [JsonProperty(PropertyName = "video")]
        public Video Video { get; set; }

        /// <summary>
        ///     Message is a voice message, information about the file
        /// </summary>
        [JsonProperty(PropertyName = "voice")]
        public Voice Voice { get; set; }

        /// <summary>
        ///     Caption for the photo or video
        /// </summary>
        [JsonProperty(PropertyName = "caption")]
        public string Caption { get; set; }

        /// <summary>
        ///     Message is a shared contact, information about the contact
        /// </summary>
        [JsonProperty(PropertyName = "contact")]
        public Contact Contact { get; set; }

        /// <summary>
        ///     Message is a shared location, information about the location
        /// </summary>
        [JsonProperty(PropertyName = "location")]
        public Location Location { get; set; }

        /// <summary>
        ///     A new member was added to the group, information about them (this member may be bot itself)
        /// </summary>
        [JsonProperty(PropertyName = "new_chat_participant")]
        public User NewChatParticipant { get; set; }

        /// <summary>
        ///     A member was removed from the group, information about them (this member may be bot itself)
        /// </summary>
        [JsonProperty(PropertyName = "left_chat_participant")]
        public User LeftChatParticipant { get; set; }

        /// <summary>
        ///     A group title was changed to this value
        /// </summary>
        [JsonProperty(PropertyName = "new_chat_title")]
        public string NewChatTitle { get; set; }

        /// <summary>
        ///     A group photo was change to this value
        /// </summary>
        [JsonProperty(PropertyName = "new_chat_photo")]
        public PhotoSize[] NewChatPhoto { get; set; }

        /// <summary>
        ///     Informs that the group photo was deleted
        /// </summary>
        [JsonProperty(PropertyName = "delete_chat_photo")]
        public bool DeleteChatPhoto { get; set; }

        /// <summary>
        ///     Informs that the group has been created
        /// </summary>
        [JsonProperty(PropertyName = "group_chat_created")]
        public bool GroupChatCreated { get; set; }
    }
}