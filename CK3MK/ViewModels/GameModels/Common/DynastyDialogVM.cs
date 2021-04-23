using CK3MK.Models.Game.Common;
using CK3MK.Services;
using CK3MK.Views.GameModels.Common;
using ReactiveUI;
using System.Collections.ObjectModel;

namespace CK3MK.ViewModels.GameModels.Common {
	public class DynastyDialogVM : ViewModelBase {

		private DynastyDialog m_Window;


		private ObservableCollection<Dynasty> m_Dynasties = new ObservableCollection<Dynasty>();
		public ObservableCollection<Dynasty> Dynasties {
			get => m_Dynasties;
			set {
				this.RaiseAndSetIfChanged(ref m_Dynasties, value);
				SelectedDynasty = value[0];
			}
		}

		private Dynasty m_SelectedDynasty;
		public Dynasty SelectedDynasty {
			get => m_SelectedDynasty;
			set => this.RaiseAndSetIfChanged(ref m_SelectedDynasty, value);
		}

		public DynastyDialogVM(DynastyDialog window) {
			m_Window = window;

			Dynasties = new ObservableCollection<Dynasty>(ServiceLocator.GameModelService.GetDynasties());
		}
	}
}
