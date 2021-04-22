using System;

namespace CK3MK.Models.Game {
	public class BaseGameModel {

		public class GameModelAttribute<T> {
			private T m_Value;

			public virtual void SetValue(T value) {
				m_Value = value;
				IsAssigned = true;
			}

			public virtual T GetValue() {
				return m_Value;
			}

			public bool IsAssigned { get; set; } = false;
		}

		public class GameModelAttributeString : GameModelAttribute<string> {
			public bool HasQuotes { get; set; } = false;

			public GameModelAttributeString(bool hasQuotes = false) {
				HasQuotes = hasQuotes;
			}

			public string GetValue(bool includeQuotes) {
				return includeQuotes ? base.GetValue() : base.GetValue().Trim('"');
			}
		}
		public class GameModelAttributeInt : GameModelAttribute<int> {
			public void SetValue(string s) {
				SetValue(int.Parse(s));
			}
		}
		public class GameModelAttributeBool : GameModelAttribute<bool> {
			public void SetValue(string s) {
				if (s.Equals("yes")) {
					SetValue(true);
				} else {
					SetValue(false);
				}
			}
		}
	}
}
