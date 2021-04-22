	using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

namespace CK3MK.Models.Game {
	public class GameModelCollection<T> where T : BaseGameModel {
		private Dictionary<string, T> m_ById = new Dictionary<string, T>();
		public ObservableCollection<T> Collection { get; set; } = new ObservableCollection<T>();

		public void AddModel(T model) {
			if (m_ById.ContainsKey(model.Id.StringValue)) {
				return;
			}
			m_ById.Add(model.Id.StringValue, model);
			Collection.Add(model);
			Collection.OrderBy(character => model.Name.StringValue);
		}

		public T GetById(string id) {
			if (m_ById.ContainsKey(id)) {
				return m_ById[id];
			}
			return null;
		}

		public void FinalizeCollection() {
			foreach(T model in Collection) {
				model.DoPostLinkAttributes();
			}
		}
	}
}
