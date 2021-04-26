using Avalonia.Controls;
using CK3MK.Models.Game;
using CK3MK.Models.Game.History;
using CK3MK.Services;
using CK3MK.Views.GameModels;
using CK3MK.Views.GameModels.Attributes;
using ReactiveUI;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CK3MK.ViewModels.GameModels {
	public class CharacterDialogVM : ViewModelBase {

		private CharacterDialog m_Window;

		private List<GameModelAttributeControl> m_Attributes = new List<GameModelAttributeControl>();

		private ObservableCollection<string> m_Countries = new ObservableCollection<string>();
		public ObservableCollection<string> Countries {
			get => m_Countries;
			set {
				this.RaiseAndSetIfChanged(ref m_Countries, value);
				SelectedCountry = value[0];
			}
		}

		private ObservableCollection<SimpleGameModel<Character>> m_Characters = new ObservableCollection<SimpleGameModel<Character>>();
		public ObservableCollection<SimpleGameModel<Character>> Characters {
			get => m_Characters;
			set {
				this.RaiseAndSetIfChanged(ref m_Characters, value);
				if (m_Characters.Count != 0) {
					SelectedCharacter = m_Characters[0].GetFullModel();
				} else {
					SelectedCharacter = null;
				}
			}
		}

		private string m_SelectedCountry = "";
		public string SelectedCountry {
			get => m_SelectedCountry;
			set {
				this.RaiseAndSetIfChanged(ref m_SelectedCountry, value);
				Characters = ServiceLocator.ModelCacheService.CharactersByCountry[value].GetSimpleModels();
			}
		}

		private int m_SelectedCharacterIndex;
		public int SelectedCharacterIndex {
			get => m_SelectedCharacterIndex;
			set {
				this.RaiseAndSetIfChanged(ref m_SelectedCharacterIndex, value);
				SelectedCharacter = Characters[m_SelectedCharacterIndex].GetFullModel();
			}
		}

		private Character m_SelectedCharacter = null;
		public Character SelectedCharacter {
			get => m_SelectedCharacter;
			set {
				if (m_SelectedCharacter != null) {
					m_SelectedCharacter.OnModelChanged -= UpdateCharacterData;
				}

				this.RaiseAndSetIfChanged(ref m_SelectedCharacter, value);

				if(m_SelectedCharacter != null) {
					m_SelectedCharacter.OnModelChanged += UpdateCharacterData;
				}
			}
		}

		public CharacterDialogVM(CharacterDialog window) {
			m_Window = window;

			Countries = new ObservableCollection<string>(ServiceLocator.ModelCacheService.Countries);
			StackPanel attributeStackPanel = m_Window.FindControl<StackPanel>("AttributeStackPanel");
			foreach(IControl c in attributeStackPanel.Children) {
				m_Attributes.Add(c as GameModelAttributeControl);
			}
		}

		private void UpdateCharacterData() {
			this.RaisePropertyChanged(nameof(SelectedCharacter));
		}

	}
}
