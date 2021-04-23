﻿using static CK3MK.Models.Game.GameModelAttributes;

namespace CK3MK.Models.Game.Common {
	public class Dynasty : BaseGameModel {

		public GameModelAttributeString Culture { get; set; }
		public GameModelAttributeString Prefix { get; set; }

		public Dynasty(string fileName) : base(fileName) {
			Culture = RegisterAttribute(new GameModelAttributeString(this, "Culture"));
			Prefix = RegisterAttribute(new GameModelAttributeString(this, "Prefix"));
		}
	}
}