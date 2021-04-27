using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CK3MK.ViewModels.RootPages;

namespace CK3MK.Views.RootPages {
	public class HomePage : UserControl {
		public HomePage() {
			InitializeComponent();

			DataContext = new HomePageVM(this);
		}

		private void InitializeComponent() {
			AvaloniaXamlLoader.Load(this);
		}
	}
}
