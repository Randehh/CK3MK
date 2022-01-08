using static CK3MK.Models.Game.GameModelAttributes;

namespace CK3MK.Models.Game.History {
	public class Character : BaseGameModel {
		public static new string ScopeType => "Character";

		public GameModelAttributeString Name { get; set; }
		public GameModelAttributeString Dna { get; set; }
		public GameModelAttributeBool Female { get; set; }
		public GameModelAttributeInt Martial { get; set; }
		public GameModelAttributeInt Prowess { get; set; }
		public GameModelAttributeInt Diplomacy { get; set; }
		public GameModelAttributeInt Intrigue { get; set; }
		public GameModelAttributeInt Stewardship { get; set; }
		public GameModelAttributeInt Learning { get; set; }
		//public GameModelAttribute<List<string>> Trait { get; set; } = new GameModelAttribute<List<string>>();
		public GameModelAttributeCharacter Father { get; set; }
		public GameModelAttributeCharacter Mother { get; set; }
		public GameModelAttributeBool DisallowRandomTraits { get; set; }
		public GameModelAttributeString Religion { get; set; }
		public GameModelAttributeString Culture { get; set; }
		public GameModelAttributeDynasty Dynasty { get; set; }
		public GameModelAttributeDynastyHouse DynastyHouse { get; set; }
		public GameModelAttributeString GiveNickname { get; set; }
		public GameModelAttributeString Sexuality { get; set; }
		public GameModelAttributeInt Health { get; set; }
		public GameModelAttributeInt Fertility { get; set; }

		public Character(string fileName) : base(fileName) {
			Name =					RegisterAttribute(new GameModelAttributeString(this, "Name", true));
			Dna =					RegisterAttribute(new GameModelAttributeString(this, "DNA"));
			Female =				RegisterAttribute(new GameModelAttributeBool(this, "Is female", "Yes", "No"));
			Martial =				RegisterAttribute(new GameModelAttributeInt(this, "Martial skill"));
			Prowess =				RegisterAttribute(new GameModelAttributeInt(this, "Prowess skill"));
			Diplomacy =				RegisterAttribute(new GameModelAttributeInt(this, "Diplomacy skill"));
			Intrigue =				RegisterAttribute(new GameModelAttributeInt(this, "Intrigue skill"));
			Stewardship =			RegisterAttribute(new GameModelAttributeInt(this, "Stewardship skill"));
			Learning =				RegisterAttribute(new GameModelAttributeInt(this, "Learning skill"));
			Father =				RegisterAttribute(new GameModelAttributeCharacter(this, "Father", fileName));
			Mother =				RegisterAttribute(new GameModelAttributeCharacter(this, "Mother", fileName));
			DisallowRandomTraits =	RegisterAttribute(new GameModelAttributeBool(this, "Disallow random traits", "Yes", "No"));
			Religion =				RegisterAttribute(new GameModelAttributeString(this, "Religion", true));
			Culture =				RegisterAttribute(new GameModelAttributeString(this, "Culture", true));
			Dynasty =				RegisterAttribute(new GameModelAttributeDynasty(this, "Dynasty"));
			DynastyHouse =			RegisterAttribute(new GameModelAttributeDynastyHouse(this, "Dynasty house"));
			GiveNickname =			RegisterAttribute(new GameModelAttributeString(this, "Give nickname"));
			Sexuality =				RegisterAttribute(new GameModelAttributeString(this, "Sexuality"));
			Health =				RegisterAttribute(new GameModelAttributeInt(this, "Health"));
			Fertility =				RegisterAttribute(new GameModelAttributeInt(this, "Fertility"));
		}

		public override string GetListEntryName() {
			return Name.StringValue;
		}
	}
}
