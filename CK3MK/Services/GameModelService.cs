using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK3MK.Services {
	public class GameModelService {
		private Dictionary<string, GameTypes> m_Types = new Dictionary<string, GameTypes>();
		private Dictionary<string, string> m_GlobalFunctions = new Dictionary<string, string>();
		private Dictionary<string, string> m_GlobalPromotes = new Dictionary<string, string>();

		public bool LoadModelDump(string path) {
			if (!File.Exists(path)) return false;

			using (StreamReader sr = new StreamReader(path)) {
				string line;
				DumpCategories currentCategory = DumpCategories.Unknown;
				GameTypes currentType = null;

				string startTableSymbol = "= {";
				string assignSymbol = " -> ";

				while ((line = sr.ReadLine()) != null) {
					line = line.Trim();
					if (line.EndsWith(startTableSymbol)) {
						string keyName = line.Substring(0, line.Length - startTableSymbol.Length).Trim();
						if (currentCategory == DumpCategories.Unknown) {
							currentCategory = GetCategoryFromName(keyName);
						} else if(currentCategory == DumpCategories.Types) {
							currentType = new GameTypes() {
								Name = keyName,
							};
						}
					} else {
						if (line.Equals("}")) {
							if (currentType != null) {
								m_Types.Add(currentType.Name, currentType);
								currentType = null;
							} else {
								currentCategory = DumpCategories.Unknown;
							}
						} else if(line.Contains(assignSymbol)) {
							string[] keyval = line.Split(assignSymbol);
							if (keyval[1].Equals("[unregistered]")) continue; // Ignore unused values

							if (currentType != null) {
								currentType.AddParameter(keyval[0], keyval[1]);
							} else {
								if(currentCategory == DumpCategories.GlobalFunctions) {
									m_GlobalFunctions.Add(keyval[0], keyval[1]);
								} else if(currentCategory == DumpCategories.GlobalPromotes) {
									m_GlobalPromotes.Add(keyval[0], keyval[1]);
								}
							}
						}
					}
				}
			}

			return true;
		}

		private DumpCategories GetCategoryFromName(string name) {
			if (name == "Global Promotes") return DumpCategories.GlobalPromotes;
			if (name == "Global Functions") return DumpCategories.GlobalFunctions;
			if (name == "Types") return DumpCategories.Types;
			return DumpCategories.Unknown;
		}
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
