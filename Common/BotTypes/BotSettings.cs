using System;
using System.IO;
using System.Net;

namespace Telesharp.Common.BotTypes
{
    /// <summary>
    ///     Settings of the Bot
    /// </summary>
    [Serializable]
    public class BotSettings
    {
        private string _cacheDir;

        private int _checkUpdatesInterval;

        private bool _exceptionsToConsole;

        private int _maxThreadsForCommands;

        private string _name;

        private int _timeoutForRequest;

        private string _token;

        public BotSettings(string token, int updateInsterval)
        {
            _cacheDir = $"{Environment.CurrentDirectory}{Path.DirectorySeparatorChar}Cache{Path.DirectorySeparatorChar}";
            _checkUpdatesInterval = 500;
            _timeoutForRequest = 2000;
            _maxThreadsForCommands = -1;
            if (string.IsNullOrEmpty(token) || string.IsNullOrWhiteSpace(token))
            {
                throw new ArgumentNullException(nameof(token));
            }
            _token = token;
            _checkUpdatesInterval = updateInsterval;
            _maxThreadsForCommands = -1;
            _name = "bot" + new Random().Next(0, 999999);
            _exceptionsToConsole = true;
            GetProfile = true;
            InfoToConsole = true;
            RequestsToConsole = true;
            ResponsesToConsole = false;
            ExecuteCommandsSync = false;
            Proxy = null;
        }

        public BotSettings(string token) : this(token, 1000)
        {
        }

        /// <summary>
        ///     Interval for check updates
        /// </summary>
        public int CheckUpdatesInterval
        {
            get { return _checkUpdatesInterval; }
            set
            {
                if (value < 1)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                _checkUpdatesInterval = value;
            }
        }

        /// <summary>
        ///     Token of the bot
        /// </summary>
        public string Token
        {
            get { return _token; }
            set
            {
                if (string.IsNullOrEmpty(value) || string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentNullException(nameof(value));
                }
                _token = value;
            }
        }

        /// <summary>
        ///     Write all info to console
        /// </summary>
        public bool InfoToConsole { get; set; }


        /// <summary>
        ///     Write all exceptions to console
        /// </summary>
        public bool ExceptionsToConsole
        {
            get { return InfoToConsole && _exceptionsToConsole; }
            set { _exceptionsToConsole = value; }
        }


        /// <summary>
        ///     Write all requests to console
        /// </summary>
        public bool RequestsToConsole { get; set; }

        /// <summary>
        ///     Write all responses to console
        /// </summary>
        public bool ResponsesToConsole { get; set; }

        /// <summary>
        ///     Execute all commands in one thread?
        /// </summary>
        public bool ExecuteCommandsSync { get; set; }

        /// <summary>
        ///     Maximum count of thread for command
        /// </summary>
        public int MaximumThreadsForCommands
        {
            get { return _maxThreadsForCommands; }
            set
            {
                if (value != -1 && value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                _maxThreadsForCommands = value;
            }
        }


        /// <summary>
        ///     Directory of cache
        /// </summary>
        public string CacheDirectory
        {
            get { return _cacheDir; }
            set
            {
                var attr = File.GetAttributes(value);
                if (!attr.HasFlag(FileAttributes.Directory))
                {
                    throw new DirectoryNotFoundException();
                }
                _cacheDir = value;
            }
        }

        /// <summary>
        ///     Get profile when bot awakens?
        /// </summary>
        public bool GetProfile { get; set; }

        /// <summary>
        ///     Timeout to send request
        /// </summary>
        public int TimeoutForRequest
        {
            get { return _timeoutForRequest; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(value));
                }
                _timeoutForRequest = value;
            }
        }

        /// <summary>
        ///     Proxy for Bot (if your ip will be blocked for various reasons (ERRAR 500!11 U CAN'T DO ANYTHING11!))
        /// </summary>
        public WebProxy Proxy { get; set; }

        /// <summary>
        ///     Name for logger and thread
        /// </summary>
        public string Name
        {
            get { return _name; }
            set
            {
                if (value == null) throw new ArgumentNullException(nameof(value));
                _name = value;
            }
        }
    }
}