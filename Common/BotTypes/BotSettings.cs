using System;
using System.IO;

namespace Telesharp.Common.BotTypes
{
    /// <summary>
    ///     Settings of the Bot
    /// </summary>
    public class BotSettings
    {
        private string _cacheDir = Environment.CurrentDirectory + Path.DirectorySeparatorChar + "Cache" +
                                   Path.DirectorySeparatorChar;

        private int _checkUpdatesInterval = 500;

        private int _maxThreadsForCommands = -1;

        private int _timeoutForRequest = 2000;

        private bool _exceptionsToConsole;

        private string _token = "";

        public BotSettings(string token, int updateInsterval)
        {
            Token = token;
            CheckUpdatesInterval = updateInsterval;
            MaximumThreadsForCommands = -1;
            _name = "bot" + (new Random()).Next(0, 999999);
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
                    throw new ArgumentOutOfRangeException("value");
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
                    throw new ArgumentNullException("value");
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
                    throw new ArgumentOutOfRangeException("value");
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
        ///     Timeout to send request
        /// </summary>
        public int TimeoutForRequest
        {
            get { return _timeoutForRequest; }
            set
            {
                if (value <= 0)
                {
                    throw new ArgumentOutOfRangeException("value");
                }
                _timeoutForRequest = value;
            }
        }


        private string _name;
        public string Name
        {
            get { return _name; }
            set
            {
                if (value == null) throw new ArgumentNullException("value");
                _name = value;
            }
        }
    }
}