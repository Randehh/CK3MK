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

		private static GameModelService m_GameModelService = null;
		public static GameModelService GameModelService {
			get {
				if (m_GameModelService == null) {
					m_GameModelService = new GameModelService();
				}
				return m_GameModelService;
			}
		}
	}
}
