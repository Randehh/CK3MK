using CK3MK.Utilities;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;

namespace CK3MK.Services {
	public class GameModelService {
		private Dictionary<string, GameTypes> m_Types = new Dictionary<string, GameTypes>();
		private Dictionary<string, string> m_GlobalFunctions = new Dictionary<string, string>();
		private Dictionary<string, string> m_GlobalPromotes = new Dictionary<string, string>();

		//Instances
		private Dictionary<string, ObservableCollection<Models.Game.History.Character>> m_Characters = new Dictionary<string, ObservableCollection<Models.Game.History.Character>>();

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
				ObservableCollection<Models.Game.History.Character> list = new ObservableCollection<Models.Game.History.Character>();

				AssetsUtil.ReadCK3ConfigFile(Path.Combine(charactersFolder, file),
					(key, depth) => {
						if(depth == 0) { // New character
							currentCharacter = new Models.Game.History.Character();
							currentCharacter.Id.SetValue(key);
						}
					},
					(key, value, depth) => {
						if (depth == 1) { // Base attributes
							switch (key) {
								case "name": currentCharacter.Name.SetValue(value); break;
								case "female": currentCharacter.Female.SetValue(value); break;
								case "dna": currentCharacter.Dna.SetValue(value); break;
								case "martial": currentCharacter.Martial.SetValue(value); break;
								case "diplomacy": currentCharacter.Diplomacy.SetValue(value); break;
								case "intrigue": currentCharacter.Intrigue.SetValue(value); break;
								case "stewardship": currentCharacter.Stewardship.SetValue(value); break;
								case "learning": currentCharacter.Learning.SetValue(value); break;
								case "father": currentCharacter.Father.SetValue(value); break;
								case "mother": currentCharacter.Mother.SetValue(value); break;
								case "disallow_random_traits": currentCharacter.DisallowRandomTraits.SetValue(value); break;
								case "religion": currentCharacter.Religion.SetValue(value); break;
								case "culture": currentCharacter.Culture.SetValue(value); break;
								case "dynasty": currentCharacter.Dynasty.SetValue(value); break;
								case "dynasty_house": currentCharacter.DynastyHouse.SetValue(value); break;
								case "give_nickname": currentCharacter.GiveNickname.SetValue(value); break;
								case "sexuality": currentCharacter.Sexuality.SetValue(value); break;
								case "health": currentCharacter.Health.SetValue(value); break;
								case "fertility": currentCharacter.Fertility.SetValue(value); break;
							}
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


				m_Characters.Add(fileName, list);
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
