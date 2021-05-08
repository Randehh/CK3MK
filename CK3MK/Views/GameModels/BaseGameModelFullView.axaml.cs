using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace CK3MK.Views.GameModels {
	public class BaseGameModelFullView : UserControl {
		public BaseGameModelFullView() {
			InitializeComponent();
		}

		private void InitializeComponent() {
			AvaloniaXamlLoader.Load(this);
		}
	}
}
