using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace O365PWSync_SyncService
{
	static class Logger
	{
		public enum LogLevel
		{
			INFO,
			ERROR
		}

		private const string CONSOLE_LOG_FILE = @"C:\O365PWSync.log";

		public static bool Initialize()
		{
			try { System.IO.File.Create(CONSOLE_LOG_FILE).Close(); } catch (Exception) { }

			try
			{
				if (!EventLog.SourceExists("O365PWSync"))
				{
					EventLog.CreateEventSource("O365PWSync", "Application");
				}
			}
			catch (Exception ex)
			{
				try { System.IO.File.AppendAllText(CONSOLE_LOG_FILE, "Exception while initializing Logger: " + ex.Message); } catch (Exception) { }
				return false;
			}

			return true;
		}

		public static void Send(string message, LogLevel level, int eventID)
		{
			try { System.IO.File.AppendAllText(CONSOLE_LOG_FILE, message + Environment.NewLine); } catch (Exception) { } 

			using (EventLog eventLog = new EventLog("Application"))
			{
				eventLog.Source = "O365PWSync";

				EventLogEntryType type = EventLogEntryType.Error;
				if (level == LogLevel.ERROR) { type = EventLogEntryType.Error; }
				else if (level == LogLevel.INFO) { type = EventLogEntryType.Information; }

				eventLog.WriteEntry(message, type, eventID);
			}
		}
	}
}
