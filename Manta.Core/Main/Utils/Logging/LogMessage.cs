/*
* Copyright © 2018-2019 Scott Sewell
* See "Licence.txt" for full licence.
*/

using System;
using System.Linq;
using System.Text;

namespace Manta.Logging
{
    /// <summary>
    /// Stores the details of a log message.
    /// </summary>
    internal struct LogMessage
    {
        /// <summary>
        /// The format of the date in each log line.
        /// </summary>
        private static readonly string DATE_FORMAT = "yyyy/MM/dd HH:mm:ss";

        private static readonly string[] LEVEL_NAMES = Enum.GetNames(typeof(LogLevel));
        private static readonly int MESSAGE_START_PADDING;

        static LogMessage()
        {
            MESSAGE_START_PADDING = DATE_FORMAT.Length + LEVEL_NAMES.Max(x => x.Length) + 4;
        }

        private readonly LogLevel m_level;
        private readonly string m_message;
        private readonly string m_stackTrace;

        public LogMessage(LogLevel level, string message, string stackTrace = null)
        {
            m_level = level;
            m_message = message;
            m_stackTrace = stackTrace;
        }

        public void AppendTo(StringBuilder sb)
        {
            int initialLength = sb.Length;

            sb.Append(DateTime.Now.ToString(DATE_FORMAT));
            sb.Append(" [");
            sb.Append(LEVEL_NAMES[(int)m_level]);
            sb.Append("]");

            // right pad the line start
            int lineLength = sb.Length - initialLength;
            sb.Append(' ', Math.Max(0, MESSAGE_START_PADDING - lineLength));

            sb.AppendLine(m_message);

            if (m_stackTrace != null)
            {
                sb.AppendLine(m_stackTrace);
            }
        }
    }
}
