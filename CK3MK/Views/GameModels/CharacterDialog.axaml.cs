using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CK3MK.ViewModels.GameModels;

namespace CK3MK.Views.GameModels {
	public class CharacterDialog : Window {
		public CharacterDialog() {
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif

			DataContext = new CharacterDialogVM(this);
		}

		private void InitializeComponent() {
			AvaloniaXamlLoader.Load(this);
		}
	}
}
