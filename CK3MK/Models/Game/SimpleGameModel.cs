using CK3MK.Utilities;

namespace CK3MK.Models.Game {
    public class SimpleGameModel<T> where T : BaseGameModel {
        private GameModelCache<T> m_ParentCache;
        public string Id { get; set; }
        public string CacheId { get; set; }
        public string ListEntryName { get; set; }

        public SimpleGameModel(GameModelCache<T> parentCache, T model) {
            m_ParentCache = parentCache;
            Id = model.Id.Value;
            CacheId = model.CacheId;
            ListEntryName = model.GetListEntryName();
        }

        public T GetFullModel() {
            if (!m_ParentCache.ContainsKey(Id)) {
                return null;
            }
            return m_ParentCache.GetFullModel(Id);
        }
    }
}