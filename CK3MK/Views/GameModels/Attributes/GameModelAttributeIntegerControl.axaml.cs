using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CK3MK.Views.GameModels.Attributes {
	public class GameModelAttributeIntegerControl : UserControl {
		public GameModelAttributeIntegerControl() {
			InitializeComponent();
		}

		private void InitializeComponent() {
			AvaloniaXamlLoader.Load(this);
		}
	}
}
