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
	}
}
