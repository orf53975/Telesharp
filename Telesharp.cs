﻿using System;
using Telesharp.Common.TelesharpTypes;

namespace Telesharp
{
    public class Telesharp
    {
        private static ILogger _logger = new TelesharpLogger();

        public static ILogger Logger
        {
            get { return _logger; }
            set
            {
                if(value == null) throw new ArgumentNullException("value");
                _logger = value;
            }
        }

        internal static void InvokeBotDiedEvent(Bot bot, BotDiedEventArgs eventArgs)
        {
            BotDied(bot, eventArgs);
        }
        internal static void InvokeBotRunnedEvent(Bot bot, BotRunnedEventArgs eventArgs)
        {
            BotRunned(bot, eventArgs);
        }

        public static event BotDiedEventHandler BotDied = delegate { };
        public static event BotRunnedEventHandler BotRunned = delegate { };

    }
}
