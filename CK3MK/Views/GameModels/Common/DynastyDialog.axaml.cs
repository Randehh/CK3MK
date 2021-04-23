using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CK3MK.ViewModels.GameModels;
using CK3MK.ViewModels.GameModels.Common;

namespace CK3MK.Views.GameModels.Common {
	public class DynastyDialog : Window {
		public DynastyDialog() {
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif

			DataContext = new DynastyDialogVM(this);
		}

		private void InitializeComponent() {
			AvaloniaXamlLoader.Load(this);
		}
	}
}
