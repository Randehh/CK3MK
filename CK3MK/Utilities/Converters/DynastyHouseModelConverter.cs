using CK3MK.Models.Game;
using CK3MK.Models.Game.Common;
using CK3MK.Services;

namespace CK3MK.Utilities.Converters {
	public class DynastyHouseModelConverter : BaseModelConverter<DynastyHouse> {
		public override SimpleGameModel<DynastyHouse> GetSimpleModel(DynastyHouse fullModel) {
			return ServiceLocator.ModelCacheService.DynastyHouses.GetSimpleModel(fullModel.Id.Value);
		}
	}
}
