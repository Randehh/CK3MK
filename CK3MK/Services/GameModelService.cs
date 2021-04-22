using CK3MK.Utilities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;

namespace CK3MK.Services {
	public class GameModelService {
		private Dictionary<string, GameTypes> m_Types = new Dictionary<string, GameTypes>();
		private Dictionary<string, string> m_GlobalFunctions = new Dictionary<string, string>();
		private Dictionary<string, string> m_GlobalPromotes = new Dictionary<string, string>();

		//Instances
		private Dictionary<string, ObservableCollection<Models.Game.History.Character>> m_Characters = new Dictionary<string, ObservableCollection<Models.Game.History.Character>>();

		public List<string> GetCountries() {
			return new List<string>(m_Characters.Keys);
		}

		public ObservableCollection<Models.Game.History.Character> GetCharacters(string country) {
			if (!m_Characters.ContainsKey(country)) {
				return new ObservableCollection<Models.Game.History.Character>();
			}

			return m_Characters[country];
		}

		#region Model dump
		public bool LoadModelDump(string path) {
			if (!File.Exists(path)) return false;

			DumpCategories currentCategory = DumpCategories.Unknown;
			GameTypes currentType = null;

			bool success = AssetsUtil.ReadCK3ConfigFile(path,
				(key, _) => {
					if (currentCategory == DumpCategories.Unknown) {
						currentCategory = GetCategoryFromName(key);
					} else if (currentCategory == DumpCategories.Types) {
						currentType = new GameTypes() {
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

			return success;
		}

		private DumpCategories GetCategoryFromName(string name) {
			if (name == "Global Promotes") return DumpCategories.GlobalPromotes;
			if (name == "Global Functions") return DumpCategories.GlobalFunctions;
			if (name == "Types") return DumpCategories.Types;
			return DumpCategories.Unknown;
		}
		#endregion

		#region Data load
		public void LoadCharacters() {
			string charactersFolder = GameModelPathUtil.Characters;
			foreach (string file in Directory.GetFiles(charactersFolder)) {
				string fileName = file.Substring(charactersFolder.Length);
				fileName = fileName.Substring(1, fileName.Length - ".txt".Length - 1);
				Models.Game.History.Character currentCharacter = new Models.Game.History.Character();
				List<Models.Game.History.Character> list = new List<Models.Game.History.Character>();

				AssetsUtil.ReadCK3ConfigFile(Path.Combine(charactersFolder, file),
					(key, depth) => {
						if(depth == 0) { // New character
							currentCharacter = new Models.Game.History.Character();
							currentCharacter.Id.StringValue = key;
						}
					},
					(key, value, depth) => {
						if (depth == 1) {
							currentCharacter.SetAttributeValue(key, value);
						} else {
							// Advanced attributes (???)
						}
					},
					(depth) => {
						if (depth == 0) { // End of new character
							list.Add(currentCharacter);
							currentCharacter = null;
						}
					});

				m_Characters.Add(fileName, new ObservableCollection<Models.Game.History.Character>(list.OrderBy(character => character.Name.StringValue)));
			}
		}
		#endregion
	}

	public class GameTypes {
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
