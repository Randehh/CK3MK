using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CK3MK.Views.Generic {
	public class TextBlockWithLink : UserControl {
		public TextBlockWithLink() {
			InitializeComponent();
		}

		private void InitializeComponent() {
			AvaloniaXamlLoader.Load(this);
		}
	}
}
