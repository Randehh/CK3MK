using System;
using System.Diagnostics;
using System.IO;

namespace CK3MK.Services {
	public class LoggingService {
		public static string LogFilePath => Path.Combine(GlobalSettingsService.RootFolder, "log.txt");

		private LogSeverity m_Severity = LogSeverity.Debug;
		private StreamWriter m_LogWriter;

		public LoggingService(LogSeverity severity) {
			if (File.Exists(LogFilePath)) {
				File.Delete(LogFilePath);
			}

			m_Severity = severity;
			m_LogWriter = File.CreateText(LogFilePath);
			m_LogWriter.AutoFlush = false;
		}

		~LoggingService() {
			m_LogWriter.Flush();
			m_LogWriter.Close();
			m_LogWriter.Dispose();
		}

		public void WriteLine(string s, LogSeverity severity) {
			if ((int)severity <= (int)m_Severity) {
				string output = $"{DateTime.Now.ToLongTimeString()} {GetSeverityTag(severity)} {s}";
				m_LogWriter.WriteLine(output);
				m_LogWriter.Flush();
			}
		}

		private string GetSeverityTag(LogSeverity severity) {
			switch (severity) {
				case LogSeverity.Error: return "[ERROR]";
				case LogSeverity.Critical: return "[Critical]";
				default: return "";
			}
		}

		public enum LogSeverity : int {
			Error = 0,
			Critical = 1,
			Normal = 2,
			Debug = 3,
		}
	}
}
