using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CK3MK.ViewModels;

namespace CK3MK.Views {
	public class MainWindow : Window {
		public MainWindow() {
			InitializeComponent();
#if DEBUG
            this.AttachDevTools();
#endif

			DataContext = new MainWindowViewModel(this);
		}

		private void InitializeComponent() {
			AvaloniaXamlLoader.Load(this);
		}

		public void RegisterTaskBar(TaskBarVM taskBar) {
			(DataContext as MainWindowViewModel).RegisterTaskBar(taskBar);
		}
	}
}
