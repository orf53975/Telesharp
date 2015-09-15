using Telesharp.Common.TelesharpTypes;

namespace Telesharp.Common.TelesharpInterfaces
{
    public interface ILogger
    {
        void Log(string text);
        void Log(string title, string text);
        void Log(LogType logType, string text);
        void Log(LogType logType, string title, string text);
    }
}