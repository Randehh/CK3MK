using static CK3MK.Models.Game.GameModelAttributes;

namespace CK3MK.Models.Game.Common {
	public class Decision : BaseGameModel {

		public GameModelAttributeString Picture { get; set; }
		public GameModelAttributeString IsShown { get; set; }
		public GameModelAttributeString IsValidShowingFailuresOnly { get; set; }
		public GameModelAttributeString Effect { get; set; }
		public GameModelAttributeInt AiCheckInterval { get; set; }

		public Decision(string fileName) : base(fileName) {
			Picture = RegisterAttribute(new GameModelAttributeString(this, "Picture"));
			IsShown = RegisterAttribute(new GameModelAttributeString(this, "Is shown"));
			IsValidShowingFailuresOnly = RegisterAttribute(new GameModelAttributeString(this, "Is valid showing failures only"));
			Effect = RegisterAttribute(new GameModelAttributeString(this, "Effect"));
			AiCheckInterval = RegisterAttribute(new GameModelAttributeInt(this, "AI check interval"));
		}
	}
}
