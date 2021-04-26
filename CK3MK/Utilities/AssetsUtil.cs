using Avalonia;
using Avalonia.Platform;
using CK3MK.Models.Game;
using CK3MK.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;
using System.Text.Json;

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

		public static void ForEachFileInFolder(string path, Action<string, string, string> doFile) {
			foreach (string file in Directory.GetFiles(path)) {
				if (!file.EndsWith(".txt")) continue;
				string fileName = Path.GetFileName(file);
				string fileNameNoExtension = Path.GetFileNameWithoutExtension(file);
				doFile(file, fileName, fileNameNoExtension);
			}
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

		public static void LoadModelsFromFolder<T>(GameModelCache<T> cache, string path, Action<T, WeakReference> OnModelLoaded = null) where T : BaseGameModel {
			string modelName = typeof(T).Name;

			ForEachFileInFolder(path, (path, fileName, fileNameNoExtension) => {
				ServiceLocator.LoggingService.WriteLine($"=== Reading {modelName} file {fileName}... ===", LoggingService.LogSeverity.Debug);

				T currentModel = (T)Activator.CreateInstance(typeof(T), fileNameNoExtension);

				ReadCK3ConfigFile(path,
					(key, depth) => {
						if (depth == 0) { // New model
							currentModel = (T)Activator.CreateInstance(typeof(T), fileNameNoExtension);
							currentModel.Id.StringValue = key;
							ServiceLocator.LoggingService.WriteLine($"Starting new {modelName} with id {key}", LoggingService.LogSeverity.Debug);
						} else {
							string w = "wait";
						}
					},
					(key, value, depth) => {
						if (depth == 1) {
							currentModel.SetAttributeValue(key, value, true);
						} else {
							// Advanced attributes (???)
						}
						string spacing = "";
						for (int i = 0; i < depth; i++) {
							spacing += "    ";
						}
						ServiceLocator.LoggingService.WriteLine($"{spacing}Attribute on depth {depth}: {key} -> {value}", LoggingService.LogSeverity.Debug);
					},
					(depth) => {
						if (depth == 0) { // End of new model
							ServiceLocator.LoggingService.WriteLine($"Ending {modelName} with id {currentModel.Id.StringValue}\n", LoggingService.LogSeverity.Debug);

							if (cache.ContainsKey(currentModel.Id.StringValue)) {
								string firstFile = cache.GetSourceFile(currentModel.Id.StringValue);
								string currentFile = currentModel.FileSourceName;
								ServiceLocator.LoggingService.WriteLine($"Duplicate {modelName} found with id {currentModel.Id.StringValue}, original from {firstFile}, new from {currentFile}", LoggingService.LogSeverity.Error);
							} else {
								WeakReference reference = cache.AddModel(currentModel.Id.StringValue, currentModel);

								if(OnModelLoaded!= null) {
									OnModelLoaded(currentModel, reference);
								}
							}

							currentModel = null;
						}
					});

				ServiceLocator.LoggingService.WriteLine($"=== Finished {modelName} {fileName} ===\n", LoggingService.LogSeverity.Debug);
			});
		}
	}
}
