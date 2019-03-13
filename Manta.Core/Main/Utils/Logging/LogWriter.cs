/*
* Copyright © 2018-2019 Scott Sewell
* See "Licence.txt" for full licence.
*/

using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Manta.Logging
{
    /// <summary>
    /// Handles logging buffered output to file and debug console. Uses double buffering to reduce
    /// the time other threads might have to wait in case the message formatting on the writer
    /// thread is significant.
    /// </summary>
    internal class LogWriter
    {
        private readonly List<LogMessage> m_buffer = new List<LogMessage>();
        private readonly object m_bufferLock = new object();

        private readonly StringBuilder m_sb = new StringBuilder();
        private readonly object m_writeLock = new object();

        private readonly string m_filePath;

        /// <summary>
        /// Creates a new log writer and spawns a new thread to write from.
        /// </summary>
        /// <param name="outputFile">The log file to output to.</param>
        public LogWriter(FileInfo outputFile)
        {
            m_filePath = outputFile.FullName;

            // start a thread for the log write loop
            Task task = new Task(LogLoop, TaskCreationOptions.LongRunning);
            task.Start();
        }

        /// <summary>
        /// Loop that repeatedly checks the buffer for any new messages, and
        /// if any are found logs them.
        /// </summary>
        public void LogLoop()
        {
            List<LogMessage> toWrite = new List<LogMessage>();

            while (true)
            {
                // Aquire any buffered log messages
                lock (m_bufferLock)
                {
                    // release lock and sleep until there is content to consume
                    while (m_buffer.Count == 0)
                    {
                        Monitor.Wait(m_bufferLock);
                    }

                    toWrite.AddRange(m_buffer);
                    m_buffer.Clear();
                }

                // write the messages in the write buffer
                lock (m_writeLock)
                {
                    // build up the messages to log
                    foreach (LogMessage m in toWrite)
                    {
                        m.AppendTo(m_sb);
                    }
                    WriteText(m_sb);
                }

                // clear away written messages
                toWrite.Clear();
            }
        }

        /// <summary>
        /// Adds a message to the message buffer.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void BufferMessage(LogMessage message)
        {
            lock (m_bufferLock)
            {
                m_buffer.Add(message);
                // wake up writer thread since there is new content to consume
                Monitor.Pulse(m_bufferLock);
            }
        }

        /// <summary>
        /// Logs a message synchronously.
        /// </summary>
        /// <param name="message">The message to log.</param>
        public void WriteSynchronous(LogMessage message)
        {
            lock (m_bufferLock)
            {
                lock (m_writeLock)
                {
                    // flush any buffered messages first to preserve message ordering
                    foreach (LogMessage m in m_buffer)
                    {
                        m.AppendTo(m_sb);
                    }
                    m_buffer.Clear();

                    // add the new message to the end
                    message.AppendTo(m_sb);

                    // write all messages
                    WriteText(m_sb);
                }
            }
        }

        /// <summary>
        /// Writes text to the log file.
        /// </summary>
        /// <param name="sb">The text to write.</param>
        private void WriteText(StringBuilder sb)
        {
            string text = sb.ToString();
            sb.Clear();

#if DEBUG
            Debug.Print(text);
#endif
            using (StreamWriter stream = File.AppendText(m_filePath))
            {
                stream.Write(text);
            }
        }
    }
}
