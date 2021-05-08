using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CK3MK.Views.GameModels {
	public class BaseGameModelFullCategorisedView : UserControl {
		public BaseGameModelFullCategorisedView() {
			InitializeComponent();
		}

		private void InitializeComponent() {
			AvaloniaXamlLoader.Load(this);
		}
	}
}
