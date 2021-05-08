using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CK3MK.Models.Game;
using CK3MK.ViewModels.GameModels;

namespace CK3MK.Views.GameModels {
	public class BaseGameModelView : UserControl {

		public static BaseGameModelView CreateForModel(string title, BaseGameModel model) {
			BaseGameModelView modelView = new BaseGameModelView();
			BaseGameModelVM vm = new BaseGameModelVM(modelView);
			vm.SetGameModel(title, model);
			modelView.DataContext = vm;
			return modelView;
		}

		public BaseGameModelView() {
			InitializeComponent();
			DataContext = new BaseGameModelVM(this);
		}

		private void InitializeComponent() {
			AvaloniaXamlLoader.Load(this);
		}
	}
}
