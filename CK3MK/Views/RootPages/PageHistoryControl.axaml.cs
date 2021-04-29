using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CK3MK.ViewModels.RootPages;

namespace CK3MK.Views.RootPages {
	public class PageHistoryControl : UserControl {
		public PageHistoryControl() {
			InitializeComponent();

			DataContext = new PageHistoryControlVM(this);
		}

		private void InitializeComponent() {
			AvaloniaXamlLoader.Load(this);
		}

		public PageHistoryControlVM GetViewModel() {
			return DataContext as PageHistoryControlVM;
		}
	}
}
