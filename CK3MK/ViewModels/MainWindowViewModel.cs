using CK3MK.Views;

namespace CK3MK.ViewModels {
	public class MainWindowViewModel : ViewModelBase {

		public static MainWindow WindowInstance { get; set; }

		public MainWindowViewModel(MainWindow window) {
			WindowInstance = window;
		}

		public void RegisterTaskBar(TaskBarVM taskBar) {

		}
	}
}
