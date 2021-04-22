using Avalonia;
using Avalonia.Platform;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CK3MK.Utilities {
	public class AssetsUtil {

		public static Stream GetResource(string fileName) {
			IAssetLoader assets = AvaloniaLocator.Current.GetService<IAssetLoader>();
			Stream stream = assets.Open(new Uri(@$"avares://CK3MK/Assets/{fileName}"));
			return stream;
		}

		public static void UnzipResourceTo(string fileName, string destination) {
			Stream zipStream = GetResource(fileName);

			string zipPath = Path.Combine(destination, fileName);
			using (FileStream fileStream = File.Create(zipPath)) {
				zipStream.CopyTo(fileStream);
			}

			using (ZipArchive archive = ZipFile.OpenRead(zipPath)) {
				archive.ExtractToDirectory(destination);
			}

			File.Delete(zipPath);
		}

		public static void SerializeToJson<T>(T obj, string path, string fileName) where T : class {
			string jsonData = JsonSerializer.Serialize(obj);

			//Ensure folder exists
			if (!Directory.Exists(path)) {
				Directory.CreateDirectory(path);
			}

			string filePath = Path.Combine(path, fileName);
			File.WriteAllText(filePath, jsonData);
		}

		public static T DeserializeFromJson<T>(string path, string fileName) where T : class {
			return DeserializeFromJson<T>(Path.Combine(path, fileName));
		}

		public static T DeserializeFromJson<T>(string path) where T : class {
			string jsonString = File.ReadAllText(path);
			return JsonSerializer.Deserialize<T>(jsonString);
		}

		public static bool ReadCK3ConfigFile(string path, Action<string, int> onStartNewTable, Action<string, string, int> onAssign, Action<int> onEndTable) {
			if (!File.Exists(path)) {
				return false;
			}

			using (StreamReader sr = new StreamReader(path)) {
				string line;
				int depth = 0;

				string startTableSymbol = "= {";
				string assignSymbol = " -> ";
				string equalsSymbol = " = ";

				while ((line = sr.ReadLine()) != null) {
					line = line.Trim();
					if (line.StartsWith("#")) continue; // Ignore comments

					if (line.EndsWith(startTableSymbol)) {
						string keyName = line.Substring(0, line.Length - startTableSymbol.Length).Trim();
						onStartNewTable(keyName, depth);
						depth++;
					} else {
						if (line.Equals("}")) {
							depth--;
							onEndTable(depth);
						} else {
							string[] keyval = new string[0];
							if (line.Contains(assignSymbol)) {
								keyval = line.Split(assignSymbol);
							} else if (line.Contains(equalsSymbol)) {
								keyval = line.Split(equalsSymbol);
							}
							if (keyval.Length != 2) continue; // Ignore unknown

							string key = keyval[0];
							string val = keyval[1];
							if (val.Equals("[unregistered]")) continue; // Ignore unused values

							if (val.Contains("#")) {
								val = val.Substring(0, val.IndexOf("#") - 1);
							}
							onAssign(key, val, depth);
						}
					}
				}
			}
			return true;
		}
	}
}
