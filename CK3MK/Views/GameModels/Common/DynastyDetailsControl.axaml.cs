using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CK3MK.Views.GameModels.Common {
	public class DynastyDetailsControl : UserControl {
		public DynastyDetailsControl() {
			InitializeComponent();
		}

		private void InitializeComponent() {
			AvaloniaXamlLoader.Load(this);
		}
	}
}
