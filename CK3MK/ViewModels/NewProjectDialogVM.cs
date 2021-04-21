using CK3MK.Models;
using CK3MK.Utilities;
using CK3MK.Views;
using ReactiveUI;
using System.Collections.Generic;
using System.IO;
using System.Reactive;
using System.Threading.Tasks;

namespace CK3MK.ViewModels {
	public class NewProjectDialogVM : ViewModelBase {

		private NewProjectDialog m_Window;

		public ReactiveCommand<Unit, Unit> OnCommand_BrowseDestination { get; }
		public ReactiveCommand<Unit, Unit> OnCommand_CreateProject { get; }

		private string m_ModName = "";
		public string ModName {
			get => m_ModName;
			set {
				this.RaiseAndSetIfChanged(ref m_ModName, value);
				this.RaisePropertyChanged(nameof(CanCreateProject));
			}
		}

		private string m_Destination = "";
		public string Destination {
			get => m_Destination;
			set {
				this.RaiseAndSetIfChanged(ref m_Destination, value);
				this.RaisePropertyChanged(nameof(CanCreateProject));
			}
		}

		private bool m_TagAlternativeHistory = false;
		public bool TagAlternativeHistory {
			get => m_TagAlternativeHistory;
			set {
				this.RaiseAndSetIfChanged(ref m_TagAlternativeHistory, value);
				this.RaisePropertyChanged(nameof(CanCreateProject));
			}
		}

		private bool m_TagBalance = false;
		public bool TagBalance {
			get => m_TagBalance;
			set {
				this.RaiseAndSetIfChanged(ref m_TagBalance, value);
				this.RaisePropertyChanged(nameof(CanCreateProject));
			}
		}

		private bool m_TagBookmarks = false;
		public bool TagBookmarks {
			get => m_TagBookmarks;
			set {
				this.RaiseAndSetIfChanged(ref m_TagBookmarks, value);
				this.RaisePropertyChanged(nameof(CanCreateProject));
			}
		}

		private bool m_TagCharacterFocuses = false;
		public bool TagCharacterFocuses {
			get => m_TagCharacterFocuses;
			set {
				this.RaiseAndSetIfChanged(ref m_TagCharacterFocuses, value);
				this.RaisePropertyChanged(nameof(CanCreateProject));
			}
		}

		private bool m_TagCharacterInteractions = false;
		public bool TagCharacterInteractions {
			get => m_TagCharacterInteractions;
			set {
				this.RaiseAndSetIfChanged(ref m_TagCharacterInteractions, value);
				this.RaisePropertyChanged(nameof(CanCreateProject));
			}
		}

		private bool m_TagCulture = false;
		public bool TagCulture {
			get => m_TagCulture;
			set {
				this.RaiseAndSetIfChanged(ref m_TagCulture, value);
				this.RaisePropertyChanged(nameof(CanCreateProject));
			}
		}

		private bool m_TagDecisions = false;
		public bool TagDecisions {
			get => m_TagDecisions;
			set {
				this.RaiseAndSetIfChanged(ref m_TagDecisions, value);
				this.RaisePropertyChanged(nameof(CanCreateProject));
			}
		}

		private bool m_TagEvents = false;
		public bool TagEvents {
			get => m_TagEvents;
			set {
				this.RaiseAndSetIfChanged(ref m_TagEvents, value);
				this.RaisePropertyChanged(nameof(CanCreateProject));
			}
		}

		private bool m_TagFixes = false;
		public bool TagFixes {
			get => m_TagFixes;
			set {
				this.RaiseAndSetIfChanged(ref m_TagFixes, value);
				this.RaisePropertyChanged(nameof(CanCreateProject));
			}
		}

		private bool m_TagGameplay = false;
		public bool TagGameplay {
			get => m_TagGameplay;
			set {
				this.RaiseAndSetIfChanged(ref m_TagGameplay, value);
				this.RaisePropertyChanged(nameof(CanCreateProject));
			}
		}

		private bool m_TagGraphics = false;
		public bool TagGraphics {
			get => m_TagGraphics;
			set {
				this.RaiseAndSetIfChanged(ref m_TagGraphics, value);
				this.RaisePropertyChanged(nameof(CanCreateProject));
			}
		}

		private bool m_TagHistorical = false;
		public bool TagHistorical {
			get => m_TagHistorical;
			set {
				this.RaiseAndSetIfChanged(ref m_TagHistorical, value);
				this.RaisePropertyChanged(nameof(CanCreateProject));
			}
		}

		private bool m_TagMap = false;
		public bool TagMap {
			get => m_TagMap;
			set {
				this.RaiseAndSetIfChanged(ref m_TagMap, value);
				this.RaisePropertyChanged(nameof(CanCreateProject));
			}
		}

		private bool m_TagPortraits = false;
		public bool TagPortraits {
			get => m_TagPortraits;
			set {
				this.RaiseAndSetIfChanged(ref m_TagPortraits, value);
				this.RaisePropertyChanged(nameof(CanCreateProject));
			}
		}

		private bool m_TagReligion = false;
		public bool TagReligion {
			get => m_TagReligion;
			set {
				this.RaiseAndSetIfChanged(ref m_TagReligion, value);
				this.RaisePropertyChanged(nameof(CanCreateProject));
			}
		}

		private bool m_TagSchemes = false;
		public bool TagSchemes {
			get => m_TagSchemes;
			set {
				this.RaiseAndSetIfChanged(ref m_TagSchemes, value);
				this.RaisePropertyChanged(nameof(CanCreateProject));
			}
		}

		private bool m_TagSound = false;
		public bool TagSound {
			get => m_TagSound;
			set {
				this.RaiseAndSetIfChanged(ref m_TagSound, value);
				this.RaisePropertyChanged(nameof(CanCreateProject));
			}
		}

		private bool m_TagTotalConversion = false;
		public bool TagTotalConversion {
			get => m_TagTotalConversion;
			set {
				this.RaiseAndSetIfChanged(ref m_TagTotalConversion, value);
				this.RaisePropertyChanged(nameof(CanCreateProject));
			}
		}

		private bool m_TagTranslation = false;
		public bool TagTranslation {
			get => m_TagTranslation;
			set {
				this.RaiseAndSetIfChanged(ref m_TagTranslation, value);
				this.RaisePropertyChanged(nameof(CanCreateProject));
			}
		}

		private bool m_TagUtilities = false;
		public bool TagUtilities {
			get => m_TagUtilities;
			set {
				this.RaiseAndSetIfChanged(ref m_TagUtilities, value);
				this.RaisePropertyChanged(nameof(CanCreateProject));
			}
		}

		private bool m_TagWarfare = false;
		public bool TagWarfare {
			get => m_TagWarfare;
			set {
				this.RaiseAndSetIfChanged(ref m_TagWarfare, value);
				this.RaisePropertyChanged(nameof(CanCreateProject));
			}
		}

		public bool IsAnyTagSelected => GetTags().Count != 0;

		public List<string> GetTags() {
			List<string> tagList = new List<string>();
			if (TagAlternativeHistory) tagList.Add("Alternative History");
			if (TagBalance) tagList.Add("Balance");
			if (TagBookmarks) tagList.Add("Bookmarks");
			if (TagCharacterFocuses) tagList.Add("Character Focuses");
			if (TagCharacterInteractions) tagList.Add("Character Interactions");
			if (TagCulture) tagList.Add("Culture");
			if (TagDecisions) tagList.Add("Decisions");
			if (TagEvents) tagList.Add("Events");
			if (TagFixes) tagList.Add("Fixes");
			if (TagGameplay) tagList.Add("Gameplay");
			if (TagGraphics) tagList.Add("Graphics");
			if (TagHistorical) tagList.Add("Historical");
			if (TagMap) tagList.Add("Map");
			if (TagPortraits) tagList.Add("Portraits");
			if (TagReligion) tagList.Add("Religion");
			if (TagSchemes) tagList.Add("Schemes");
			if (TagSound) tagList.Add("Sound");
			if (TagTotalConversion) tagList.Add("Total Conversion");
			if (TagTranslation) tagList.Add("Translation");
			if (TagUtilities) tagList.Add("Utilities");
			if (TagWarfare) tagList.Add("Warfare");
			return tagList;
		}

		private bool m_CanCreateProject = false;
		public bool CanCreateProject {
			get {
				m_CanCreateProject = true;
				CanCreateProjectMessage = "Ready to create project.";

				if (string.IsNullOrEmpty(ModName)) {
					CanCreateProjectMessage = "Mod name is empty.";
					m_CanCreateProject = false;
				} else if (string.IsNullOrEmpty(Destination)) {
					CanCreateProjectMessage = "Destination is empty.";
					m_CanCreateProject = false;
				} else if (!Directory.Exists(Destination)) {
					CanCreateProjectMessage = "Destination is not a valid path.";
					m_CanCreateProject = false;
				} else if (!IsAnyTagSelected) {
					CanCreateProjectMessage = "No category is selected.";
					m_CanCreateProject = false;
				}

				this.RaisePropertyChanged(nameof(m_CanCreateProject));
				return m_CanCreateProject;
			}
		}

		private string m_CanCreateProjectMessage = "";
		public string CanCreateProjectMessage {
			get => m_CanCreateProjectMessage;
			set => this.RaiseAndSetIfChanged(ref m_CanCreateProjectMessage, value);
		}

		public NewProjectDialogVM(NewProjectDialog window) {
			m_Window = window;

			OnCommand_BrowseDestination = ReactiveCommand.CreateFromTask(BrowseDestination);
			OnCommand_CreateProject = ReactiveCommand.Create(CreateProject);
		}

		public async Task BrowseDestination() {
			string result = await FileBrowserUtil.BrowseFolderAsync(m_Window);
			if(result == null || result.Length == 0) {
				return;
			}
			Destination = result;
		}

		public void CreateProject() {
			ModProject project = new ModProject() {
				Name = ModName,
				DisplayName = ModName,
				Tags = GetTags(),
				Version = "1.0",
				Path = Destination,
			};
			m_Window.Close(project);
		}
	}
}
