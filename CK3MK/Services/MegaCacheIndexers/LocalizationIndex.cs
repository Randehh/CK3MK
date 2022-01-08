using System;
using System.Collections.Generic;
using System.IO;

namespace CK3MK.Services.MegaCacheIndexers {
	public class LocalizationIndex : IMegaCacheIndex {

		private Dictionary<string, Dictionary<string, string>> m_LocalizationStrings = new Dictionary<string, Dictionary<string, string>>();

		public void Start(string path) {
			foreach (string languagePath in Directory.GetDirectories(path)) {
				string languageId = Path.GetFileNameWithoutExtension(languagePath);
				m_LocalizationStrings.Add(languageId, new Dictionary<string, string>());
				IndexLanguage(languagePath, languageId);
			}
		}

		public string GetLocalization(string tag, string languageId) {
			if (m_LocalizationStrings.ContainsKey(languageId) && m_LocalizationStrings[languageId].ContainsKey(tag)) {
				return m_LocalizationStrings[languageId][tag];
			}
			return tag;
		}

		private void IndexLanguage(string path, string languageId) {
			foreach (string file in Directory.GetFiles(path)) {
				if (!file.EndsWith(".yml")) continue;
				ReadFile(file, languageId);
			}

			foreach (string subfolder in Directory.GetDirectories(path)) {
				IndexLanguage(subfolder, languageId);
			}
		}

		private void ReadFile(string path, string languageId) {
			if (!File.Exists(path)) {
				return;
			}

			using (StreamReader sr = new StreamReader(path)) {
				while (sr.Peek() >= 0) {
					string currentLine = sr.ReadLine();
					if (currentLine == null || string.IsNullOrWhiteSpace(currentLine)) continue;

					if (currentLine.StartsWith($"l_{languageId}")) continue;

					currentLine = currentLine.Trim();
					if (currentLine.StartsWith("#")) continue; // Ignore comments

					string[] lineSplit = currentLine.Split(':', 2, StringSplitOptions.TrimEntries);
					string toInsert = lineSplit[1];
					if(toInsert.Length > 2) {
						toInsert = lineSplit[1].Substring(3, lineSplit[1].Length - 4);
					}
					m_LocalizationStrings[languageId][lineSplit[0]] = toInsert;
				}
			}
		}
	}
}
