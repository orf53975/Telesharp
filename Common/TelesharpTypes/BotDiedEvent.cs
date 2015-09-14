using System;

namespace Telesharp.Common.TelesharpTypes
{

    public delegate void BotDiedEventHandler(object sender, BotDiedEventArgs e);

    public class BotDiedEventArgs : EventArgs
    {
        public BotDiedEventArgs(Exception exception)
        {
            if (exception == null) throw new ArgumentNullException("exception");
            ExitType = BotExitType.Error;
            Exception = exception;
        }

        public BotDiedEventArgs()
        {
            ExitType = BotExitType.Normal;
            Exception = null;
        }

        public BotExitType ExitType { get; private set; }
        public Exception Exception { get; private set;  }
    }
    
}
