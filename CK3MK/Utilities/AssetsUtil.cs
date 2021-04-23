using Avalonia;
using Avalonia.Platform;
using CK3MK.Services;
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
				bool isComment = false;
				bool checkStartTable = false;

				HashSet<int> charsToIgnore = new HashSet<int>() {
					'\t',	//Tabs
					' ',	//Spaces
					'\r',	//Carriage return
				};
				int newLineChar = '\n';
				char[] startTableSymbol = new char[2] { '=', '{' };
				int endTableSymbol = '}';
				string assignSymbol = "->";
				string equalsSymbol = "=";
				char commentSymbol = '#';

				StringBuilder currentLine = new StringBuilder();

				while (sr.Peek() >= 0) {
					int raw = sr.Read();
					char c = (char)raw;

					if (charsToIgnore.Contains(raw)) continue;

					if (checkStartTable && !isComment) { // New table, call new table action
						if (c == startTableSymbol[1]) {
							line = currentLine.ToString();
							currentLine.Clear();
							onStartNewTable(line, depth);
							depth++;
							checkStartTable = false;
							continue;
						} else if(c == newLineChar) { // Read through newlines in this state
							continue;
						} else {
							currentLine.Append(startTableSymbol[0]); // Add withheld character
							checkStartTable = false;
						}
					}

					if (c == newLineChar) { // End line, call assign action
						isComment = false;
						line = currentLine.ToString();
						if (string.IsNullOrWhiteSpace(line)) continue;

						currentLine.Clear();

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

						onAssign(key, val, depth);
					} else if (c == endTableSymbol) { // End table, call end table action
						if (!isComment) {
							depth--;
							onEndTable(depth);
						}
					} else {
						if(c == commentSymbol) {
							isComment = true;
						}

						if (!isComment) {
							if (c == startTableSymbol[0]) {
								checkStartTable = true;
								continue;
							}
						
							currentLine.Append(c);
						}
					}
				}
			}
			return true;
		}
	}
}
