using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CK3MK.Views.GameModels.Attributes {
	public class GameModelAttributeStringControl : UserControl {
		public GameModelAttributeStringControl() {
			InitializeComponent();
		}

		private void InitializeComponent() {
			AvaloniaXamlLoader.Load(this);
		}
	}
}
