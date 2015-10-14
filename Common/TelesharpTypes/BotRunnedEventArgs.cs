using System;

namespace Telesharp.Common.TelesharpTypes
{
    public class BotRunnedEventArgs
    {
        public BotRunnedEventArgs()
        {
            Status = BotRunnedState.Normal;
        }

        public BotRunnedEventArgs(Exception exception)
        {
            if (exception == null) throw new ArgumentNullException(nameof(exception));
            Status = BotRunnedState.Error;
            Exception = exception;
        }

        public BotRunnedState Status { get; private set; }
        public Exception Exception { get; set; }
    }
}