using CK3MK.Models.Game;
using CK3MK.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Text.Json;
using static CK3MK.Models.Game.GameModelAttributes;

namespace CK3MK.Utilities {
	public class GameModelCache<T> where T : BaseGameModel {
        private Dictionary<string, WeakReference> m_Cache = new Dictionary<string, WeakReference>();
        private Dictionary<string, string> m_FileSources = new Dictionary<string, string>();

        private Dictionary<string, SimpleGameModel<T>> m_SimpleCache = new Dictionary<string, SimpleGameModel<T>>();

        public WeakReference AddModel(string id, T model) {
            WeakReference reference = new WeakReference(model, false);
            m_Cache.Add(id, reference);
            m_FileSources.Add(id, model.FileSourceName);
            m_SimpleCache.Add(id, CreateSimpleModel(model));
            SaveToCache(model);
            return reference;
        }

        public void AddRaw(string id, string fileSource, WeakReference reference) {
            m_Cache.Add(id, reference);
            m_FileSources.Add(id, fileSource);
            m_SimpleCache.Add(id, CreateSimpleModel(reference.Target as T));
        }

        public int Count {
            get { return m_Cache.Count; }
        }

        public ObservableCollection<SimpleGameModel<T>> GetSimpleModels() {
            return new ObservableCollection<SimpleGameModel<T>>(m_SimpleCache.Values);
		}

        public SimpleGameModel<T> GetSimpleModel(string id) {
			if (!m_SimpleCache.ContainsKey(id)) {
                return null;
			}
            return m_SimpleCache[id];
		}

        public T GetFullModel(string id) {
            T model = m_Cache[id].Target as T;
            if (model == null) {
                string fileSource = m_FileSources[id];
                model = (T)Activator.CreateInstance(typeof(T), fileSource);
                model.Id.Value = id;
                LoadFromCache(model);
                model.DoPostLinkAttributes();
                m_Cache[id].Target = model;
            }

            return model;
        }

        public bool ContainsKey(string id) {
            return m_Cache.ContainsKey(id);
		}

        public string GetSourceFile(string id) {
            return m_FileSources[id];
        }

        #region Full model caching functions
        private string CacheFolder => Path.Combine(GlobalSettingsService.RootFolder, "cache", typeof(T).Name);
        private string GetModelCachePath(T model) => Path.Combine(CacheFolder, model.CacheId + ".mc");

        private void SaveToCache(T model, bool overrideExisting = false) {
            EnsureFolder();

            string path = GetModelCachePath(model);
            if (File.Exists(path) && !overrideExisting) return;

            GameModelCacheModel cm = new GameModelCacheModel();
            cm.Attributes = new Dictionary<string, GameModelCacheModelAttribute>();
            foreach (IGameModelAttribute att in model.Attributes) {
                cm.Attributes.Add(att.Name, new GameModelCacheModelAttribute() {
                    Value = att.RawStringValue,
                    IsAssigned = att.IsAssigned,
                });
			}

            string json = JsonSerializer.Serialize(cm);
            File.WriteAllText(path, json);
        }

        private void LoadFromCache(T model) {
            EnsureFolder();

            string path = GetModelCachePath(model);
            if (!File.Exists(path)) return;

            string json = File.ReadAllText(path);
            GameModelCacheModel cm = JsonSerializer.Deserialize<GameModelCacheModel>(json);
            foreach(KeyValuePair<string, GameModelCacheModelAttribute> att in cm.Attributes) {
                model.SetAttributeValue(att.Key, att.Value.Value, att.Value.IsAssigned);
			}
        }

        private void EnsureFolder() {
			if (!Directory.Exists(CacheFolder)) {
                Directory.CreateDirectory(CacheFolder);
			}
		}

        private struct GameModelCacheModel {
            public Dictionary<string, GameModelCacheModelAttribute> Attributes { get; set; }
        }

        private struct GameModelCacheModelAttribute {
            public string Value { get; set; }
            public bool IsAssigned { get; set; }
		}
        #endregion

        #region Simple model caching functions
        public SimpleGameModel<T> CreateSimpleModel(T model) {
            SimpleGameModel<T> simpleModel = new SimpleGameModel<T>(this, model) {
                CacheId = model.CacheId,
                ListEntryName = model.GetListEntryName()
            };
            return simpleModel;
        }

        public ObservableCollection<SimpleGameModel<T>> GetObservableCollection() {
            ObservableCollection<SimpleGameModel<T>> collection = new ObservableCollection<SimpleGameModel<T>>();
            foreach (string key in m_Cache.Keys) {
                collection.Add(GetSimpleModel(key));
            }
            return collection;
        }
        #endregion
    }
}
