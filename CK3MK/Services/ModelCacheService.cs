using CK3MK.Models.Game.Common;
using CK3MK.Models.Game.History;
using CK3MK.Utilities;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace CK3MK.Services {
	public class ModelCacheService {
		public void LoadAllData(bool forceReload = false) {
			if (m_IsLoaded && !forceReload) return;
			m_IsLoaded = true;
			LoadCharacters();
			LoadDynasties();
			LoadDynastyHouses();
		}

		private bool m_IsLoaded = false;

		public Dictionary<string, GameModelCache<Character>> CharactersByCountry { get; set; } = new Dictionary<string, GameModelCache<Character>>();
		public GameModelCache<Character> Characters { get; set; } = new GameModelCache<Character>();
		public GameModelCache<Dynasty> Dynasties { get; set; } = new GameModelCache<Dynasty>();
		public GameModelCache<DynastyHouse> DynastyHouses { get; set; } = new GameModelCache<DynastyHouse>();

		public ObservableCollection<string> Countries => new ObservableCollection<string>(CharactersByCountry.Keys);

		#region Data load		
		public void LoadCharacters() {
			string charactersFolder = GameModelPathUtil.Characters;
			AssetsUtil.LoadModelsFromFolder(Characters, charactersFolder, (character, weakReference) => {
				if (!CharactersByCountry.ContainsKey(character.FileSourceName)) {
					CharactersByCountry.Add(character.FileSourceName, new GameModelCache<Character>());
				}
				CharactersByCountry[character.FileSourceName].AddRaw(character.Id.Value, character.FileSourceName, weakReference);
			});
		}

		public void LoadDynasties() {
			string dynastiesFolder = GameModelPathUtil.Dynasties;
			AssetsUtil.LoadModelsFromFolder(Dynasties, dynastiesFolder);
		}

		public void LoadDynastyHouses() {
			string dynastyHousesFolder = GameModelPathUtil.DynastyHouses;
			AssetsUtil.LoadModelsFromFolder(DynastyHouses, dynastyHousesFolder);
		}
		#endregion
	}
}
