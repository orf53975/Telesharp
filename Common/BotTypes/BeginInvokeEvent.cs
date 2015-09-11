using System;
using Telesharp.Common.Interfaces;
using Telesharp.Common.Types;

namespace Telesharp.Common.BotTypes
{
    public class BeginInvokeEventArgs : EventArgs
    {
        public BeginInvokeEventArgs(ICommand command, Message message)
        {
            Command = command;
            Message = message;
            Refuse = false;
            RunSync = false;
        }

        /// <summary>
        ///     Message
        /// </summary>
        public Message Message { get; private set; }

        /// <summary>
        ///     Command
        /// </summary>
        public ICommand Command { get; set; }

        /// <summary>
        ///     Refuse the perform
        /// </summary>
        public bool Refuse { get; set; }

        /// <summary>
        ///     Run this command in one thread
        /// </summary>
        public bool RunSync { get; set; }
    }

    public delegate void BeginInvokeEventHandler(object sender, BeginInvokeEventArgs e);
}