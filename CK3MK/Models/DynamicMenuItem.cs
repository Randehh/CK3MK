using System.Windows.Input;

namespace CK3MK.Models {
	public class DynamicMenuItem {
		public string Text { get; set; }
		public ICommand OnClicked { get; set; }
		public object CommandParameter { get; set; }
	}
}
