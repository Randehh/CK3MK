using Avalonia.Controls;
using System.Threading.Tasks;

namespace CK3MK.Utilities {
	public class FileBrowserUtil {
		
		public static async Task<string[]> BrowseFileAsync(Window parent, FileDialogFilter filter = null, bool allowMultiple = false) {
			OpenFileDialog dialog = new OpenFileDialog();
			dialog.Filters.Add(filter);
			dialog.Directory = @"C:\";
			dialog.AllowMultiple = allowMultiple;
			string[] result = await dialog.ShowAsync(parent);
			return result;			
		}

		public static async Task<string> BrowseFolderAsync(Window parent) {
			OpenFolderDialog dialog = new OpenFolderDialog();
			dialog.Directory = @"C:\";
			string result = await dialog.ShowAsync(parent);
			return result;
		}
	}
}
