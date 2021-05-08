using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CK3MK.Models.Game;
using CK3MK.ViewModels.GameModels;

namespace CK3MK.Views.GameModels {
	public class BaseGameModelListView : UserControl {

		public BaseGameModelListView() {
			InitializeComponent();
		}

		private void InitializeComponent() {
			AvaloniaXamlLoader.Load(this);
		}
	}
}
