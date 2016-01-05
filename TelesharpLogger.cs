using System;
using Telesharp.Common.TelesharpInterfaces;
using Telesharp.Common.TelesharpTypes;

namespace Telesharp
{
	public class TelesharpLogger : ILogger
	{
		private const string Format = "[{0}] [{1}] [{2}]\t{3}";

		public void Log(string text)
		{
			Log(LogType.Info, text);
		}

		public void Log(string title, string text)
		{
			Log(LogType.Info, title, text);
		}

		public void Log(LogType logType, string text)
		{
			switch (logType)
			{
				case LogType.Info:
					Console.ForegroundColor = ConsoleColor.Cyan;
					break;
				case LogType.Warning:
					Console.ForegroundColor = ConsoleColor.DarkYellow;
					break;
				case LogType.Error:
					Console.ForegroundColor = ConsoleColor.Red;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(logType), logType, null);
			}
			Console.WriteLine(Format, GenerateTimeString(), logType, "Telesharp", text);
			Console.ForegroundColor = ConsoleColor.White;
		}

		public void Log(LogType logType, string title, string text)
		{
			switch (logType)
			{
				case LogType.Info:
					Console.ForegroundColor = ConsoleColor.Cyan;
					break;
				case LogType.Warning:
					Console.ForegroundColor = ConsoleColor.DarkYellow;
					break;
				case LogType.Error:
					Console.ForegroundColor = ConsoleColor.Red;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(logType), logType, null);
			}
			Console.WriteLine(Format, GenerateTimeString(), logType, title, text);
			Console.ForegroundColor = ConsoleColor.White;
		}


		private static string GenerateTimeString()
		{
			return
				$"{DateTime.Now.Day}.{DateTime.Now.Month} {DateTime.Now.Hour.ToString("00")}:{DateTime.Now.Minute.ToString("00")}:{DateTime.Now.Second.ToString("00")}.{DateTime.Now.Millisecond.ToString("000")}";
		}
	}
}