using System;

namespace Telesharp.Common.TelesharpTypes
{
	public class BotDiedEventArgs : EventArgs
	{
		public BotDiedEventArgs(Exception exception)
		{
			if (exception == null) throw new ArgumentNullException(nameof(exception));
			ExitType = BotExitType.Error;
			Exception = exception;
		}

		public BotDiedEventArgs()
		{
			ExitType = BotExitType.Normal;
			Exception = null;
		}

		public BotExitType ExitType { get; private set; }
		public Exception Exception { get; private set; }
	}
}