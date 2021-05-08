
using CK3MK.Models.Game;
using CK3MK.ViewModels.GameModels;
using CK3MK.Views.GameModels;
using System;
using System.Collections.ObjectModel;

namespace CK3MK.Utilities {
	public static class GameModelViewCreator {

		public static BaseGameModelFullView CreateFullView<T>(ObservableCollection<SimpleGameModel<T>> models) where T : BaseGameModel {
			BaseGameModelFullView view = new BaseGameModelFullView();
			view.DataContext = GameModelListBaseVM<T>.CreateForModel(view, models);
			return view;
		}

		public static BaseGameModelFullCategorisedView CreateFullViewCategorised<T>(ObservableCollection<string> categories, Func<string, ObservableCollection<SimpleGameModel<T>>> getModelsInCategory) where T : BaseGameModel {
			BaseGameModelFullCategorisedView view = new BaseGameModelFullCategorisedView();
			view.DataContext = GameModelListCategorisedBaseVM<T>.Create(view, categories, getModelsInCategory);
			return view;
		}

		public static BaseGameModelListView CreateListView<T>(ObservableCollection<SimpleGameModel<T>> models) where T : BaseGameModel {
			BaseGameModelListView view = new BaseGameModelListView();
			view.DataContext = GameModelListBaseVM<T>.CreateForModel(view, models);
			return view;
		}

		public static BaseGameModelListCategorisedView CreateListViewCategorised<T>(ObservableCollection<string> categories, Func<string, ObservableCollection<SimpleGameModel<T>>> getModelsInCategory) where T : BaseGameModel {
			BaseGameModelListCategorisedView view = new BaseGameModelListCategorisedView();
			view.DataContext = GameModelListCategorisedBaseVM<T>.Create(view, categories, getModelsInCategory);
			return view;
		}

		public static BaseGameModelView CreateDetailsView<T>(string title, T model) where T : BaseGameModel {
			return BaseGameModelView.CreateForModel(title, model);
		}
	}
}
