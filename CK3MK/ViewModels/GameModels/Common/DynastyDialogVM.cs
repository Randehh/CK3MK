using CK3MK.Models.Game;
using CK3MK.Models.Game.Common;
using CK3MK.Services;
using CK3MK.Views.GameModels.Common;
using ReactiveUI;
using System.Collections.ObjectModel;

namespace CK3MK.ViewModels.GameModels.Common {
	public class DynastyDialogVM : ViewModelBase {

		private DynastyDialog m_Window;

		private ObservableCollection<SimpleGameModel<Dynasty>> m_Dynasties = new ObservableCollection<SimpleGameModel<Dynasty>>();
		public ObservableCollection<SimpleGameModel<Dynasty>> Dynasties {
			get => m_Dynasties;
			set {
				this.RaiseAndSetIfChanged(ref m_Dynasties, value);
				SelectedDynastyIndex = 0;
			}
		}

		private int m_SelectedDynastyIndex;
		public int SelectedDynastyIndex {
			get => m_SelectedDynastyIndex;
			set {
				this.RaiseAndSetIfChanged(ref m_SelectedDynastyIndex, value);
				SelectedDynasty = m_Dynasties[m_SelectedDynastyIndex].GetFullModel();
			}
		}

		private Dynasty m_SelectedDynasty;
		public Dynasty SelectedDynasty {
			get => m_SelectedDynasty;
			set => this.RaiseAndSetIfChanged(ref m_SelectedDynasty, value);
		}

		public DynastyDialogVM(DynastyDialog window) {
			m_Window = window;

			Dynasties = ServiceLocator.ModelCacheService.Dynasties.GetObservableCollection();
		}
	}
}
