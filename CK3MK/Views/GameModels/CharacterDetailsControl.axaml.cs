using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CK3MK.Views.GameModels {
	public class CharacterDetailsControl : UserControl {
		public CharacterDetailsControl() {
			InitializeComponent();
		}

		private void InitializeComponent() {
			AvaloniaXamlLoader.Load(this);
		}
	}
}
