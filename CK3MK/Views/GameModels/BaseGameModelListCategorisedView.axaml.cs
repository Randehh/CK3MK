using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CK3MK.Views.GameModels {
	public class BaseGameModelListCategorisedView : UserControl {

		public BaseGameModelListCategorisedView() {
			InitializeComponent();
		}

		private void InitializeComponent() {
			AvaloniaXamlLoader.Load(this);
		}
	}
}
