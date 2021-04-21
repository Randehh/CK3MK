using Avalonia.Controls;
using CK3MK.Services;
using CK3MK.Utilities;
using CK3MK.Views;
using CK3MK.Views.Generic;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reactive;
using System.Text;
using System.Threading.Tasks;

namespace CK3MK.ViewModels {
	public class GlobalSettingsDialogVM : ViewModelBase {

		private GlobalSettingsDialog m_Window;

		public ReactiveCommand<Unit, Unit> OnCommand_BrowseBaseGameFiles { get; }
		public ReactiveCommand<Unit, Unit> OnCommand_ParseGameDump { get; }

		private string m_BaseGameFilePath = ServiceLocator.GlobalSettingsService.BaseGameFilePath;
		public string BaseGameFilePath {
			get => m_BaseGameFilePath;
			set {
				this.RaiseAndSetIfChanged(ref m_BaseGameFilePath, value);
				ServiceLocator.GlobalSettingsService.BaseGameFilePath = value;
				ServiceLocator.GlobalSettingsService.Save();
			}
		}

		public GlobalSettingsDialogVM(GlobalSettingsDialog window) {
			m_Window = window;

			OnCommand_BrowseBaseGameFiles = ReactiveCommand.Create(BrowseBaseGameFiles);
			OnCommand_ParseGameDump = ReactiveCommand.Create(ParseGameDump);
		}

		private async void BrowseBaseGameFiles() {
			string result = await FileBrowserUtil.BrowseFolderAsync(m_Window);
			if(!string.IsNullOrWhiteSpace(result)) {
				string errorMessage = "";
				if (!VerifyBaseGamesFolder(result, out errorMessage)) {
					MessageBoxDialog messageDialog = new MessageBoxDialog();
					messageDialog.SetMessage(errorMessage);
					await messageDialog.ShowDialog(m_Window);
					return;
				}

				BaseGameFilePath = result;
			}
		}

		private bool VerifyBaseGamesFolder(string path, out string errorMessage) {
			List<string> foldersToCheck = new List<string>() {
				"common",
				"content_source",
				"events",
				"fonts",
				"gfx",
				"gui",
				"history",
				"localization",
				"map_data",
				"music",
				"sound",
				"tests",
				"tools",
				"tweakergui_assets"
			};

			foreach(string folder in foldersToCheck) {
				if(!Directory.Exists(Path.Combine(path, folder))) {
					errorMessage = "Not all folders are found, please select the correct game folder.";
					return false;
				}
			}
			errorMessage = "All good.";
			return true;
		}

		private async void ParseGameDump() {
			string[] result = await FileBrowserUtil.BrowseFileAsync(m_Window, new FileDialogFilter() {
				Name = "Log file",
				Extensions = { "log" }
			});

			ServiceLocator.GameModelService.LoadModelDump(result[0]);
		}
	}
}
