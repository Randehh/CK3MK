using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CK3MK.ViewModels.History.GameModels;

namespace CK3MK.Views.GameModels.History {
	public class CharacterFullControl : UserControl {
		public CharacterFullControl() {
			InitializeComponent();
			DataContext = new CharacterFullControlVM(this);
		}

		private void InitializeComponent() {
			AvaloniaXamlLoader.Load(this);
		}
	}
}
