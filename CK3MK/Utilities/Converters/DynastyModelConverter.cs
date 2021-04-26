using CK3MK.Models.Game;
using CK3MK.Models.Game.Common;
using CK3MK.Services;

namespace CK3MK.Utilities.Converters {
	public class DynastyModelConverter : BaseModelConverter<Dynasty> {
		public override SimpleGameModel<Dynasty> GetSimpleModel(Dynasty fullModel) {
			return ServiceLocator.ModelCacheService.Dynasties.GetSimpleModel(fullModel.Id.Value);
		}
	}
}
