using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using CK3MK.ViewModels;

namespace CK3MK.Views {
	public class TaskBar : UserControl {
		public TaskBar() {
			InitializeComponent();

			DataContext = new TaskBarVM(this);
		}

		private void InitializeComponent() {
			AvaloniaXamlLoader.Load(this);
		}

		protected override void OnAttachedToVisualTree(VisualTreeAttachmentEventArgs e) {
			base.OnAttachedToVisualTree(e);
			(DataContext as TaskBarVM).RegisterTaskBar();
		}
	}
}
