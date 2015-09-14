using System;
using Telesharp.Common.TelesharpTypes;

namespace Telesharp
{
    public class TelesharpLogger : ILogger
    {
        private readonly string format = "[{0}] [{1}] [{2}]\t{3}";
        public void Log(string text)
        {
            Log(LogType.Info, text);
            //Console.WriteLine(format, GenerateTimeString(), "Info", "Telesharp", text);
        }

        public void Log(string title, string text)
        {
            Log(LogType.Info, title, text);
            //Console.WriteLine(format, GenerateTimeString(), "Info", title, text);
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
                    throw new ArgumentOutOfRangeException("logType", logType, null);
            }
            Console.WriteLine(format, GenerateTimeString(), logType, "Telesharp", text);
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
                    throw new ArgumentOutOfRangeException("logType", logType, null);
            }
            Console.WriteLine(format, GenerateTimeString(), logType, title, text);
            Console.ForegroundColor = ConsoleColor.White;
        }


        private string GenerateTimeString()
        {
            return string.Format("{0}.{1} {2}:{3}:{4}.{5}", DateTime.Now.Day, DateTime.Now.Month, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second, DateTime.Now.Millisecond);
        }
    }
}
