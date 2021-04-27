using CK3MK.Views.RootPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK3MK.ViewModels.RootPages {
	public class HomePageVM : ViewModelBase {

		private HomePage m_Control;

		public HomePageVM(HomePage control) {
			m_Control = control;
		}
	}
}
