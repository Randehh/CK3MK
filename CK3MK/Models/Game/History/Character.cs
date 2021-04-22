using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CK3MK.Models.Game.GameModelAttributes;

/* Character structure
1001 = {	# character id
	name = ...
	dna = ...
	female = ...
	martial = ...
	prowess = ...
	diplomacy = ...
	intrigue = ...
	stewardship = ...
	learning = ...
	trait = ...
	father = ...
	mother = ...
	disallow_random_traits = ...

	faith = ...
	culture = ...
	dynasty = ...
	dynasty_house = ...
	give_nickname = ...
	sexuality = ...
	health = ...
	fertility = ...
	set_house = ...
	set_culture = ...
	set_character_faith_no_effect = ...
	
	portrait_override = {	# Will override the character's appearance
		portrait_modifier_overrides={
			modifier_category_1 = modifier_1 # E.g. clothes=western_low_nobles
			modifier_category_1 = modifier_2
			...
		}
		hair={ R G B }	# hair color, e.g. hair={ 0.592 0.314 0.176 }
	}
}
*/

namespace CK3MK.Models.Game.History {
	public class Character : BaseGameModel {
		public GameModelAttributeString Id { get; set; }
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
		public GameModelAttributeString Father { get; set; }
		public GameModelAttributeString Mother { get; set; }
		public GameModelAttributeBool DisallowRandomTraits { get; set; }
		public GameModelAttributeString Religion { get; set; }
		public GameModelAttributeString Culture { get; set; }
		public GameModelAttributeString Dynasty { get; set; }
		public GameModelAttributeString DynastyHouse { get; set; }
		public GameModelAttributeString GiveNickname { get; set; }
		public GameModelAttributeString Sexuality { get; set; }
		public GameModelAttributeInt Health { get; set; }
		public GameModelAttributeInt Fertility { get; set; }

		public Character() {
			Id =					RegisterAttribute(new GameModelAttributeString("ID"));
			Name =					RegisterAttribute(new GameModelAttributeString("Name", true));
			Dna =					RegisterAttribute(new GameModelAttributeString("DNA"));
			Female =				RegisterAttribute(new GameModelAttributeBool("Is female"));
			Martial =				RegisterAttribute(new GameModelAttributeInt("Martial skill"));
			Prowess =				RegisterAttribute(new GameModelAttributeInt("Prowess skill"));
			Diplomacy =				RegisterAttribute(new GameModelAttributeInt("Diplomacy skill"));
			Intrigue =				RegisterAttribute(new GameModelAttributeInt("Intrigue skill"));
			Stewardship =			RegisterAttribute(new GameModelAttributeInt("Stewardship skill"));
			Learning =				RegisterAttribute(new GameModelAttributeInt("Learning skill"));
			Father =				RegisterAttribute(new GameModelAttributeString("Father"));
			Mother =				RegisterAttribute(new GameModelAttributeString("Mother"));
			DisallowRandomTraits =	RegisterAttribute(new GameModelAttributeBool("Disallow random traits"));
			Religion =				RegisterAttribute(new GameModelAttributeString("Religion", true));
			Culture =				RegisterAttribute(new GameModelAttributeString("Culture", true));
			Dynasty =				RegisterAttribute(new GameModelAttributeString("Dynasty"));
			DynastyHouse =			RegisterAttribute(new GameModelAttributeString("Dynasty house"));
			GiveNickname =			RegisterAttribute(new GameModelAttributeString("Give nickname"));
			Sexuality =				RegisterAttribute(new GameModelAttributeString("Sexuality"));
			Health =				RegisterAttribute(new GameModelAttributeInt("Health"));
			Fertility =				RegisterAttribute(new GameModelAttributeInt("Fertility"));
		}
	}
}
