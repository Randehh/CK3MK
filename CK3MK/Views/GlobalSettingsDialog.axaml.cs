using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CK3MK.ViewModels;

namespace CK3MK.Views {
	public class GlobalSettingsDialog : Window {
		public GlobalSettingsDialog() {
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif

			DataContext = new GlobalSettingsDialogVM(this);
		}

		private void InitializeComponent() {
			AvaloniaXamlLoader.Load(this);
		}
	}
}
