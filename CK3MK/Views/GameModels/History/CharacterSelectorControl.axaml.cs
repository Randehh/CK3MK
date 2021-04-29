using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CK3MK.Views.GameModels.History {
	public class CharacterSelectorControl : UserControl {
		public CharacterSelectorControl() {
			InitializeComponent();
		}

		private void InitializeComponent() {
			AvaloniaXamlLoader.Load(this);
		}
	}
}
