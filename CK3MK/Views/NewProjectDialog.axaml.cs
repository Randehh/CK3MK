using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CK3MK.ViewModels;

namespace CK3MK.Views {
	public class NewProjectDialog : Window {
		public NewProjectDialog() {
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif

			DataContext = new NewProjectDialogVM(this);
		}

		private void InitializeComponent() {
			AvaloniaXamlLoader.Load(this);
		}
	}
}
