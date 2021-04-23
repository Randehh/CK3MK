using CK3MK.Models.Game;
using CK3MK.Models.Game.Common;
using CK3MK.Models.Game.History;
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
		private Dictionary<string, ObservableCollection<Character>> m_CharactersByCountry = new Dictionary<string, ObservableCollection<Character>>();
		private Dictionary<string, Character> m_AllCharacters = new Dictionary<string, Character>();
		private Dictionary<string, Dynasty> m_Dynasties = new Dictionary<string, Dynasty>();

		public List<string> GetCountries() {
			return new List<string>(m_CharactersByCountry.Keys);
		}

		public ObservableCollection<Character> GetCharacters() {
			return new ObservableCollection<Character>(m_AllCharacters.Values);
		}

		public ObservableCollection<Character> GetCharacters(string country) {
			if (!m_CharactersByCountry.ContainsKey(country)) {
				return new ObservableCollection<Character>();
			}

			return m_CharactersByCountry[country];
		}

		public Character GetCharacter(string id) {
			if (!m_AllCharacters.ContainsKey(id)) {
				return null;
			}
			return m_AllCharacters[id];
		}

		public ObservableCollection<Dynasty> GetDynasties() {
			return new ObservableCollection<Dynasty>(m_Dynasties.Values);
		}

		public Dynasty GetDynasty(string id) {
			if (!m_Dynasties.ContainsKey(id)) {
				return null;
			}
			return m_Dynasties[id];
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
		public void LoadAllData() {
			LoadCharacters();
			LoadDynasties();
			PostLoadLink();
		}
		
		public void LoadCharacters() {
			string charactersFolder = GameModelPathUtil.Characters;
			m_AllCharacters = AssetsUtil.LoadModelsFromFolder<Character>(charactersFolder, (character) => {
				if (!m_CharactersByCountry.ContainsKey(character.FileSourceName)) {
					m_CharactersByCountry.Add(character.FileSourceName, new ObservableCollection<Character>());
				}
				m_CharactersByCountry[character.FileSourceName].Add(character);
			});
		}
		
		public void LoadDynasties() {
			string dynastiesFolder = GameModelPathUtil.Dynasties;
			m_Dynasties = AssetsUtil.LoadModelsFromFolder<Dynasty>(dynastiesFolder);
		}

		private void PostLoadLink() {
			foreach (Character c in m_AllCharacters.Values) c.DoPostLinkAttributes();
			foreach (Dynasty d in m_Dynasties.Values) d.DoPostLinkAttributes();
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
