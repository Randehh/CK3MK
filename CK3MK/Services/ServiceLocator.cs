using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK3MK.Services {
	public static class ServiceLocator {
		private static ProjectService m_ProjectService = null;
		public static ProjectService ProjectService {
			get {
				if(m_ProjectService == null) {
					m_ProjectService = new ProjectService();
				}
				return m_ProjectService;
			}
		}

		private static GlobalSettingsService m_GlobalSettingsService = null;
		public static GlobalSettingsService GlobalSettingsService {
			get {
				if(m_GlobalSettingsService == null) {
					m_GlobalSettingsService = GlobalSettingsService.Load();
				}
				return m_GlobalSettingsService;
			}
		}

		private static MegaCacheService m_MegaCacheService = null;
		public static MegaCacheService MegaCacheService {
			get {
				if (m_MegaCacheService == null) {
					m_MegaCacheService = new MegaCacheService();
				}
				return m_MegaCacheService;
			}
		}

		private static ModelCacheService m_ModelCacheService = null;
		public static ModelCacheService ModelCacheService {
			get {
				if (m_ModelCacheService == null) {
					m_ModelCacheService = new ModelCacheService();
				}
				return m_ModelCacheService;
			}
		}

		private static GameDumpService m_GameDumpService = null;
		public static GameDumpService GameDumpService {
			get {
				if (m_GameDumpService == null) {
					m_GameDumpService = new GameDumpService();
				}
				return m_GameDumpService;
			}
		}

		private static LoggingService m_LoggingService = null;
		public static LoggingService LoggingService {
			get {
				if (m_LoggingService == null) {
					m_LoggingService = new LoggingService(LoggingService.LogSeverity.Debug);
				}
				return m_LoggingService;
			}
		}
	}
}
