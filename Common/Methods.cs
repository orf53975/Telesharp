using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Linq;
using System.Net;
using Newtonsoft.Json.Linq;
using Telesharp.Common.BotTypes;
using Telesharp.Common.TelesharpTypes;
using Telesharp.Common.Types;

namespace Telesharp.Common
{
    public class TelegramBotMethods
    {
        private int _updateOffset;

        public TelegramBotMethods(Bot bot)
        {
            Bot = bot;
        }

        private Bot Bot { get; set; }

        public string BuildUriForMethod(string method)
        {
            var uri = "http://api.telegram.org/bot" + Bot.Settings.Token + "/" + method;
            //if (Bot.Settings.InfoToConsole)
            //{
            //    Console.WriteLine("Ready url: " + uri);
            //}
            // Not useful
            return uri;
        }

        public bool CheckConnection()
        {
            return SendTRequest(BuildUriForMethod("getMe"));
        }

        public User GetMe()
        {
            var me = SendTRequest<User>(BuildUriForMethod("getMe"));
            return me;
        }

        public Update[] GetUpdates()
        {
            try
            {
                var updates = SendTRequest<Update[]>(BuildUriForMethod("getUpdates"), new Dictionary<string, string>
                {
                    {"offset", _updateOffset + ""}
                });
                if (updates.Length == 0)
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

        [SuppressMessage("ReSharper", "MethodOverloadWithOptionalParameter")]
        public Update[] GetUpdates(int offset = 0, int limit = 100, int timeout = 0)
        {
            return SendTRequest<Update[]>(BuildUriForMethod("getUpdates"), new Dictionary<string, string>
            {
                {"offset", offset + ""},
                {"limit", limit + ""},
                {"timeout", timeout + ""}
            });
        }

        public Message SendMessage(int chatId, string text, bool disableWebPagePreview = false)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendMessage"), new Dictionary<string, string>
            {
                {"chat_id", chatId + ""},
                {"text", text},
                {"disable_web_page_preview", disableWebPagePreview + ""}
            });
        }

        public Message SendMessage(Chat chat, string text, bool disableWebPagePreview = false)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendMessage"), new Dictionary<string, string>
            {
                {"chat_id", chat.Id + ""},
                {"text", text},
                {"disable_web_page_preview", disableWebPagePreview + ""}
            });
        }

        public Message ReplyToMessage(Message message, string text)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendMessage"), new Dictionary<string, string>
            {
                {"text", text},
                {"reply_to_message_id", message.MessageId + ""},
                {"chat_id", message.Chat.Id + ""}
            });
        }

        public Message ReplyToMessage(Message message, PhotoSize photo)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendPhoto"), new Dictionary<string, string>
            {
                { "photo", photo.FileId },
                { "reply_to_message_id", message.MessageId + "" },
                { "chat_id", message.Chat.Id + "" }
            });
        }

        public Message ReplyToMessage(Message message, Audio audio)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendAudio"), new Dictionary<string, string>
            {
                { "audio", audio.FileId },
                { "reply_to_message_id", message.MessageId + "" },
                { "chat_id", message.Chat.Id + "" }
            });
        }

        public Message ReplyToMessage(Message message, Document document)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendDocument"), new Dictionary<string, string>
            {
                { "document", document.FileId },
                { "reply_to_message_id", message.MessageId + "" },
                { "chat_id", message.Chat.Id + "" }
            });
        }

        public Message ReplyToMessage(Message message, Sticker sticker)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendSticker"), new Dictionary<string, string>
            {
                { "sticker", sticker.FileId },
                { "reply_to_message_id", message.MessageId + "" },
                { "chat_id", message.Chat.Id + "" }
            });
        }

        public Message ReplyToMessage(Message message, Video video)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendVideo"), new Dictionary<string, string>
            {
                { "video", video.FileId },
                { "reply_to_message_id", message.MessageId + "" },
                { "chat_id", message.Chat.Id + "" }
            });
        }

        public Message ReplyToMessage(Message message, Location location)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendLocation"), new Dictionary<string, string>
            {
                { "latitude", location.Latitude + "" },
                { "longitude", location.Longitude + "" },
                { "reply_to_message_id", message.MessageId + "" },
                { "chat_id", message.Chat.Id + "" }
            });
        }

        public Message ForwardMessage(Message message, Chat fromChat, Chat toChat)
        {
            return SendTRequest<Message>(BuildUriForMethod("forwardMessage"), new Dictionary<string, string>
            {
                {"chat_id", toChat.Id + ""},
                {"from_chat_id", fromChat.Id + ""},
                {"message_id", message.MessageId + ""}
            });
        }

        public Message SendPhoto(int chatId, PhotoSize photo, string caption = null)
        {
            var fields = new Dictionary<string, string>
            {
                {"chat_id", chatId + ""},
                {"photo", photo.FileId}
            };
            if (caption != null) fields.Add("caption", caption);
            return SendTRequest<Message>(BuildUriForMethod("sendPhoto"), fields);
        }

        public Message SendPhoto(Chat chat, PhotoSize photo, string caption = null)
        {
            var fields = new Dictionary<string, string>
            {
                {"chat_id", chat.Id + ""},
                {"photo", photo.FileId}
            };
            if (caption != null) fields.Add("caption", caption);
            return SendTRequest<Message>(BuildUriForMethod("sendPhoto"), fields);
        }

        public Message SendPhoto(int chatId, string photoId, string caption = null)
        {
            var fields = new Dictionary<string, string>
            {
                {"chat_id", chatId + ""},
                {"photo", photoId}
            };
            if (caption != null) fields.Add("caption", caption);
            return SendTRequest<Message>(BuildUriForMethod("sendPhoto"), fields);
        }

        public Message SendPhoto(Chat chat, string photoId, string caption = null)
        {
            var fields = new Dictionary<string, string>
            {
                {"chat_id", chat.Id + ""},
                {"photo", photoId}
            };
            if (caption != null) fields.Add("caption", caption);
            return SendTRequest<Message>(BuildUriForMethod("sendPhoto"), fields);
        }

        public Message SendAudio(int chatId, Audio audio)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendAudio"), new Dictionary<string, string>
            {
                {"chat_id", chatId + ""},
                {"audio", audio.FileId}
            });
        }

        public Message SendAudio(Chat chat, Audio audio)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendAudio"), new Dictionary<string, string>
            {
                {"chat_id", chat.Id + ""},
                {"audio", audio.FileId}
            });
        }


        public Message SendAudio(int chatId, string audioId)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendAudio"), new Dictionary<string, string>
            {
                {"chat_id", chatId + ""},
                {"audio", audioId}
            });
        }

        public Message SendAudio(Chat chat, string audioId)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendAudio"), new Dictionary<string, string>
            {
                {"chat_id", chat.Id + ""},
                {"audio", audioId}
            });
        }

        public Message SendDocument(int chatId, Document document)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendDocument"), new Dictionary<string, string>
            {
                {"chat_id", chatId + ""},
                {"document", document.FileId}
            });
        }

        public Message SendDocument(Chat chat, Document document)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendDocument"), new Dictionary<string, string>
            {
                {"chat_id", chat.Id + ""},
                {"document", document.FileId}
            });
        }

        public Message SendDocument(int chatId, string documentId)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendDocument"), new Dictionary<string, string>
            {
                {"chat_id", chatId + ""},
                {"document", documentId}
            });
        }

        public Message SendDocument(Chat chat, string documentId)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendDocument"), new Dictionary<string, string>
            {
                {"chat_id", chat.Id + ""},
                {"document", documentId}
            });
        }

        public Message SendSticker(int chatId, Sticker sticker)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendSticker"), new Dictionary<string, string>
            {
                {"chat_id", chatId + ""},
                {"sticker", sticker.FileId}
            });
        }

        public Message SendSticker(Chat chat, Sticker sticker)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendSticker"), new Dictionary<string, string>
            {
                {"chat_id", chat.Id + ""},
                {"sticker", sticker.FileId}
            });
        }

        public Message SendSticker(int chatId, string stickerId)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendSticker"), new Dictionary<string, string>
            {
                {"chat_id", chatId + ""},
                {"sticker", stickerId}
            });
        }

        public Message SendSticker(Chat chat, string stickerId)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendSticker"), new Dictionary<string, string>
            {
                {"chat_id", chat.Id + ""},
                {"sticker", stickerId}
            });
        }

        public Message SendVideo(int chatId, Video video)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendVideo"), new Dictionary<string, string>
            {
                {"chat_id", chatId + ""},
                {"audio", video.FileId}
            });
        }

        public Message SendVideo(Chat chat, Video video)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendVideo"), new Dictionary<string, string>
            {
                {"chat_id", chat.Id + ""},
                {"audio", video.FileId}
            });
        }


        public Message SendVideo(int chatId, string videoId)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendVideo"), new Dictionary<string, string>
            {
                {"chat_id", chatId + ""},
                {"audio", videoId}
            });
        }

        public Message SendVideo(Chat chat, string videoId)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendVideo"), new Dictionary<string, string>
            {
                {"chat_id", chat.Id + ""},
                {"audio", videoId}
            });
        }

        public Message SendLocation(int chatId, Location location)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendLocation"), new Dictionary<string, string>
            {
                {"chat_id", chatId + ""},
                {"latitude", location.Latitude + ""},
                {"longitude", location.Longitude + ""}
            });
        }

        public Message SendLocation(Chat chat, Location location)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendLocation"), new Dictionary<string, string>
            {
                {"chat_id", chat.Id + ""},
                {"latitude", location.Latitude + ""},
                {"longitude", location.Longitude + ""}
            });
        }

        public Message SendLocation(Chat chat, float latitude, float longitude)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendLocation"), new Dictionary<string, string>
            {
                {"chat_id", chat.Id + ""},
                {"latitude", latitude + ""},
                {"longitude", longitude + ""}
            });
        }

        public Message SendLocation(int chatId, float latitude, float longitude)
        {
            return SendTRequest<Message>(BuildUriForMethod("sendLocation"), new Dictionary<string, string>
            {
                {"chat_id", chatId + ""},
                {"latitude", latitude + ""},
                {"longitude", longitude + ""}
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
                {"chat_id", chatId + ""},
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
                {"chat_id", chat.Id + ""},
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

        #region Requests

        private bool SendTRequest(string uri)
        {
            try
            {
                new AdvancedWebClient(Bot.Settings.TimeoutForRequest).DownloadString(uri);
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
                var wc = new AdvancedWebClient(Bot.Settings.TimeoutForRequest);
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
                        Telesharp.Logger.Log(LogType.Info, "TRequest", "Exception: \n" + exc);
                    }
                }
                return false;
            }
        }

        private T SendTRequest<T>(string uri)
        {
            try
            {
                var token = JToken.Parse(new AdvancedWebClient(Bot.Settings.TimeoutForRequest).DownloadString(uri));
                return token["result"].ToObject<T>();
            }
            catch (Exception exc)
            {
                if (Bot.Settings.ResponsesToConsole)
                {
                    Telesharp.Logger.Log(LogType.Warning, "TRequest", "Failed to get response.");
                }
                if (Bot.Settings.ExceptionsToConsole)
                {
                    Telesharp.Logger.Log(LogType.Info, "TRequest", "Exception: \n" + exc);
                }
                return default(T);
            }
        }

        private T SendTRequest<T>(string uri, Dictionary<string, string> parametrs) where T : class
        {
            try
            {
                var wc = new AdvancedWebClient(Bot.Settings.TimeoutForRequest);
                var token = JToken.Parse(wc.SendGET(uri, parametrs));
                return token["result"].ToObject<T>();
            }
            catch (WebException exc)
            {
                try
                {
                    if (exc.Response == null) return default(T);
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
                            Telesharp.Logger.Log(LogType.Error, "TRequest",  "Exception: \n" + exc2);
                        }
                    }
                }
            }
            catch (Exception exc)
            {
                if (Bot.Settings.InfoToConsole)
                {
                    Telesharp.Logger.Log(LogType.Warning, "TRequest", "Exception when trying to get response or parse JSON");
                    if (Bot.Settings.ExceptionsToConsole)
                    {
                        Telesharp.Logger.Log(LogType.Error, "TRequest", "Exception: \n" + exc);
                    }
                }
            }
            return default(T);
        }

        #endregion
    }
}