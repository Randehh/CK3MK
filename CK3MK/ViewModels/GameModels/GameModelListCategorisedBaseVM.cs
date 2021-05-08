using Avalonia.Controls;
using CK3MK.Models.Game;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;

namespace CK3MK.ViewModels.GameModels {
	public class GameModelListCategorisedBaseVM<T> : GameModelListBaseVM<T> where T : BaseGameModel {

		public static GameModelListCategorisedBaseVM<T> Create(IControl owner, ObservableCollection<string> categories, Func<string, ObservableCollection<SimpleGameModel<T>>> getModelsInCategory) {
			GameModelListCategorisedBaseVM<T> vm = new GameModelListCategorisedBaseVM<T>(owner);
			vm.getModelsInCategory = getModelsInCategory;
			vm.Categories = categories;
			return vm;
		}

		public GameModelListCategorisedBaseVM(IControl ownerControl) : base(ownerControl) { }

		public Func<string, ObservableCollection<SimpleGameModel<T>>> getModelsInCategory = null;

		private string m_CategoryName = "Category";
		public string CategoryName {
			get => m_CategoryName;
			set => this.RaiseAndSetIfChanged(ref m_CategoryName, value);
		}

		private ObservableCollection<string> m_Categories = new ObservableCollection<string>();
		public ObservableCollection<string> Categories {
			get => m_Categories;
			set {
				this.RaiseAndSetIfChanged(ref m_Categories, value);
				SelectedCategory = value[0];
			}
		}

		private string m_SelectedCategory = "";
		public string SelectedCategory {
			get => m_SelectedCategory;
			set {
				this.RaiseAndSetIfChanged(ref m_SelectedCategory, value);
				Models = GetModelsForCategory();
			}
		}

		public ObservableCollection<SimpleGameModel<T>> GetModelsForCategory() {
			if(getModelsInCategory == null) {
				throw new Exception("getModelsInCategory is not set, make sure to create an instance of this class via the static Create method.");
			}
			return getModelsInCategory(SelectedCategory);
		}
	}
}
