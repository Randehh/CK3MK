using CK3MK.Models.Game;
using CK3MK.Models.Game.History;
using CK3MK.Services;

namespace CK3MK.Utilities.Converters {
	public class CharacterModelConverter : BaseModelConverter<Character> {
		public override SimpleGameModel<Character> GetSimpleModel(Character fullModel) {
			return ServiceLocator.ModelCacheService.Characters.GetSimpleModel(fullModel.Id.Value);
		}
	}
}
