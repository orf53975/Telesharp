using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Telesharp.Common.BotTypes;
using Telesharp.Common.Interfaces;
using Telesharp.Common.TelesharpTypes;
using Telesharp.Common.Types;
using File = Telesharp.Common.Types.File;

namespace Telesharp.Common
{
    public class TelegramBotMethods
    {
        private int _updateOffset;

        public TelegramBotMethods(Bot bot)
        {
            Bot = bot;
        }

        private Bot Bot { get; }

        public string BuildUriForMethod(string method)
        {
            return $"https://api.telegram.org/bot{Bot.Settings.Token}/{method}";
        }

        public bool CheckConnection()
        {
            return SendTRequest(BuildUriForMethod("getMe"));
        }

        public User GetMe()
        {
            return SendTRequest<User>(BuildUriForMethod("getMe"));
        }

        public Message ForwardMessage(Message message, Chat fromChat, Chat toChat)
        {
            return SendTRequest<Message>(BuildUriForMethod("forwardMessage"), new Dictionary<string, string>
            {
                {"chat_id", $"{toChat.Id}"},
                {"from_chat_id", $"{fromChat.Id}"},
                {"message_id", $"{message.MessageId}"}
            });
        }

        #region Get Updates

        public Update[] GetUpdates()
        {
            try
            {
                var updates = SendTRequest<Update[]>(BuildUriForMethod("getUpdates"), new Dictionary<string, string>
                {
                    {"offset", _updateOffset + ""}
                });
                if (updates == null || updates.Length == 0)
                {
                    return null;
                }
                _updateOffset = updates[updates.Length - 1].Id + 1;
                return updates;
            }
            catch
            {
                return null;
            }
        }

        public Update[] GetUpdates(int offset, int limit = 100, int timeout = 0)
        {
            return SendTRequest<Update[]>(BuildUriForMethod("getUpdates"), new Dictionary<string, string>
            {
                {"offset", offset + ""},
                {"limit", limit + ""},
                {"timeout", timeout + ""}
            });
        }

        #endregion

        #region Send Text Message

        public Message SendMessage(int chatId, string text, bool disableWebPagePreview = false)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendMessage"), new Dictionary<string, string>
            {
                {"chat_id", $"{chatId}"},
                {"text", text},
                {"disable_web_page_preview", $"{disableWebPagePreview}"}
            });
        }

        public Message SendMessage(int chatId, string text, IReplyMarkup replyMarkup, bool disableWebPagePreview = false)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendMessage"), new Dictionary<string, string>
            {
                {"chat_id", $"{chatId}"},
                {"text", text},
                {"disable_web_page_preview", $"{disableWebPagePreview}"},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            });
        }

        public Message SendMessage(Chat chat, string text, bool disableWebPagePreview = false)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendMessage"), new Dictionary<string, string>
            {
                {"chat_id", $"{chat.Id}"},
                {"text", text},
                {"disable_web_page_preview", $"{disableWebPagePreview}"}
            });
        }

        public Message SendMessage(Chat chat, string text, IReplyMarkup replyMarkup, bool disableWebPagePreview = false)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendMessage"), new Dictionary<string, string>
            {
                {"chat_id", $"{chat.Id}"},
                {"text", text},
                {"disable_web_page_preview", $"{disableWebPagePreview}"},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            });
        }

        #endregion Send Text Message

        #region Reply

        public Message ReplyToMessage(Message message, string text)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendMessage"), new Dictionary<string, string>
            {
                {"text", text},
                {"reply_to_message_id", $"{message.MessageId}"},
                {"chat_id", $"{message.Chat.Id}"}
            });
        }

        public Message ReplyToMessage(Message message, string text, IReplyMarkup replyMarkup)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendMessage"), new Dictionary<string, string>
            {
                {"text", text},
                {"reply_to_message_id", $"{message.MessageId}"},
                {"chat_id", $"{message.Chat.Id}"},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            });
        }

        public Message ReplyToMessage(Message message, PhotoSize photo)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendPhoto"), new Dictionary<string, string>
            {
                {"photo", photo.FileId},
                {"reply_to_message_id", $"{message.MessageId}"},
                {"chat_id", $"{message.Chat.Id}"}
            });
        }

        public Message ReplyToMessage(Message message, PhotoSize photo, IReplyMarkup replyMarkup)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendPhoto"), new Dictionary<string, string>
            {
                {"photo", photo.FileId},
                {"reply_to_message_id", $"{message.MessageId}"},
                {"chat_id", $"{message.Chat.Id}"},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            });
        }

        public Message ReplyToMessage(Message message, Audio audio)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendAudio"), new Dictionary<string, string>
            {
                {"audio", audio.FileId},
                {"reply_to_message_id", $"{message.MessageId}"},
                {"chat_id", $"{message.Chat.Id}"}
            });
        }

        public Message ReplyToMessage(Message message, Audio audio, IReplyMarkup replyMarkup)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendAudio"), new Dictionary<string, string>
            {
                {"audio", audio.FileId},
                {"reply_to_message_id", $"{message.MessageId}"},
                {"chat_id", $"{message.Chat.Id}"},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            });
        }

        public Message ReplyToMessage(Message message, Document document)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendDocument"), new Dictionary<string, string>
            {
                {"document", document.FileId},
                {"reply_to_message_id", $"{message.MessageId}"},
                {"chat_id", $"{message.Chat.Id}"}
            });
        }

        public Message ReplyToMessage(Message message, Document document, IReplyMarkup replyMarkup)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendDocument"), new Dictionary<string, string>
            {
                {"document", document.FileId},
                {"reply_to_message_id", $"{message.MessageId}"},
                {"chat_id", $"{message.Chat.Id}"},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            });
        }

        public Message ReplyToMessage(Message message, Sticker sticker)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendSticker"), new Dictionary<string, string>
            {
                {"sticker", sticker.FileId},
                {"reply_to_message_id", $"{message.MessageId}"},
                {"chat_id", $"{message.Chat.Id}"}
            });
        }

        public Message ReplyToMessage(Message message, Sticker sticker, IReplyMarkup replyMarkup)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendSticker"), new Dictionary<string, string>
            {
                {"sticker", sticker.FileId},
                {"reply_to_message_id", $"{message.MessageId}"},
                {"chat_id", $"{message.Chat.Id}"},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            });
        }

        public Message ReplyToMessage(Message message, Video video)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendVideo"), new Dictionary<string, string>
            {
                {"video", video.FileId},
                {"reply_to_message_id", $"{message.MessageId}"},
                {"chat_id", $"{message.Chat.Id}"}
            });
        }

        public Message ReplyToMessage(Message message, Video video, IReplyMarkup replyMarkup)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendVideo"), new Dictionary<string, string>
            {
                {"video", video.FileId},
                {"reply_to_message_id", $"{message.MessageId}"},
                {"chat_id", $"{message.Chat.Id}"},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            });
        }

        public Message ReplyToMessage(Message message, Location location)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendLocation"), new Dictionary<string, string>
            {
                {"latitude", $"{location.Latitude}"},
                {"longitude", $"{location.Longitude}"},
                {"reply_to_message_id", $"{message.MessageId}"},
                {"chat_id", $"{message.Chat.Id}"}
            });
        }

        public Message ReplyToMessage(Message message, Location location, IReplyMarkup replyMarkup)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendLocation"), new Dictionary<string, string>
            {
                {"latitude", $"{location.Latitude}"},
                {"longitude", $"{location.Longitude}"},
                {"reply_to_message_id", $"{message.MessageId}"},
                {"chat_id", $"{message.Chat.Id}"},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            });
        }

        #endregion

        #region Send Audio

        public Message SendAudio(int chatId, Audio audio)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendAudio"), new Dictionary<string, string>
            {
                {"chat_id", $"{chatId}"},
                {"audio", audio.FileId}
            });
        }

        public Message SendAudio(int chatId, Audio audio, IReplyMarkup replyMarkup)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendAudio"), new Dictionary<string, string>
            {
                {"chat_id", $"{chatId}"},
                {"audio", audio.FileId},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            });
        }

        public Message SendAudio(Chat chat, Audio audio)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendAudio"), new Dictionary<string, string>
            {
                {"chat_id", $"{chat.Id}"},
                {"audio", audio.FileId}
            });
        }

        public Message SendAudio(Chat chat, Audio audio, IReplyMarkup replyMarkup)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendAudio"), new Dictionary<string, string>
            {
                {"chat_id", $"{chat.Id}"},
                {"audio", audio.FileId},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            });
        }

        public Message SendAudio(int chatId, string audioId)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendAudio"), new Dictionary<string, string>
            {
                {"chat_id", $"{chatId}"},
                {"audio", audioId}
            });
        }

        public Message SendAudio(int chatId, string audioId, IReplyMarkup replyMarkup)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendAudio"), new Dictionary<string, string>
            {
                {"chat_id", $"{chatId}"},
                {"audio", audioId},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            });
        }

        public Message SendAudio(Chat chat, string audioId)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendAudio"), new Dictionary<string, string>
            {
                {"chat_id", $"{chat.Id}"},
                {"audio", audioId}
            });
        }

        public Message SendAudio(Chat chat, string audioId, IReplyMarkup replyMarkup)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendAudio"), new Dictionary<string, string>
            {
                {"chat_id", $"{chat.Id}"},
                {"audio", audioId},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            });
        }

        public Message SendAudioFile(int chatId, string fileName)
        {
            if (!System.IO.File.Exists(fileName)) throw new FileNotFoundException();

            return TeFileUpload<Message>(BuildUriForMethod("sendAudio"), fileName, Path.GetFileName(fileName), "audio",
                new Dictionary<string, string>
                {
                    {"chat_id", $"{chatId}"}
                });
        }

        public Message SendAudioFile(int chatId, string fileName, IReplyMarkup replyMarkup)
        {
            if (!System.IO.File.Exists(fileName)) throw new FileNotFoundException();

            return TeFileUpload<Message>(BuildUriForMethod("sendAudio"), fileName, Path.GetFileName(fileName), "audio",
                new Dictionary<string, string>
                {
                    {"chat_id", $"{chatId}"},
                    {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
                });
        }

        public Message SendAudioFile(Chat chat, string fileName)
        {
            if (!System.IO.File.Exists(fileName)) throw new FileNotFoundException();
            return TeFileUpload<Message>(BuildUriForMethod("sendAudio"), fileName, Path.GetFileName(fileName), "audio",
                new Dictionary<string, string>
                {
                    {"chat_id", $"{chat.Id}"}
                });
        }

        public Message SendAudioFile(Chat chat, string fileName, IReplyMarkup replyMarkup)
        {
            if (!System.IO.File.Exists(fileName)) throw new FileNotFoundException();
            return TeFileUpload<Message>(BuildUriForMethod("sendAudio"), fileName, Path.GetFileName(fileName), "audio",
                new Dictionary<string, string>
                {
                    {"chat_id", $"{chat.Id}"},
                    {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
                });
        }

        public Message SendAudioFile(int chatId, byte[] bytes, string tFileName)
        {
            return TeFileUpload<Message>(BuildUriForMethod("sendAudio"), bytes, tFileName, "audio",
                new Dictionary<string, string>
                {
                    {"chat_id", $"{chatId}"}
                });
        }

        public Message SendAudioFile(int chatId, byte[] bytes, string tFileName, IReplyMarkup replyMarkup)
        {
            return TeFileUpload<Message>(BuildUriForMethod("sendAudio"), bytes, tFileName, "audio",
                new Dictionary<string, string>
                {
                    {"chat_id", $"{chatId}"},
                    {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
                });
        }

        public Message SendAudioFile(Chat chat, byte[] bytes, string tFileName)
        {
            return TeFileUpload<Message>(BuildUriForMethod("sendAudio"), bytes, tFileName, "audio",
                new Dictionary<string, string>
                {
                    {"chat_id", $"{chat.Id}"}
                });
        }

        public Message SendAudioFile(Chat chat, byte[] bytes, string tFileName, IReplyMarkup replyMarkup)
        {
            return TeFileUpload<Message>(BuildUriForMethod("sendAudio"), bytes, tFileName, "audio",
                new Dictionary<string, string>
                {
                    {"chat_id", $"{chat.Id}"},
                    {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
                });
        }

        #endregion

        #region Send Sticker

        public Message SendSticker(int chatId, Sticker sticker)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendSticker"), new Dictionary<string, string>
            {
                {"chat_id", $"{chatId}"},
                {"sticker", sticker.FileId}
            });
        }

        public Message SendSticker(int chatId, Sticker sticker, IReplyMarkup replyMarkup)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendSticker"), new Dictionary<string, string>
            {
                {"chat_id", $"{chatId}"},
                {"sticker", sticker.FileId},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            });
        }

        public Message SendSticker(Chat chat, Sticker sticker)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendSticker"), new Dictionary<string, string>
            {
                {"chat_id", $"{chat.Id}"},
                {"sticker", sticker.FileId}
            });
        }

        public Message SendSticker(Chat chat, Sticker sticker, IReplyMarkup replyMarkup)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendSticker"), new Dictionary<string, string>
            {
                {"chat_id", $"{chat.Id}"},
                {"sticker", sticker.FileId},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            });
        }

        public Message SendSticker(int chatId, string stickerId)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendSticker"), new Dictionary<string, string>
            {
                {"chat_id", $"{chatId}"},
                {"sticker", stickerId}
            });
        }

        public Message SendSticker(int chatId, string stickerId, IReplyMarkup replyMarkup)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendSticker"), new Dictionary<string, string>
            {
                {"chat_id", $"{chatId}"},
                {"sticker", stickerId},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            });
        }

        public Message SendSticker(Chat chat, string stickerId)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendSticker"), new Dictionary<string, string>
            {
                {"chat_id", $"{chat.Id}"},
                {"sticker", stickerId}
            });
        }

        public Message SendSticker(Chat chat, string stickerId, IReplyMarkup replyMarkup)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendSticker"), new Dictionary<string, string>
            {
                {"chat_id", $"{chat.Id}"},
                {"sticker", stickerId},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            });
        }

        #endregion Send Sticker

        #region Send Video

        public Message SendVideo(int chatId, Video video)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendVideo"), new Dictionary<string, string>
            {
                {"chat_id", $"{chatId}"},
                {"video", video.FileId}
            });
        }

        public Message SendVideo(int chatId, Video video, IReplyMarkup replyMarkup)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendVideo"), new Dictionary<string, string>
            {
                {"chat_id", $"{chatId}"},
                {"video", video.FileId},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            });
        }

        public Message SendVideo(Chat chat, Video video)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendVideo"), new Dictionary<string, string>
            {
                {"chat_id", $"{chat.Id}"},
                {"video", video.FileId}
            });
        }

        public Message SendVideo(Chat chat, Video video, IReplyMarkup replyMarkup)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendVideo"), new Dictionary<string, string>
            {
                {"chat_id", $"{chat.Id}"},
                {"video", video.FileId},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            });
        }


        public Message SendVideo(int chatId, string videoId)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendVideo"), new Dictionary<string, string>
            {
                {"chat_id", $"{chatId}"},
                {"video", videoId}
            });
        }

        public Message SendVideo(int chatId, string videoId, IReplyMarkup replyMarkup)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendVideo"), new Dictionary<string, string>
            {
                {"chat_id", $"{chatId}"},
                {"video", videoId},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            });
        }

        public Message SendVideo(Chat chat, string videoId)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendVideo"), new Dictionary<string, string>
            {
                {"chat_id", $"{chat.Id}"},
                {"video", videoId}
            });
        }

        public Message SendVideo(Chat chat, string videoId, IReplyMarkup replyMarkup)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendVideo"), new Dictionary<string, string>
            {
                {"chat_id", $"{chat.Id}"},
                {"video", videoId},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            });
        }

        public Message SendVideoFile(int chatId, string fileName)
        {
            if (!System.IO.File.Exists(fileName)) throw new FileNotFoundException();
            return TeFileUpload<Message>(BuildUriForMethod("sendVideo"), fileName, Path.GetFileName(fileName), "video",
                new Dictionary<string, string>
                {
                    {"chat_id", $"{chatId}"}
                });
        }

        public Message SendVideoFile(int chatId, string fileName, IReplyMarkup replyMarkup)
        {
            if (!System.IO.File.Exists(fileName)) throw new FileNotFoundException();
            return TeFileUpload<Message>(BuildUriForMethod("sendVideo"), fileName, Path.GetFileName(fileName), "video",
                new Dictionary<string, string>
                {
                    {"chat_id", $"{chatId}"},
                    {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
                });
        }

        public Message SendVideoFile(Chat chat, string fileName)
        {
            if (!System.IO.File.Exists(fileName)) throw new FileNotFoundException();
            return TeFileUpload<Message>(BuildUriForMethod("sendVideo"), fileName, Path.GetFileName(fileName), "video",
                new Dictionary<string, string>
                {
                    {"chat_id", $"{chat.Id}"}
                });
        }

        public Message SendVideoFile(Chat chat, string fileName, IReplyMarkup replyMarkup)
        {
            if (!System.IO.File.Exists(fileName)) throw new FileNotFoundException();
            return TeFileUpload<Message>(BuildUriForMethod("sendVideo"), fileName, Path.GetFileName(fileName), "video",
                new Dictionary<string, string>
                {
                    {"chat_id", $"{chat.Id}"},
                    {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
                });
        }

        public Message SendVideoFile(int chatId, byte[] bytes, string tFileName)
        {
            return TeFileUpload<Message>(BuildUriForMethod("sendVideo"), bytes, tFileName, "video",
                new Dictionary<string, string>
                {
                    {"chat_id", $"{chatId}"}
                });
        }

        public Message SendVideoFile(int chatId, byte[] bytes, string tFileName, IReplyMarkup replyMarkup)
        {
            return TeFileUpload<Message>(BuildUriForMethod("sendVideo"), bytes, tFileName, "video",
                new Dictionary<string, string>
                {
                    {"chat_id", $"{chatId}"},
                    {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
                });
        }

        public Message SendVideoFile(Chat chat, byte[] bytes, string tFileName)
        {
            return TeFileUpload<Message>(BuildUriForMethod("sendVideo"), bytes, tFileName, "video",
                new Dictionary<string, string>
                {
                    {"chat_id", $"{chat.Id}"}
                });
        }

        public Message SendVideoFile(Chat chat, byte[] bytes, string tFileName, IReplyMarkup replyMarkup)
        {
            return TeFileUpload<Message>(BuildUriForMethod("sendVideo"), bytes, tFileName, "video",
                new Dictionary<string, string>
                {
                    {"chat_id", $"{chat.Id}"},
                    {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
                });
        }

        #endregion

        #region Send Location

        public Message SendLocation(int chatId, Location location)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendLocation"), new Dictionary<string, string>
            {
                {"chat_id", $"{chatId}"},
                {"latitude", location.Latitude + ""},
                {"longitude", location.Longitude + ""}
            });
        }

        public Message SendLocation(int chatId, Location location, IReplyMarkup replyMarkup)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendLocation"), new Dictionary<string, string>
            {
                {"chat_id", $"{chatId}"},
                {"latitude", location.Latitude + ""},
                {"longitude", location.Longitude + ""},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            });
        }

        public Message SendLocation(Chat chat, Location location)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendLocation"), new Dictionary<string, string>
            {
                {"chat_id", $"{chat.Id}"},
                {"latitude", location.Latitude + ""},
                {"longitude", location.Longitude + ""}
            });
        }

        public Message SendLocation(Chat chat, Location location, IReplyMarkup replyMarkup)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendLocation"), new Dictionary<string, string>
            {
                {"chat_id", $"{chat.Id}"},
                {"latitude", location.Latitude + ""},
                {"longitude", location.Longitude + ""},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            });
        }

        public Message SendLocation(Chat chat, float latitude, float longitude)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendLocation"), new Dictionary<string, string>
            {
                {"chat_id", $"{chat.Id}"},
                {"latitude", latitude + ""},
                {"longitude", longitude + ""}
            });
        }

        public Message SendLocation(Chat chat, float latitude, float longitude, IReplyMarkup replyMarkup)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendLocation"), new Dictionary<string, string>
            {
                {"chat_id", $"{chat.Id}"},
                {"latitude", latitude + ""},
                {"longitude", longitude + ""},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            });
        }

        public Message SendLocation(int chatId, float latitude, float longitude)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendLocation"), new Dictionary<string, string>
            {
                {"chat_id", $"{chatId}"},
                {"latitude", latitude + ""},
                {"longitude", longitude + ""}
            });
        }

        public Message SendLocation(int chatId, float latitude, float longitude, IReplyMarkup replyMarkup)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendLocation"), new Dictionary<string, string>
            {
                {"chat_id", $"{chatId}"},
                {"latitude", latitude + ""},
                {"longitude", longitude + ""},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            });
        }

        public bool SendChatAction(int chatId, string action)
        {
            if (!GetChatActions().Contains(action))
            {
                throw new ArgumentException("Action is invalid");
            }
            SendTRequest(BuildUriForMethod("sendChatAction"), new Dictionary<string, string>
            {
                {"chat_id", $"{chatId}"},
                {"action", action}
            });
            return true;
        }

        public bool SendChatAction(Chat chat, string action)
        {
            if (!GetChatActions().Contains(action))
            {
                throw new ArgumentException("Action is invalid");
            }
            return SendTRequest(BuildUriForMethod("sendChatAction"), new Dictionary<string, string>
            {
                {"chat_id", $"{chat.Id}"},
                {"action", action}
            });
        }

        public string[] GetChatActions()
        {
            return new[]
            {
                "typing",
                "upload_photo", "record_video",
                "upload_video", "record_audio",
                "upload_audio",
                "upload_document"
            };
        }

        #endregion Send Location

        #region Send Photo

        public Message SendPhoto(int chatId, PhotoSize photo, string caption = null)
        {
            var fields = new Dictionary<string, string>
            {
                {"chat_id", $"{chatId}"},
                {"photo", photo.FileId}
            };
            if (caption != null) fields.Add("caption", caption);
            return SendTRequest<Message>(BuildUriForMethod("sendPhoto"), fields);
        }

        public Message SendPhoto(int chatId, PhotoSize photo, IReplyMarkup replyMarkup, string caption = null)
        {
            var fields = new Dictionary<string, string>
            {
                {"chat_id", $"{chatId}"},
                {"photo", photo.FileId},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            };
            if (caption != null) fields.Add("caption", caption);
            return SendTRequest<Message>(BuildUriForMethod("sendPhoto"), fields);
        }

        public Message SendPhoto(Chat chat, PhotoSize photo, string caption = null)
        {
            var fields = new Dictionary<string, string>
            {
                {"chat_id", $"{chat.Id}"},
                {"photo", photo.FileId}
            };
            if (caption != null) fields.Add("caption", caption);
            return SendTRequest<Message>(BuildUriForMethod("sendPhoto"), fields);
        }

        public Message SendPhoto(Chat chat, PhotoSize photo, IReplyMarkup replyMarkup, string caption = null)
        {
            var fields = new Dictionary<string, string>
            {
                {"chat_id", $"{chat.Id}"},
                {"photo", photo.FileId},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            };
            if (caption != null) fields.Add("caption", caption);
            return SendTRequest<Message>(BuildUriForMethod("sendPhoto"), fields);
        }

        public Message SendPhoto(int chatId, string photoId, string caption = null)
        {
            var fields = new Dictionary<string, string>
            {
                {"chat_id", $"{chatId}"},
                {"photo", photoId}
            };
            if (caption != null) fields.Add("caption", caption);
            return SendTRequest<Message>(BuildUriForMethod("sendPhoto"), fields);
        }

        public Message SendPhoto(int chatId, string photoId, IReplyMarkup replyMarkup, string caption = null)
        {
            var fields = new Dictionary<string, string>
            {
                {"chat_id", $"{chatId}"},
                {"photo", photoId},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            };
            if (caption != null) fields.Add("caption", caption);
            return SendTRequest<Message>(BuildUriForMethod("sendPhoto"), fields);
        }

        public Message SendPhoto(Chat chat, string photoId, string caption = null)
        {
            var fields = new Dictionary<string, string>
            {
                {"chat_id", $"{chat.Id}"},
                {"photo", photoId}
            };
            if (caption != null) fields.Add("caption", caption);
            return SendTRequest<Message>(BuildUriForMethod("sendPhoto"), fields);
        }

        public Message SendPhoto(Chat chat, string photoId, IReplyMarkup replyMarkup, string caption = null)
        {
            var fields = new Dictionary<string, string>
            {
                {"chat_id", $"{chat.Id}"},
                {"photo", photoId},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            };
            if (caption != null) fields.Add("caption", caption);
            return SendTRequest<Message>(BuildUriForMethod("sendPhoto"), fields);
        }

        public Message SendPhotoFile(int chatId, string fileName, string caption = null)
        {
            if (!System.IO.File.Exists(fileName)) throw new FileNotFoundException();
            var parametrs = new Dictionary<string, string>
            {
                {"chat_id", $"{chatId}"}
            };
            if (caption != null) parametrs.Add("caption", caption);
            return TeFileUpload<Message>(BuildUriForMethod("sendPhoto"), fileName, Path.GetFileName(fileName), "photo",
                parametrs);
        }

        public Message SendPhotoFile(int chatId, string fileName, IReplyMarkup replyMarkup, string caption = null)
        {
            if (!System.IO.File.Exists(fileName)) throw new FileNotFoundException();
            var parametrs = new Dictionary<string, string>
            {
                {"chat_id", $"{chatId}"},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            };
            if (caption != null) parametrs.Add("caption", caption);
            return TeFileUpload<Message>(BuildUriForMethod("sendPhoto"), fileName, Path.GetFileName(fileName), "photo",
                parametrs);
        }

        public Message SendPhotoFile(Chat chat, string fileName, string caption = null)
        {
            if (!System.IO.File.Exists(fileName)) throw new FileNotFoundException();
            var parametrs = new Dictionary<string, string>
            {
                {"chat_id", $"{chat.Id}"}
            };
            if (caption != null) parametrs.Add("caption", caption);
            return TeFileUpload<Message>(BuildUriForMethod("sendPhoto"), fileName, Path.GetFileName(fileName), "photo",
                parametrs);
        }

        public Message SendPhotoFile(Chat chat, string fileName, IReplyMarkup replyMarkup, string caption = null)
        {
            if (!System.IO.File.Exists(fileName)) throw new FileNotFoundException();
            var parametrs = new Dictionary<string, string>
            {
                {"chat_id", $"{chat.Id}"},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            };
            if (caption != null) parametrs.Add("caption", caption);
            return TeFileUpload<Message>(BuildUriForMethod("sendPhoto"), fileName, Path.GetFileName(fileName), "photo",
                parametrs);
        }

        public Message SendPhotoFile(int chatId, byte[] bytes, string tFileName, string caption = null)
        {
            var parametrs = new Dictionary<string, string>
            {
                {"chat_id", $"{chatId}"}
            };
            if (caption != null) parametrs.Add("caption", caption);
            return TeFileUpload<Message>(BuildUriForMethod("sendPhoto"), bytes, tFileName, "photo",
                parametrs);
        }

        public Message SendPhotoFile(int chatId, byte[] bytes, string tFileName, IReplyMarkup replyMarkup,
            string caption = null)
        {
            var parametrs = new Dictionary<string, string>
            {
                {"chat_id", $"{chatId}"},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            };
            if (caption != null) parametrs.Add("caption", caption);
            return TeFileUpload<Message>(BuildUriForMethod("sendPhoto"), bytes, tFileName, "photo",
                parametrs);
        }

        public Message SendPhotoFile(Chat chat, byte[] bytes, string tFileName, string caption = null)
        {
            var parametrs = new Dictionary<string, string>
            {
                {"chat_id", $"{chat.Id}"}
            };
            if (caption != null) parametrs.Add("caption", caption);
            return TeFileUpload<Message>(BuildUriForMethod("sendPhoto"), bytes, tFileName, "photo",
                parametrs);
        }

        public Message SendPhotoFile(Chat chat, byte[] bytes, string tFileName, IReplyMarkup replyMarkup,
            string caption = null)
        {
            var parametrs = new Dictionary<string, string>
            {
                {"chat_id", $"{chat.Id}"},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            };
            if (caption != null) parametrs.Add("caption", caption);
            return TeFileUpload<Message>(BuildUriForMethod("sendPhoto"), bytes, tFileName, "photo",
                parametrs);
        }

        #endregion Send Photo

        #region Send Document

        public Message SendDocument(int chatId, Document document)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendDocument"), new Dictionary<string, string>
            {
                {"chat_id", $"{chatId}"},
                {"document", document.FileId}
            });
        }

        public Message SendDocument(int chatId, Document document, IReplyMarkup replyMarkup)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendDocument"), new Dictionary<string, string>
            {
                {"chat_id", $"{chatId}"},
                {"document", document.FileId},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            });
        }

        public Message SendDocument(Chat chat, Document document)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendDocument"), new Dictionary<string, string>
            {
                {"chat_id", $"{chat.Id}"},
                {"document", document.FileId}
            });
        }

        public Message SendDocument(Chat chat, Document document, IReplyMarkup replyMarkup)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendDocument"), new Dictionary<string, string>
            {
                {"chat_id", $"{chat.Id}"},
                {"document", document.FileId},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            });
        }

        public Message SendDocument(int chatId, string documentId)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendDocument"), new Dictionary<string, string>
            {
                {"chat_id", $"{chatId}"},
                {"document", documentId}
            });
        }

        public Message SendDocument(int chatId, string documentId, IReplyMarkup replyMarkup)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendDocument"), new Dictionary<string, string>
            {
                {"chat_id", $"{chatId}"},
                {"document", documentId},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            });
        }

        public Message SendDocument(Chat chat, string documentId)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendDocument"), new Dictionary<string, string>
            {
                {"chat_id", $"{chat.Id}"},
                {"document", documentId}
            });
        }

        public Message SendDocument(Chat chat, string documentId, IReplyMarkup replyMarkup)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendDocument"), new Dictionary<string, string>
            {
                {"chat_id", $"{chat.Id}"},
                {"document", documentId},
                {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
            });
        }

        public Message SendDocumentFile(int chatId, string fileName)
        {
            if (!System.IO.File.Exists(fileName)) throw new FileNotFoundException();
            return TeFileUpload<Message>(BuildUriForMethod("sendDocument"), fileName, Path.GetFileName(fileName),
                "document",
                new Dictionary<string, string>
                {
                    {"chat_id", $"{chatId}"}
                });
        }

        public Message SendDocumentFile(int chatId, string fileName, IReplyMarkup replyMarkup)
        {
            if (!System.IO.File.Exists(fileName)) throw new FileNotFoundException();
            return TeFileUpload<Message>(BuildUriForMethod("sendDocument"), fileName, Path.GetFileName(fileName),
                "document",
                new Dictionary<string, string>
                {
                    {"chat_id", $"{chatId}"},
                    {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
                });
        }

        public Message SendDocumentFile(Chat chat, string fileName)
        {
            if (!System.IO.File.Exists(fileName)) throw new FileNotFoundException();
            return TeFileUpload<Message>(BuildUriForMethod("sendDocument"), fileName, Path.GetFileName(fileName),
                "document",
                new Dictionary<string, string>
                {
                    {"chat_id", $"{chat.Id}"}
                });
        }

        public Message SendDocumentFile(Chat chat, string fileName, IReplyMarkup replyMarkup)
        {
            if (!System.IO.File.Exists(fileName)) throw new FileNotFoundException();
            return TeFileUpload<Message>(BuildUriForMethod("sendDocument"), fileName, Path.GetFileName(fileName),
                "document",
                new Dictionary<string, string>
                {
                    {"chat_id", $"{chat.Id}"},
                    {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
                });
        }

        public Message SendDocumentFile(int chatId, byte[] bytes, string tFileName)
        {
            return TeFileUpload<Message>(BuildUriForMethod("sendDocument"), bytes, tFileName, "document",
                new Dictionary<string, string>
                {
                    {"chat_id", $"{chatId}"}
                });
        }

        public Message SendDocumentFile(int chatId, byte[] bytes, string tFileName, IReplyMarkup replyMarkup)
        {
            return TeFileUpload<Message>(BuildUriForMethod("sendDocument"), bytes, tFileName, "document",
                new Dictionary<string, string>
                {
                    {"chat_id", $"{chatId}"},
                    {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
                });
        }

        public Message SendDocumentFile(Chat chat, byte[] bytes, string tFileName)
        {
            return TeFileUpload<Message>(BuildUriForMethod("sendDocument"), bytes, tFileName, "document",
                new Dictionary<string, string>
                {
                    {"chat_id", $"{chat.Id}"}
                });
        }

        public Message SendDocumentFile(Chat chat, byte[] bytes, string tFileName, IReplyMarkup replyMarkup)
        {
            return TeFileUpload<Message>(BuildUriForMethod("sendDocument"), bytes, tFileName, "document",
                new Dictionary<string, string>
                {
                    {"chat_id", $"{chat.Id}"},
                    {"reply_markup", JsonConvert.SerializeObject(replyMarkup)}
                });
        }

        #endregion

        #region Get File

        public File GetFile(string fileId)
        {
            return SendTRequest<File>(BuildUriForMethod("getFile"), new Dictionary<string, string>
            {
                {"file_id", fileId}
            });
        }

        public File GetFile(FileBase file)
        {
            return SendTRequest<File>(BuildUriForMethod("getFile"), new Dictionary<string, string>
            {
                {"file_id", file.FileId}
            });
        }

        public string GetFileUri(string fileId)
        {
            var f = GetFile(fileId);
            return $"https://api.telegram.org/file/bot{Bot.Settings.Token}/{f.FilePath}";
        }

        public string GetFileUri(FileBase file)
        {
            var f = GetFile(file);
            return $"https://api.telegram.org/file/bot{Bot.Settings.Token}/{f.FilePath}";
        }

        #endregion

        #region Get user profile photos

        public UserProfilePhotos GetUserProfilePhotos(int userId, int offset = 0, int limit = 100)
        {
            return SendTRequest<UserProfilePhotos>(BuildUriForMethod("getUserProfilePhotos"), new Dictionary<string, string>()
            {
                { "user_id", $"{userId}" },
                { "offset", $"{offset}" },
                { "limit", $"{offset}" }
            });
        }

        public UserProfilePhotos GetUserProfilePhotos(User user, int offset = 0, int limit = 100)
        {
            return SendTRequest<UserProfilePhotos>(BuildUriForMethod("getUserProfilePhotos"), new Dictionary<string, string>()
            {
                { "user_id", $"{user.Id}" },
                { "offset", $"{offset}" },
                { "limit", $"{offset}" }
            });
        }

        #endregion

        #region Requests

        private bool SendTRequest(string uri)
        {
            try
            {
                CreateWebClient().DownloadString(uri);
                return true;
            }
            catch (Exception exc)
            {
                if (Bot.Settings.ResponsesToConsole)
                {
                    Telesharp.Logger.Log(LogType.Warning, "TRequest", "Failed to get response.");
                    if (Bot.Settings.ExceptionsToConsole)
                    {
                        Telesharp.Logger.Log(LogType.Info, "TRequest", "Exception: \n" + exc);
                    }
                }
                return false;
            }
        }

        private bool SendTRequest(string uri, Dictionary<string, string> parametrs)
        {
            try
            {
                var wc = CreateWebClient();
                wc.SendGET(uri, parametrs);
                return true;
            }
            catch (Exception exc)
            {
                if (Bot.Settings.ResponsesToConsole)
                {
                    Telesharp.Logger.Log(LogType.Warning, "TRequest", "Failed to get response.");
                    if (Bot.Settings.ExceptionsToConsole)
                    {
                        Telesharp.Logger.Log(LogType.Info, "TRequest", $"Exception:\n{exc}");
                    }
                }
                return false;
            }
        }

        private T SendTRequest<T>(string uri)
        {
            try
            {
                var token = JToken.Parse(CreateWebClient().DownloadString(uri));
                return token["result"].ToObject<T>();
            }
            catch (Exception exc)
            {
                if (Bot.Settings.ResponsesToConsole)
                {
                    Telesharp.Logger.Log(LogType.Warning, "TRequest", "Failed to get response.");
                    if (Bot.Settings.ExceptionsToConsole)
                    {
                        Telesharp.Logger.Log(LogType.Info, "TRequest", $"Exception:\n{exc}");
                    }
                }
                return default(T);
            }
        }

        private T SendTRequest<T>(string uri, Dictionary<string, string> parametrs) where T : class
        {
            try
            {
                var wc = CreateWebClient();
                var token = JToken.Parse(wc.SendGET(uri, parametrs));
                return token["result"].ToObject<T>();
            }
            catch (WebException exc)
            {
                try
                {
                    if (exc.Response?.GetResponseStream() == null) return default(T);
                    // ReSharper disable once AssignNullToNotNullAttribute
                    using (var reader = new StreamReader(exc.Response.GetResponseStream()))
                    {
                        var result = reader.ReadToEnd();
                        var token = JToken.Parse(result);
                        if (token["ok"].ToObject<bool>() == false)
                        {
                            Telesharp.Logger.Log(LogType.Error, "Telegram sent failure code: \n" + result);
                        }
                    }
                }
                catch (Exception exc2)
                {
                    if (Bot.Settings.InfoToConsole)
                    {
                        Telesharp.Logger.Log(LogType.Warning, "TRequest", "Failed to parse failure code/text");
                        if (Bot.Settings.ExceptionsToConsole)
                        {
                            Telesharp.Logger.Log(LogType.Error, "TRequest", "Exception: \n" + exc2);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                if (Bot.Settings.InfoToConsole)
                {
                    Telesharp.Logger.Log(LogType.Warning, "TRequest",
                        "Exception when trying to get response or parse JSON");
                    if (Bot.Settings.ExceptionsToConsole)
                    {
                        Telesharp.Logger.Log(LogType.Error, "TRequest", "Exception: \n" + exc);
                    }
                }
            }
            return default(T);
        }

        private T TeFileUpload<T>(string uri, string fileName, string tFileName, string fFieldName,
            Dictionary<string, string> parametrs) where T : class
        {
            try
            {
                var bytes = System.IO.File.ReadAllBytes(fileName);
                var file = new FormUpload.FileParameter(bytes, tFileName, "multipart/form-data");
                var fields = parametrs.ToDictionary(parametr => parametr.Key, parametr => (object) parametr.Value);
                fields.Add(fFieldName, file);
                if (Bot.Settings.InfoToConsole)
                {
                    Telesharp.Logger.Log(LogType.Info, "TFiSender",
                        $"Sending file with size: {(bytes.Length/1024.0).ToString("0.00")}Kb");
                }
                var resp = FormUpload.MultipartFormDataPost(uri,
                    "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36",
                    fields);
                if (resp?.GetResponseStream() == null) return default(T);
                // He does not consider it a solution ^^^ and i add this ↙ 
                // ReSharper disable once AssignNullToNotNullAttribute
                var streamreader = new StreamReader(resp.GetResponseStream());
                return JToken.Parse(streamreader.ReadToEnd())["result"].ToObject<T>();
            }
            catch (Exception exc)
            {
                if (Bot.Settings.InfoToConsole)
                {
                    Telesharp.Logger.Log(LogType.Warning, "TFiSender", "Failed to upload file.");
                    if (Bot.Settings.ExceptionsToConsole)
                    {
                        Telesharp.Logger.Log(LogType.Error, "TFiSender", "Exception: \n" + exc);
                    }
                }
                return default(T);
            }
        }

        private T TeFileUpload<T>(string uri, byte[] bytes, string tFileName, string fFieldName,
            Dictionary<string, string> parametrs) where T : class
        {
            try
            {
                var file = new FormUpload.FileParameter(bytes, tFileName, "multipart/form-data");
                var fields = parametrs.ToDictionary(parametr => parametr.Key, parametr => (object) parametr.Value);
                fields.Add(fFieldName, file);
                if (Bot.Settings.InfoToConsole)
                {
                    Telesharp.Logger.Log(LogType.Info, "TFiSender",
                        $"Sending file with size: {(bytes.Length/1024.0).ToString("0.00")}Kb");
                }
                var resp = FormUpload.MultipartFormDataPost(uri,
                    "Mozilla/5.0 (Windows NT 6.1) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/41.0.2228.0 Safari/537.36",
                    fields);
                if (resp?.GetResponseStream() == null) return default(T);
                // ReSharper disable once AssignNullToNotNullAttribute
                var streamreader = new StreamReader(resp.GetResponseStream());
                return JToken.Parse(streamreader.ReadToEnd())["result"].ToObject<T>();
            }
            catch (Exception exc)
            {
                if (Bot.Settings.InfoToConsole)
                {
                    Telesharp.Logger.Log(LogType.Warning, "TFiSender", "Failed to upload file.");
                    if (Bot.Settings.ExceptionsToConsole)
                    {
                        Telesharp.Logger.Log(LogType.Error, "TFiSender", "Exception: \n" + exc);
                    }
                }
                return default(T);
            }
        }

        private AdvancedWebClient CreateWebClient()
        {
            var client = new AdvancedWebClient(Bot.Settings.TimeoutForRequest);
            if (Bot.Settings.Proxy != null)
            {
                client.Proxy = Bot.Settings.Proxy;
            }
            return client;
        }

        #endregion
    }
}