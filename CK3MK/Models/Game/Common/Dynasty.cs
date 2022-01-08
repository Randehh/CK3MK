using CK3MK.Services;
using static CK3MK.Models.Game.GameModelAttributes;

namespace CK3MK.Models.Game.Common {
	public class Dynasty : BaseGameModel {

		public GameModelAttributeString Name { get; set; }
		public GameModelAttributeString Culture { get; set; }
		public GameModelAttributeString Prefix { get; set; }

		public Dynasty(string fileName) : base(fileName) {
			Name = RegisterAttribute(new GameModelAttributeString(this, "Name", true));
			Culture = RegisterAttribute(new GameModelAttributeString(this, "Culture"));
			Prefix = RegisterAttribute(new GameModelAttributeString(this, "Prefix"));
		}

		public override string GetListEntryName() {
			if (string.IsNullOrWhiteSpace(Name.StringValue)) return Name.StringValue;
			return ServiceLocator.MegaCacheService.GetLocalizedString(Name.StringValue);
		}
	}
}
