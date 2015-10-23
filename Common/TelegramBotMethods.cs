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
                ["chat_id"] = toChat.Id,
                ["from_chat_id"] = fromChat.Id,
                ["message_id"] = $"{message.MessageId}"
            });
        }

        public bool SendChatAction(string chatId, string action)
        {
            if (!GetChatActions().Contains(action))
            {
                throw new ArgumentException("Action is invalid");
            }
            SendTRequest(BuildUriForMethod("sendChatAction"), new Dictionary<string, string>
            {
                ["chat_id"] = chatId,
                ["action"] = action
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
                ["chat_id"] = chat.Id,
                ["action"] = action
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

        public Message SendMessage(string chatId, string text, bool disableWebPagePreview = false,
            IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chatId,
                ["text"] = text,
                ["disable_web_page_preview"] = $"{disableWebPagePreview}"
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendMessage"), fields);
        }

        public Message SendMessage(Chat chat, string text, bool disableWebPagePreview = false,
            IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chat.Id,
                ["text"] = text,
                ["disable_web_page_preview"] = $"{disableWebPagePreview}"
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendMessage"), fields);
        }

        #endregion Send Text Message

        #region Reply

        #region Reply with Text

        public Message ReplyToMessage(Message message, string text, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                //{"text", text},
                //{"reply_to_message_id", $"{message.MessageId}"},
                //{"chat_id", message.Chat.Id}
                ["text"] = text,
                ["reply_to_message_id"] = $"{message.MessageId}",
                ["chat_id"] = message.Chat.Id
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendMessage"), fields);
        }

        #endregion

        #region Reply with Photo

        public Message ReplyToMessage(Message message, PhotoSize photo, string caption = null,
            IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["photo"] = photo.FileId,
                ["reply_to_message_id"] = $"{message.MessageId}",
                ["chat_id"] = message.Chat.Id
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            if (caption != null) fields.Add("caption", caption);
            return SendTRequest<Message>(BuildUriForMethod("sendPhoto"), fields);
        }

        public Message ReplyToMessageWithPhotoFile(Message message, FileInfo fileInfo, string caption = null,
            IReplyMarkup replyMarkup = null)
        {
            if (fileInfo == null || !fileInfo.Exists) throw new ArgumentNullException();
            if (fileInfo.Attributes.HasFlag(FileAttributes.Directory)) throw new InvalidOperationException();
            var fields = new Dictionary<string, string>
            {
                ["reply_to_message_id"] = $"{message.MessageId}",
                ["chat_id"] = message.Chat.Id
            };
            if (caption != null) fields.Add("caption", caption);
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return TeFileUpload<Message>(BuildUriForMethod("sendPhoto"), fileInfo.FullName, fileInfo.Name, "photo",
                fields);
        }

        #endregion

        #region Reply with Audio

        public Message ReplyToMessage(Message message, Audio audio, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["audio"] = audio.FileId,
                ["reply_to_message_id"] = $"{message.MessageId}",
                ["chat_id"] = message.Chat.Id
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendAudio"), fields);
        }

        public Message ReplyToMessageWithAudioFile(Message message, FileInfo fileInfo, IReplyMarkup replyMarkup = null)
        {
            if (fileInfo == null || !fileInfo.Exists) throw new ArgumentNullException();
            if (fileInfo.Attributes.HasFlag(FileAttributes.Directory)) throw new InvalidOperationException();
            var fields = new Dictionary<string, string>
            {
                ["reply_to_message_id"] = $"{message.MessageId}",
                ["chat_id"] = message.Chat.Id
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return TeFileUpload<Message>(BuildUriForMethod("sendAudio"), fileInfo.FullName, fileInfo.Name, "audio",
                fields);
        }

        #endregion

        #region Reply with Document

        public Message ReplyToMessage(Message message, Document document, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["document"] = document.FileId,
                ["reply_to_message_id"] = $"{message.MessageId}",
                ["chat_id"] = message.Chat.Id
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendDocument"), fields);
        }

        public Message ReplyToMessageWithDocumentFile(Message message, FileInfo fileInfo,
            IReplyMarkup replyMarkup = null)
        {
            if (fileInfo == null || !fileInfo.Exists) throw new ArgumentNullException();
            if (fileInfo.Attributes.HasFlag(FileAttributes.Directory)) throw new InvalidOperationException();
            var fields = new Dictionary<string, string>
            {
                ["reply_to_message_id"] = $"{message.MessageId}",
                ["chat_id"] = message.Chat.Id
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return TeFileUpload<Message>(BuildUriForMethod("sendDocument"), fileInfo.FullName, fileInfo.Name, "document",
                fields);
        }

        #endregion

        #region Reply with Sticker

        public Message ReplyToMessage(Message message, Sticker sticker, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["sticker"] = sticker.FileId,
                ["reply_to_message_id"] = $"{message.MessageId}",
                ["chat_id"] = message.Chat.Id
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendSticker"), fields);
        }

        public Message ReplyToMessageWithStickerFile(Message message, FileInfo fileInfo, IReplyMarkup replyMarkup = null)
        {
            if (fileInfo == null || !fileInfo.Exists) throw new ArgumentNullException();
            if (fileInfo.Attributes.HasFlag(FileAttributes.Directory)) throw new InvalidOperationException();
            var fields = new Dictionary<string, string>
            {
                ["reply_to_message_id"] = $"{message.MessageId}",
                ["chat_id"] = message.Chat.Id
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return TeFileUpload<Message>(BuildUriForMethod("sendSticker"), fileInfo.FullName, fileInfo.Name, "sticker",
                fields);
        }

        #endregion

        #region Reply with Video

        public Message ReplyToMessage(Message message, Video video, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["video"] = video.FileId,
                ["reply_to_message_id"] = $"{message.MessageId}",
                ["chat_id"] = message.Chat.Id
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendVideo"), fields);
        }

        public Message ReplyToMessageWithVideoFile(Message message, FileInfo fileInfo, IReplyMarkup replyMarkup = null)
        {
            if (fileInfo == null || !fileInfo.Exists) throw new ArgumentNullException();
            if (fileInfo.Attributes.HasFlag(FileAttributes.Directory)) throw new InvalidOperationException();
            var fields = new Dictionary<string, string>
            {
                ["reply_to_message_id"] = $"{message.MessageId}",
                ["chat_id"] = message.Chat.Id
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return TeFileUpload<Message>(BuildUriForMethod("sendVideo"), fileInfo.FullName, fileInfo.Name, "video",
                fields);
        }

        #endregion

        #region Reply with Voice

        public Message ReplyToMessage(Message message, Voice voice, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["voice"] = voice.FileId,
                ["reply_to_message_id"] = $"{message.MessageId}",
                ["chat_id"] = message.Chat.Id
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendVoice"), fields);
        }

        public Message ReplyToMessageWithVoiceFile(Message message, FileInfo fileInfo, IReplyMarkup replyMarkup = null)
        {
            if (fileInfo == null || !fileInfo.Exists) throw new ArgumentNullException();
            if (fileInfo.Attributes.HasFlag(FileAttributes.Directory)) throw new InvalidOperationException();
            var fields = new Dictionary<string, string>
            {
                ["reply_to_message_id"] = $"{message.MessageId}",
                ["chat_id"] = message.Chat.Id
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return TeFileUpload<Message>(BuildUriForMethod("sendVoice"), fileInfo.FullName, fileInfo.Name, "voice",
                fields);
        }

        #endregion

        #region Reply with Location

        public Message ReplyToMessage(Message message, Location location, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["latitude"] = $"{location.Latitude.ToString("000.000")}",
                ["longitude"] = $"{location.Longitude.ToString("000.000")}",
                ["reply_to_message_id"] = $"{message.MessageId}",
                ["chat_id"] = message.Chat.Id
            };
            return SendTRequest<Message>(BuildUriForMethod("sendLocation"), fields);
        }

        #endregion

        #endregion

        #region Send Audio

        public Message SendAudio(string chatId, Audio audio, string performer = null, string title = null,
            IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chatId"] = chatId,
                ["audio"] = audio.FileId
            };
            if (performer != null) fields.Add("performer", performer);
            if (title != null) fields.Add("title", title);
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendAudio"), fields);
        }

        public Message SendAudio(Chat chat, Audio audio, string performer = null, string title = null,
            IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chatId"] = chat.Id,
                ["audio"] = audio.FileId
            };
            if (performer != null) fields.Add("performer", performer);
            if (title != null) fields.Add("title", title);
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendAudio"), fields);
        }

        public Message SendAudio(string chatId, string audioId, string performer = null, string title = null,
            IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chatId"] = chatId,
                ["audio"] = audioId
            };
            if (performer != null) fields.Add("performer", performer);
            if (title != null) fields.Add("title", title);
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendAudio"), fields);
        }

        public Message SendAudio(Chat chat, string audioId, string performer = null, string title = null,
            IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chatId"] = chat.Id,
                ["audio"] = audioId
            };
            if (performer != null) fields.Add("performer", performer);
            if (title != null) fields.Add("title", title);
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendAudio"), fields);
        }

        public Message SendAudioFile(string chatId, string filename, string performer = null, string title = null,
            int duration = -1, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chatId
            };
            if (performer != null) fields.Add("performer", performer);
            if (title != null) fields.Add("title", title);
            if (duration != -1 && duration > 0) fields.Add("duration", $"{duration}");
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return TeFileUpload<Message>(BuildUriForMethod("sendAudio"), filename, Path.GetFileName(filename), "audio",
                fields);
        }

        public Message SendAudioFile(Chat chat, string filename, string performer = null, string title = null,
            int duration = -1, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chat.Id
            };
            if (performer != null) fields.Add("performer", performer);
            if (title != null) fields.Add("title", title);
            if (duration != -1 && duration > 0) fields.Add("duration", $"{duration}");
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return TeFileUpload<Message>(BuildUriForMethod("sendAudio"), filename, Path.GetFileName(filename), "audio",
                fields);
        }

        public Message SendAudioFile(string chatId, byte[] bytes, string filename, string performer = null,
            string title = null, int duration = -1, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chatId
            };
            if (performer != null) fields.Add("performer", performer);
            if (title != null) fields.Add("title", title);
            if (duration != -1 && duration > 0) fields.Add("duration", $"{duration}");
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return TeFileUpload<Message>(BuildUriForMethod("sendAudio"), bytes, filename, "audio", fields);
        }

        public Message SendAudioFile(Chat chat, byte[] bytes, string filename, string performer = null,
            string title = null, int duration = -1, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chat.Id
            };
            if (performer != null) fields.Add("performer", performer);
            if (title != null) fields.Add("title", title);
            if (duration != -1 && duration > 0) fields.Add("duration", $"{duration}");
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return TeFileUpload<Message>(BuildUriForMethod("sendAudio"), bytes, filename, "audio", fields);
        }

        #endregion

        #region Send Sticker

        public Message SendSticker(string chatId, Sticker sticker, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chatId,
                ["sticker"] = sticker.FileId
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendSticker"), fields);
        }

        public Message SendSticker(Chat chat, Sticker sticker, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chat.Id,
                ["sticker"] = sticker.FileId
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendSticker"), fields);
        }

        public Message SendSticker(string chatId, string stickerId, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chatId,
                ["sticker"] = stickerId
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendSticker"), fields);
        }

        public Message SendSticker(Chat chat, string stickerId, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chat.Id,
                ["sticker"] = stickerId
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendSticker"), fields);
        }

        #endregion Send Sticker

        #region Send Video

        public Message SendVideo(string chatId, Video video, string caption = null, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chatId,
                ["video"] = video.FileId
            };
            if (caption != null) fields.Add("caption", caption);
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendVideo"), fields);
        }

        public Message SendVideo(Chat chat, Video video, string caption = null, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chat.Id,
                ["video"] = video.FileId
            };
            if (caption != null) fields.Add("caption", caption);
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendVideo"), fields);
        }

        public Message SendVideo(string chatId, string videoId, string caption = null, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chatId,
                ["video"] = videoId
            };
            if (caption != null) fields.Add("caption", caption);
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendVideo"), fields);
        }

        public Message SendVideo(Chat chat, string videoId, string caption = null, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chat.Id,
                ["video"] = videoId
            };
            if (caption != null) fields.Add("caption", caption);
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendVideo"), fields);
        }

        public Message SendVideoFile(string chatId, string filename, string caption = null, int duration = -1,
            IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chatId
            };
            if (caption != null) fields.Add("caption", caption);
            if (duration != -1 && duration > 0) fields.Add("duration", $"{duration}");
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return TeFileUpload<Message>(BuildUriForMethod("sendVideo"), filename, Path.GetFileName(filename), "video",
                fields);
        }

        public Message SendVideoFile(Chat chat, string filename, string caption = null, int duration = -1,
            IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chat.Id
            };
            if (caption != null) fields.Add("caption", caption);
            if (duration != -1 && duration > 0) fields.Add("duration", $"{duration}");
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return TeFileUpload<Message>(BuildUriForMethod("sendVideo"), filename, Path.GetFileName(filename), "video",
                fields);
        }

        public Message SendVideoFile(string chatId, byte[] bytes, string filename, string caption = null,
            int duration = -1,
            IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chatId
            };
            if (caption != null) fields.Add("caption", caption);
            if (duration != -1 && duration > 0) fields.Add("duration", $"{duration}");
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return TeFileUpload<Message>(BuildUriForMethod("sendVideo"), bytes, filename, "video", fields);
        }

        public Message SendVideoFile(Chat chat, byte[] bytes, string filename, string caption = null, int duration = -1,
            IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chat.Id
            };
            if (caption != null) fields.Add("caption", caption);
            if (duration != -1 && duration > 0) fields.Add("duration", $"{duration}");
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return TeFileUpload<Message>(BuildUriForMethod("sendVideo"), bytes, filename, "video", fields);
        }

        #endregion

        #region Send Voice

        public Message SendVoice(string chatId, Voice voice, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chatId"] = chatId,
                ["voice"] = voice.FileId
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendVoice"), fields);
        }

        public Message SendVoice(Chat chat, Voice voice, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chatId"] = chat.Id,
                ["voice"] = voice.FileId
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendVoice"), fields);
        }

        public Message SendVoice(string chatId, string voiceId, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chatId"] = chatId,
                ["voice"] = voiceId
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendVoice"), fields);
        }

        public Message SendVoice(Chat chat, string voiceId, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chatId"] = chat.Id,
                ["voice"] = voiceId
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendVoice"), fields);
        }

        public Message SendVoiceFile(string chatId, string filename, int duration = -1, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chatId
            };
            if (duration != -1 && duration > 0) fields.Add("duration", $"{duration}");
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return TeFileUpload<Message>(BuildUriForMethod("sendVoice"), filename, Path.GetFileName(filename), "voice",
                fields);
        }

        public Message SendVoiceFile(Chat chat, string filename, int duration = -1, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chat.Id
            };
            if (duration != -1 && duration > 0) fields.Add("duration", $"{duration}");
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return TeFileUpload<Message>(BuildUriForMethod("sendVoice"), filename, Path.GetFileName(filename), "voice",
                fields);
        }

        public Message SendVoiceFile(string chatId, byte[] bytes, string filename, int duration = -1,
            IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chatId
            };
            if (duration != -1 && duration > 0) fields.Add("duration", $"{duration}");
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return TeFileUpload<Message>(BuildUriForMethod("sendVoice"), bytes, filename, "voice", fields);
        }

        public Message SendVoiceFile(Chat chat, byte[] bytes, string filename, int duration = -1,
            IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chat.Id
            };
            if (duration != -1 && duration > 0) fields.Add("duration", $"{duration}");
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return TeFileUpload<Message>(BuildUriForMethod("sendVoice"), bytes, filename, "voice", fields);
        }

        #endregion

        #region Send Location

        public Message SendLocation(string chatId, Location location, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chatId,
                ["latitude"] = $"{location.Latitude.ToString("000.000")}",
                ["longitude"] = $"{location.Longitude.ToString("000.000")}"
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendLocation"), fields);
        }

        public Message SendLocation(Chat chat, Location location, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chat.Id,
                ["latitude"] = $"{location.Latitude.ToString("000.000")}",
                ["longitude"] = $"{location.Longitude.ToString("000.000")}"
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendLocation"), fields);
        }

        public Message SendLocation(Chat chat, float latitude, float longitude, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chat.Id,
                ["latitude"] = $"{latitude.ToString("000.000")}",
                ["longitude"] = $"{longitude.ToString("000.000")}"
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendLocation"), fields);
        }

        public Message SendLocation(string chatId, float latitude, float longitude, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chatId,
                ["latitude"] = $"{latitude.ToString("000.000")}",
                ["longitude"] = $"{longitude.ToString("000.000")}"
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendLocation"), fields);
        }

        #endregion Send Location

        #region Send Photo

        public Message SendPhoto(string chatId, PhotoSize photo, string caption = null, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chatId,
                ["photo"] = photo.FileId
            };
            if (caption != null) fields.Add("caption", caption);
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendPhoto"), fields);
        }

        public Message SendPhoto(Chat chat, PhotoSize photo, string caption = null, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chat.Id,
                ["photo"] = photo.FileId
            };
            if (caption != null) fields.Add("caption", caption);
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendPhoto"), fields);
        }

        public Message SendPhoto(string chatId, string photoId, string caption = null, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chatId,
                ["photo"] = photoId
            };
            if (caption != null) fields.Add("caption", caption);
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendPhoto"), fields);
        }

        public Message SendPhoto(Chat chat, string photoId, string caption = null, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chat.Id,
                ["photo"] = photoId
            };
            if (caption != null) fields.Add("caption", caption);
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendPhoto"), fields);
        }

        public Message SendPhotoFile(string chatId, string filename, string caption = null,
            IReplyMarkup replyMarkup = null)
        {
            if (!System.IO.File.Exists(filename)) throw new FileNotFoundException();
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chatId
            };
            if (caption != null) fields.Add("caption", caption);
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return TeFileUpload<Message>(BuildUriForMethod("sendPhoto"), filename, Path.GetFileName(filename),
                "photo", fields);
        }

        public Message SendPhotoFile(Chat chat, string filename, string caption = null, IReplyMarkup replyMarkup = null)
        {
            if (!System.IO.File.Exists(filename)) throw new FileNotFoundException();
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chat.Id
            };
            if (caption != null) fields.Add("caption", caption);
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return TeFileUpload<Message>(BuildUriForMethod("sendPhoto"), filename, Path.GetFileName(filename),
                "photo", fields);
        }

        public Message SendPhotoFile(string chatId, byte[] bytes, string filename, string caption = null,
            IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chatId
            };
            if (caption != null) fields.Add("caption", caption);
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return TeFileUpload<Message>(BuildUriForMethod("sendPhoto"), bytes, filename,
                "photo", fields);
        }

        public Message SendPhotoFile(Chat chat, byte[] bytes, string filename, string caption = null,
            IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chat.Id
            };
            if (caption != null) fields.Add("caption", caption);
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return TeFileUpload<Message>(BuildUriForMethod("sendPhoto"), bytes, filename,
                "photo", fields);
        }

        #endregion Send Photo

        #region Send Document

        public Message SendDocument(string chatId, Document document, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chatId,
                ["document"] = document.FileId
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendDocument"), fields);
        }

        public Message SendDocument(Chat chat, Document document, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chat.Id,
                ["document"] = document.FileId
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendDocument"), fields);
        }

        public Message SendDocument(string chatId, string documentId, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chatId,
                ["document"] = documentId
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendDocument"), fields);
        }

        public Message SendDocument(Chat chat, string documentId, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chat.Id,
                ["document"] = documentId
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return SendTRequest<Message>(BuildUriForMethod("sendDocument"), fields);
        }

        public Message SendDocumentFile(string chatId, string filename, IReplyMarkup replyMarkup = null)
        {
            if (!System.IO.File.Exists(filename)) throw new FileNotFoundException();
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chatId
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return TeFileUpload<Message>(BuildUriForMethod("sendDocument"), filename, Path.GetFileName(filename),
                "document", fields);
        }

        public Message SendDocumentFile(Chat chat, string filename, IReplyMarkup replyMarkup = null)
        {
            if (!System.IO.File.Exists(filename)) throw new FileNotFoundException();
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chat.Id
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return TeFileUpload<Message>(BuildUriForMethod("sendDocument"), filename, Path.GetFileName(filename),
                "document", fields);
        }

        public Message SendDocumentFile(string chatId, byte[] bytes, string filename, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chatId
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return TeFileUpload<Message>(BuildUriForMethod("sendDocument"), bytes, filename,
                "document", fields);
        }

        public Message SendDocumentFile(Chat chat, byte[] bytes, string filename, IReplyMarkup replyMarkup = null)
        {
            var fields = new Dictionary<string, string>
            {
                ["chat_id"] = chat.Id
            };
            if (replyMarkup != null) fields.Add("reply_markup", JsonConvert.SerializeObject(replyMarkup));
            return TeFileUpload<Message>(BuildUriForMethod("sendDocument"), bytes, filename,
                "document", fields);
        }

        #endregion

        #region Get File

        public File GetFile(string fileId)
        {
            return SendTRequest<File>(BuildUriForMethod("getFile"), new Dictionary<string, string>
            {
                ["file_id"] = fileId
            });
        }

        public File GetFile(FileBase file)
        {
            return SendTRequest<File>(BuildUriForMethod("getFile"), new Dictionary<string, string>
            {
                ["file_id"] = file.FileId
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
            return SendTRequest<UserProfilePhotos>(BuildUriForMethod("getUserProfilePhotos"),
                new Dictionary<string, string>
                {
                    ["user_id"] = $"{userId}",
                    ["offset"] = $"{offset}",
                    ["limit"] = $"{limit}"
                });
        }

        public UserProfilePhotos GetUserProfilePhotos(User user, int offset = 0, int limit = 100)
        {
            return SendTRequest<UserProfilePhotos>(BuildUriForMethod("getUserProfilePhotos"),
                new Dictionary<string, string>
                {
                    ["user_id"] = user.Id,
                    ["offset"] = $"{offset}",
                    ["limit"] = $"{limit}"
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
                CreateWebClient().SendGET(uri, parametrs);
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
                //var wc = CreateWebClient();
                var token = JToken.Parse(CreateWebClient().SendGET(uri, parametrs));
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