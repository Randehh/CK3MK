using CK3MK.Utilities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace CK3MK.Services {
	public class GameDumpService {
		private Dictionary<string, GameType> m_Types = new Dictionary<string, GameType>();
		private Dictionary<string, string> m_GlobalFunctions = new Dictionary<string, string>();
		private Dictionary<string, string> m_GlobalPromotes = new Dictionary<string, string>();

		public ObservableCollection<string> GameTypesKeys { get; set; } = new ObservableCollection<string>();
		public ObservableCollection<string> GlobalFunctionsKeys { get; set; } = new ObservableCollection<string>();
		public ObservableCollection<string> GlobalPromotesKeys { get; set; } = new ObservableCollection<string>();

		public GameType GetGameType(string t) {
			if (m_Types.ContainsKey(t)) {
				return m_Types[t];
			}
			return null;
		}

		public string GetFunctionReturnType(string functionName) {
			if (m_GlobalFunctions.ContainsKey(functionName)) {
				return m_GlobalFunctions[functionName];
			}
			return null;
		}

		public string GetGlobalPromoteType(string globalPromoteName) {
			if (m_GlobalPromotes.ContainsKey(globalPromoteName)) {
				return m_GlobalPromotes[globalPromoteName];
			}
			return null;
		}

		public bool LoadModelDump(string path) {
			if (!File.Exists(path)) return false;

			DumpCategories currentCategory = DumpCategories.Unknown;
			GameType currentType = null;

			bool success = AssetsUtil.ReadCK3ConfigFile(path,
				(key, _) => {
					if (currentCategory == DumpCategories.Unknown) {
						currentCategory = GetCategoryFromName(key);
					} else if (currentCategory == DumpCategories.Types) {
						currentType = new GameType() {
							Name = key,
						};
					}
				},
				(key, value, _) => {
					if (currentType != null) {
						currentType.AddParameter(key, value);
					} else {
						if (currentCategory == DumpCategories.GlobalFunctions) {
							m_GlobalFunctions.Add(key, value);
						} else if (currentCategory == DumpCategories.GlobalPromotes) {
							m_GlobalPromotes.Add(key, value);
						}
					}
				},
				(_) => {
					if (currentType != null) {
						m_Types.Add(currentType.Name, currentType);
						currentType = null;
					} else {
						currentCategory = DumpCategories.Unknown;
					}
				});

			GameTypesKeys = new ObservableCollection<string>(m_Types.Keys);
			GlobalFunctionsKeys = new ObservableCollection<string>(m_GlobalFunctions.Keys);
			GlobalPromotesKeys = new ObservableCollection<string>(m_GlobalPromotes.Keys);

			return success;
		}

		private DumpCategories GetCategoryFromName(string name) {
			if (name == "GlobalPromotes") return DumpCategories.GlobalPromotes;
			if (name == "GlobalFunctions") return DumpCategories.GlobalFunctions;
			if (name == "Types") return DumpCategories.Types;
			return DumpCategories.Unknown;
		}
	}

	public class GameType {
		public string Name { get; set; }
		private Dictionary<string, string> m_Parameters = new Dictionary<string, string>();

		public void AddParameter(string key, string val) {
			if (m_Parameters.ContainsKey(key)) return;
			m_Parameters.Add(key, val);
		}
	}

	public enum DumpCategories {
		Unknown,
		GlobalPromotes,
		GlobalFunctions,
		Types
	}
}
