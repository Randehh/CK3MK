using Avalonia.Controls;
using CK3MK.Models.Game;
using CK3MK.Views.GameModels;
using CK3MK.Views.GameModels.Attributes;
using ReactiveUI;
using static CK3MK.Models.Game.GameModelAttributes;

namespace CK3MK.ViewModels.GameModels {
	public class BaseGameModelVM : ViewModelBase {

		private BaseGameModelView m_Control;

		private string m_ViewName = "";
		public string ViewName {
			get => m_ViewName;
			set => this.RaiseAndSetIfChanged(ref m_ViewName, value);
		}

		public BaseGameModelVM(BaseGameModelView control) {
			m_Control = control;
		}

		public void SetGameModel(string title, BaseGameModel model) {
			ViewName = title;
			StackPanel attributeStackPanel = m_Control.FindControl<StackPanel>("AttributeStackPanel");
			if(attributeStackPanel.Children.Count != 0) {
				return;
			}

			foreach(IGameModelAttribute attribute in model.Attributes) {
				GameModelAttributeControl attributeControl = new GameModelAttributeControl() { DataContext = attribute };
				attributeStackPanel.Children.Add(attributeControl);
			}
		}
	}
}
