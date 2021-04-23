using System.IO;

namespace CK3MK.Services {
	public class LoggingService {
		public static string LogFilePath => Path.Combine(GlobalSettingsService.RootFolder, "log.txt");

		private StreamWriter m_LogWriter;

		public LoggingService() {
			if (File.Exists(LogFilePath)) {
				File.Delete(LogFilePath);
			}

			m_LogWriter = File.CreateText(LogFilePath);
		}

		~LoggingService() {
			m_LogWriter.Dispose();
		}

		public void WriteLine(string s) {
			m_LogWriter.WriteLine(s);
		}
	}
}
