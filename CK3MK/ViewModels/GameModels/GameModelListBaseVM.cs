using Avalonia.Controls;
using CK3MK.Models.Game;
using CK3MK.Views.GameModels;
using CK3MK.Views.GameModels.Attributes;
using ReactiveUI;
using System.Collections.ObjectModel;
using static CK3MK.Models.Game.GameModelAttributes;

namespace CK3MK.ViewModels.GameModels {
	public class GameModelListBaseVM<T> : ViewModelBase where T : BaseGameModel {
		public static GameModelListBaseVM<T> CreateForModel(IControl owner, ObservableCollection<SimpleGameModel<T>> models, T selectedModel = null) {
			GameModelListBaseVM<T> vm = new GameModelListBaseVM<T>(owner);
			vm.Models = models;
			if (selectedModel != null) vm.SelectedModel = selectedModel;
			return vm;
		}

		private IControl m_Owner;

		public GameModelListBaseVM(IControl ownerControl) {
			m_Owner = ownerControl;
		}

		private ObservableCollection<SimpleGameModel<T>> m_Models = new ObservableCollection<SimpleGameModel<T>>();
		public ObservableCollection<SimpleGameModel<T>> Models {
			get => m_Models;
			set {
				this.RaiseAndSetIfChanged(ref m_Models, value);
				SelectedModelIndex = 0;
			}
		}

		private int m_SelectedModelIndex;
		public int SelectedModelIndex {
			get => m_SelectedModelIndex;
			set {
				this.RaiseAndSetIfChanged(ref m_SelectedModelIndex, value);
				SelectedModel = Models[m_SelectedModelIndex].GetFullModel();
			}
		}

		private T m_SelectedModel;
		public T SelectedModel {
			get => m_SelectedModel;
			set {
				this.RaiseAndSetIfChanged(ref m_SelectedModel, value);
				SetGameModel(m_SelectedModel);
			}
		}

		public void SetGameModel(BaseGameModel model) {
			StackPanel attributeStackPanel = m_Owner.FindControl<BaseGameModelView>("DetailsView").FindControl<StackPanel>("AttributeStackPanel");
			attributeStackPanel.Children.Clear();
			foreach (IGameModelAttribute attribute in model.Attributes) {
				GameModelAttributeControl attributeControl = new GameModelAttributeControl() { DataContext = attribute };
				attributeStackPanel.Children.Add(attributeControl);
			}
		}
	}
}
