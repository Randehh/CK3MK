using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CK3MK.ViewModels.Generic;

namespace CK3MK.Views.Generic {
	public class MessageBoxDialog : Window {
		public MessageBoxDialog() {
			InitializeComponent();
#if DEBUG
			this.AttachDevTools();
#endif

			DataContext = new MessageBoxDialogVM(this);
		}

		public void SetMessage(string message) {
			(DataContext as MessageBoxDialogVM).MessageText = message;
		}

		private void InitializeComponent() {
			AvaloniaXamlLoader.Load(this);
		}
	}
}
