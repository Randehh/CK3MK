using CK3MK.Models.Game;
using CK3MK.Models.Game.Common;
using CK3MK.Services;
using CK3MK.Views.GameModels.Common;
using ReactiveUI;
using System.Collections.ObjectModel;

namespace CK3MK.ViewModels.GameModels.Common {
	public class DynastyHouseFullControlVM : ViewModelBase {

		private DynastyHouseFullControl m_Window;

		private ObservableCollection<SimpleGameModel<DynastyHouse>> m_DynastyHouses = new ObservableCollection<SimpleGameModel<DynastyHouse>>();
		public ObservableCollection<SimpleGameModel<DynastyHouse>> DynastyHouses {
			get => m_DynastyHouses;
			set {
				this.RaiseAndSetIfChanged(ref m_DynastyHouses, value);
				SelectedDynastyHouseIndex = 0;
			}
		}

		private int m_SelectedDynastyHouseIndex;
		public int SelectedDynastyHouseIndex {
			get => m_SelectedDynastyHouseIndex;
			set {
				this.RaiseAndSetIfChanged(ref m_SelectedDynastyHouseIndex, value);
				SelectedDynastyHouse = m_DynastyHouses[m_SelectedDynastyHouseIndex].GetFullModel();
			}
		}

		private DynastyHouse m_SelectedDynastyHouse;
		public DynastyHouse SelectedDynastyHouse {
			get => m_SelectedDynastyHouse;
			set => this.RaiseAndSetIfChanged(ref m_SelectedDynastyHouse, value);
		}

		public DynastyHouseFullControlVM(DynastyHouseFullControl window) {
			m_Window = window;

			DynastyHouses = ServiceLocator.ModelCacheService.DynastyHouses.GetObservableCollection();
		}
	}
}
