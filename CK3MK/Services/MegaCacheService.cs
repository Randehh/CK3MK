using CK3MK.Services.MegaCacheIndexers;
using CK3MK.Utilities;
using System.Collections.Generic;
using System.IO;

namespace CK3MK.Services {
	public class MegaCacheService {

		private string[] m_FolderPriority = new string[] {
			"localization"
		};

		private Dictionary<string, IMegaCacheIndex> m_Indexers = new Dictionary<string, IMegaCacheIndex> {
			["localization"] = new LocalizationIndex(),
		};

		private List<string> m_IndexedFolders = new List<string>();
		private Dictionary<string, string> m_MegaCache = new Dictionary<string, string>();

		public void LoadData() {
			string baseFolder = GameModelPathUtil.RootFolder;
			foreach(string priorityFolder in m_FolderPriority) {
				IndexFolder(Path.Combine(baseFolder, priorityFolder), priorityFolder);
			}
			
			foreach(string subfolder in Directory.GetDirectories(baseFolder)) {
				IndexFolder(Path.Combine(baseFolder, subfolder), subfolder);
			}
		}

		private void IndexFolder(string folder, string category) {
			if (m_IndexedFolders.Contains(category)) return;

			if (m_Indexers.ContainsKey(category.ToLower())) {
				m_Indexers[category.ToLower()].Start(folder);
			}
			m_IndexedFolders.Add(category);
		}

		public string GetLocalizedString(string tag, string languageId = "english") {
			LocalizationIndex localizationIndexer = m_Indexers["localization"] as LocalizationIndex;
			if (tag.StartsWith("\"")) {
				tag = tag.Substring(1, tag.Length - 2);
			}
			return localizationIndexer?.GetLocalization(tag, languageId);
		}
	}
}
