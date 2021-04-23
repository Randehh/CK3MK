using CK3MK.Utilities;
using System;
using System.Collections.Generic;
using System.IO;

namespace CK3MK.Services {
	public class GlobalSettingsService {

		public static string RootFolder => Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "CK3MK");
		private static string SettingsFile => Path.Combine(RootFolder, "Settings.json");

		public static GlobalSettingsService Load() {
			if (!File.Exists(SettingsFile)) {
				return new GlobalSettingsService();
			}

			return AssetsUtil.DeserializeFromJson<GlobalSettingsService>(SettingsFile);
		}

		public void Save() {
			AssetsUtil.SerializeToJson(this, RootFolder, "Settings.json");
		}

		#region Settings
		public string BaseGameFilePath { get; set; } = "";
		public string DumpPath { get; set; } = "";

		/// <summary>
		/// Project paths for recently opened projects
		/// </summary>
		public List<string> RecentProjects { get; set; } = new List<string>();

		public void AddRecentProjectPath(string path) {
			if (RecentProjects.Contains(path)) {
				RecentProjects.Remove(path);
			}
			RecentProjects.Insert(0, path);
			Save();
		}
		#endregion
	}
}
