using Avalonia.Controls;
using CK3MK.Models;
using CK3MK.Services;
using CK3MK.Utilities;
using CK3MK.Views;
using CK3MK.Views.GameModels;
using CK3MK.Views.GameModels.Common;
using ReactiveUI;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Reactive;

namespace CK3MK.ViewModels {
	public class MainWindowViewModel : ViewModelBase {

		private MainWindow m_Window;

		//File
		public ReactiveCommand<Unit, Unit> OnCommand_New { get; }
		public ReactiveCommand<Unit, Unit> OnCommand_Open { get; }
		public ReactiveCommand<string, Unit> OnCommand_OpenRecent { get; }
		public ReactiveCommand<Unit, Unit> OnCommand_OpenLog { get; }

		//Windows
		public ReactiveCommand<Unit, Unit> OnCommand_Preferences { get; }
		public ReactiveCommand<Unit, Unit> OnCommand_DynastyEditor { get; }
		public ReactiveCommand<Unit, Unit> OnCommand_CharacterInspector { get; }

		public ObservableCollection<DynamicMenuItem> RecentProjectItems { get; set; } = new ObservableCollection<DynamicMenuItem>();

		public MainWindowViewModel(MainWindow window) {
			m_Window = window;

			OnCommand_New = ReactiveCommand.Create(CreateNewProject);
			OnCommand_Open = ReactiveCommand.Create(OpenProjectDialog);
			OnCommand_OpenRecent = ReactiveCommand.Create<string>(OpenProjectByPath);
			OnCommand_OpenLog = ReactiveCommand.Create(OpenLog);
			OnCommand_Preferences = ReactiveCommand.Create(OpenPreferences);
			OnCommand_DynastyEditor = ReactiveCommand.Create(OpenDynastyEditor);
			OnCommand_CharacterInspector = ReactiveCommand.Create(OpenCharacterInspector);

			m_Window.FindControl<MenuItem>("RecentProjectsMenuItem").ResourcesChanged += (sender, args) => { RefreshRecentProjects(); };
			RefreshRecentProjects();

			//Hook into services
			ServiceLocator.ProjectService.OnOpenProject += OpenProject;

			//Load existing data
			if (!string.IsNullOrWhiteSpace(ServiceLocator.GlobalSettingsService.DumpPath)) {
				ServiceLocator.GameModelService.LoadModelDump(ServiceLocator.GlobalSettingsService.DumpPath);
			}

			if (!string.IsNullOrWhiteSpace(ServiceLocator.GlobalSettingsService.BaseGameFilePath)) {
				ServiceLocator.GameModelService.LoadAllData();
			}
		}

		public void RefreshRecentProjects() {
			RecentProjectItems.Clear();
			foreach(string path in ServiceLocator.GlobalSettingsService.RecentProjects) {
				string[] displayNameSplit = path.Split(Path.DirectorySeparatorChar);
				int fileNameIndex = displayNameSplit.Length - 1;
				string displayName = displayNameSplit[fileNameIndex].Substring(0, displayNameSplit[fileNameIndex].Length - ".ck3mod".Length);
				RecentProjectItems.Add(new DynamicMenuItem() {
					Text = displayName,
					OnClicked = OnCommand_OpenRecent,
					CommandParameter = path,
				});
			}

			this.RaisePropertyChanged(nameof(RecentProjectItems));
		}

		private async void CreateNewProject() {
			NewProjectDialog newDialog = new NewProjectDialog();
			ModProject result = await newDialog.ShowDialog<ModProject>(m_Window);
			if(result != null) {
				ServiceLocator.ProjectService.RegisterNewProject(result);
				ServiceLocator.ProjectService.OpenProject(result);
			} 
		}

		private async void OpenProjectDialog() {
			FileDialogFilter filter = new FileDialogFilter() {
				Name = "CK3 Mod", Extensions = { "ck3mod" }
			};
			string[] result = await FileBrowserUtil.BrowseFileAsync(m_Window, filter);
			if(result != null && result.Length != 0) {
				OpenProjectByPath(result[0]);
			}
		}

		private void OpenLog() {
			new Process {
				StartInfo = new ProcessStartInfo(LoggingService.LogFilePath) {
					UseShellExecute = true
				}
			}.Start();
		}

		private async void OpenPreferences() {
			GlobalSettingsDialog newDialog = new GlobalSettingsDialog();
			await newDialog.ShowDialog(m_Window);
		}

		private void OpenProjectByPath(string path) {
			ServiceLocator.ProjectService.OpenProject(path);
		}

		private void OpenProject(object e, ModProject project) {
			string d = "Project loaded = " + project.Name;
		}

		private async void OpenDynastyEditor() {
			DynastyDialog newDialog = new DynastyDialog();
			await newDialog.ShowDialog(m_Window);
		}

		private async void OpenCharacterInspector() {
			CharacterDialog newDialog = new CharacterDialog();
			await newDialog.ShowDialog(m_Window);
		}
	}
}
