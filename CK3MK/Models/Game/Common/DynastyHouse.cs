using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CK3MK.Models.Game.GameModelAttributes;

namespace CK3MK.Models.Game.Common {
	public class DynastyHouse : BaseGameModel {
		public GameModelAttributeDynasty Dynasty { get; set; }
		public GameModelAttributeString Prefix { get; set; }

		public DynastyHouse(string fileName) : base(fileName) {
			Dynasty = RegisterAttribute(new GameModelAttributeDynasty(this, "Dynasty"));
			Prefix = RegisterAttribute(new GameModelAttributeString(this, "Prefix"));
		}
	}
}
