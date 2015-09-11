using System;
using Telesharp.Common.Types;

namespace Telesharp.Common.BotTypes
{
    public class ParseMessageEventArgs : EventArgs
    {
        public ParseMessageEventArgs(Message message)
        {
            Message = message;
        }

        public Message Message { get; private set; }
    }

    public delegate void ParseMessageEventHandler(object sender, ParseMessageEventArgs e);
}