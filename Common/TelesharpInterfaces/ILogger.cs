using Telesharp.Common.TelesharpTypes;

namespace Telesharp.Common.TelesharpInterfaces
{
    /// <summary>
    ///     Telesharp logger
    /// </summary>
    public interface ILogger
    {
        /// <summary>
        ///     Write something in log
        /// </summary>
        /// <param name="text">Text</param>
        /// <example>Text example: [1.1.1970] [Ghost] [INFO] Run operation, please wait...</example>
        void Log(string text);

        /// <summary>
        ///     Write something in log with name
        /// </summary>
        /// <param name="title">Caller name</param>
        /// <param name="text">Text</param>
        /// <example>Text example: [1.1.1970] [Some guy] [INFO] Run operation, please wait...</example>
        void Log(string title, string text);

        /// <summary>
        ///     Write something in log with type (WARNING!!! TRAGEDY!!11 AAa)
        /// </summary>
        /// <param name="logType">Type of logged event</param>
        /// <param name="text">Text</param>
        /// <example>Text example: [1.1.1970] [Ghost] [WARNING] Running of operation canceled</example>
        void Log(LogType logType, string text);

        /// <summary>
        ///     Write something in log with title, type
        /// </summary>
        /// <param name="logType">Type of logged event</param>
        /// <param name="title">Caller name</param>
        /// <param name="text">Text</param>
        /// <example>Text example: [1.1.1970] [Some guy] [WARNING] Running of operation canceled</example>
        void Log(LogType logType, string title, string text);
    }
}