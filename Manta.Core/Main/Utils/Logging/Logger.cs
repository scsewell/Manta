/*
* Copyright © 2018-2019 Scott Sewell
* See "Licence.txt" for full licence.
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Diagnostics;
using Manta.Logging;

namespace Manta
{
    /// <summary>
    /// Gets log messages and sends them to the log writer. It is thread safe,
    /// but writes are briefly buffered so in event of a fatal crash output may
    /// not be complete.
    /// </summary>
    public class Logger : Singleton<Logger>
    {
        /// <summary>
        /// The directory of the log files relative to the application launch directory.
        /// </summary>
        private static readonly string LOG_DIRECTORY = "logs";

        /// <summary>
        /// The formatting of the log date in the file name.
        /// </summary>
        private static readonly string LOG_DATE_FORMAT = "yyyy-MM-dd_HH-mm-ss";

        /// <summary>
        /// The file extention used for log files.
        /// </summary>
        private static readonly string FILE_EXTENTION = ".log";

        /// <summary>
        /// The maximum number of logs before the oldest will be automatically removed.
        /// </summary>
        private const int MAX_LOG_COUNT = 30;

        /// <summary>
        /// If true logged messages are written to file on another thread. This helps with 
        /// performance, but messages may not be written out after a crash.
        /// </summary>
        private const bool WRITE_ASNYCHRONOUSLY = true;

        private readonly LogWriter m_writer;
        private readonly FileInfo m_logInfo;

        /// <summary>
        /// Constructor.
        /// </summary>
        public Logger()
        {
            // Create a directory for the log files beside the exe if it does not already exist
            if (!Directory.Exists(LOG_DIRECTORY))
            {
                Directory.CreateDirectory(LOG_DIRECTORY);
            }

            // Limit the number of previous logs stored by deleting the oldest
            List<FileInfo> logs = new DirectoryInfo(LOG_DIRECTORY).GetFiles('*' + FILE_EXTENTION).ToList();
            logs.Sort((x, y) => (x.CreationTime.CompareTo(y.CreationTime)));

            while (logs.Count >= MAX_LOG_COUNT)
            {
                logs[0].Delete();
                logs.RemoveAt(0);
            }

            // Get the filepath for this session's log
            string logStartTime = DateTime.Now.ToString(LOG_DATE_FORMAT);
            string logName = logStartTime + FILE_EXTENTION;

            m_logInfo = new FileInfo(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, LOG_DIRECTORY, logName));

            // create the log writer
            m_writer = new LogWriter(m_logInfo);
        }

        /// <summary>
        /// Logs the given object.
        /// </summary>
        /// <param name="o">An object to print.</param>
        /// <param name="printStackTrace">Prints the stack trace.</param>
        public static void Info(object o)
        {
            Info(o.ToString());
        }

        /// <summary>
        /// Logs the given object.
        /// </summary>
        /// <param name="o">An object to print.</param>
        /// <param name="printStackTrace">Prints the stack trace.</param>
		[Conditional("DEBUG")]
        public static void Debug(object o)
        {
            Debug(o.ToString());
        }

        /// <summary>
        /// Logs the given object.
        /// </summary>
        /// <param name="o">An object to print.</param>
        /// <param name="printStackTrace">Prints the stack trace.</param>
        public static void Warning(object o, bool printStackTrace = false)
        {
            Warning(o.ToString(), printStackTrace);
        }

        /// <summary>
        /// Logs the given object.
        /// </summary>
        /// <param name="o">An object to print.</param>
        /// <param name="printStackTrace">Prints the stack trace.</param>
        public static void Error(object o, bool printStackTrace = true)
        {
            Error(o.ToString(), printStackTrace);
        }

        /// <summary>
        /// Logs the given info message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="printStackTrace">Prints the stack trace.</param>
        public static void Info(string message)
        {
            Instance.LogMessage(message, LogLevel.Info, false);
        }

        /// <summary>
        /// Logs the given debug message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="printStackTrace">Prints the stack trace.</param>
		[Conditional("DEBUG")]
        public static void Debug(string message)
        {
            Instance.LogMessage(message, LogLevel.Debug, false);
        }

        /// <summary>
        /// Logs the given warning message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        /// <param name="printStackTrace">Prints the stack trace.</param>
        public static void Warning(string message, bool printStackTrace = false)
        {
            Instance.LogMessage(message, LogLevel.Warning, printStackTrace);
        }

        /// <summary>
        /// Logs the given error message.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public static void Error(string message, bool printStackTrace = true)
        {
            Instance.LogMessage(message, LogLevel.Error, printStackTrace);
        }

        /// <summary>
        /// Logs the given exception.
        /// </summary>
        /// <param name="message">The exception to log.</param>
        public static void Exception(object exception)
        {
            // ToString on exeption objects typically includes the stack trace, so we don't need to include it
            Instance.LogMessage(exception.ToString(), LogLevel.Error, false);
        }

        private static readonly string[] NEW_LINES = new string[] { Environment.NewLine };

        /// <summary>
        /// Formats a message to and sends it to the writer.
        /// </summary>
        /// <param name="message">The message content.</param>
        /// <param name="logLevel">The mesasge type.</param>
        /// <param name="showStackTrace">If true includes a stack trace.</param>
        private void LogMessage(string content, LogLevel level, bool showStackTrace)
        {
            // Include a stack trace if desired
            string stackTrace = null;
            if (showStackTrace)
            {
                string[] lines = Environment.StackTrace.Split(NEW_LINES, StringSplitOptions.RemoveEmptyEntries);

                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < lines.Length; i++)
                {
                    // Don't include the function calls in the logger in the stack trace, as it is not useful
                    if (i > 2 && !lines[i].Contains(typeof(Logger).FullName))
                    {
                        sb.AppendLine(lines[i]);
                    }
                }
                stackTrace = sb.ToString();
            }

            LogMessage message = new LogMessage(level, content, stackTrace);

            // Don't write error messages asynchronously as that helps ensure the message is
            // still logged in case of a fatal error.
            if (level != LogLevel.Error && WRITE_ASNYCHRONOUSLY)
            {
                m_writer.BufferMessage(message);
            }
            else
            {
                m_writer.WriteSynchronous(message);
            }
        }
    }
}
