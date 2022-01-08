using CK3MK.Services;
using System.IO;

namespace CK3MK.Utilities {
	public static class GameModelPathUtil {
		public static string RootFolder = ServiceLocator.GlobalSettingsService.BaseGameFilePath;

		//Secondary root folders
		public static string Localization => Path.Combine(RootFolder, "localization");
		public static string Common => Path.Combine(RootFolder, "common");
		public static string History => Path.Combine(RootFolder, "history");

		//Common folders
		public static string Dynasties => Path.Combine(Common, "dynasties");
		public static string DynastyHouses => Path.Combine(Common, "dynasty_houses");

		//History folders
		public static string Characters => Path.Combine(History, "characters");
	}
}
