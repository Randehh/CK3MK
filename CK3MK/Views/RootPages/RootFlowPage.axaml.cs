using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CK3MK.ViewModels.RootPages;

namespace CK3MK.Views.RootPages {
	public class RootFlowPage : UserControl {
		public RootFlowPage() {
			InitializeComponent();

			DataContext = new RootFlowPageVM(this);
		}

		private void InitializeComponent() {
			AvaloniaXamlLoader.Load(this);
		}
	}
}
