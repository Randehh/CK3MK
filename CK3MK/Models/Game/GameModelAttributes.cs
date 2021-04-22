using ReactiveUI;
using System;

namespace CK3MK.Models.Game {
	public class GameModelAttributes {
		public interface IGameModelAttribute {
			public Action OnValueChanged { get; set; }
			public string StringValue { get; set; }
		}
		public abstract class GameModelAttribute<T> : ReactiveObject, IGameModelAttribute {

			public string Name { get; set; }
			private T m_Value;
			protected T Value {
				get => m_Value;
				set {
					this.RaiseAndSetIfChanged(ref m_Value, value);
					this.RaisePropertyChanged(nameof(StringValue));
					m_Value = value;
					IsAssigned = true;
				}
			}
			public Action OnValueChanged { get; set; } = delegate { };

			public GameModelAttribute(string name) {
				Name = name;
			}

			private bool m_IsAssigned = false;
			public bool IsAssigned {
				get => m_IsAssigned;
				set => this.RaiseAndSetIfChanged(ref m_IsAssigned, value);
			}

			//Viewmodel functions
			public string StringValue {
				get => Value == null ? "" : ValueToString(Value);
				set {
					Value = ValueFromString(value);
					IsAssigned = true;
				}
			}

			public virtual T ValueFromString(string s) {
				throw new NotImplementedException("Cannot convert a string to an unknown type");
			}

			public virtual string ValueToString(T o) {
				return o.ToString();
			}
		}

		public class GameModelAttributeString : GameModelAttribute<string> {
			public bool HasQuotes { get; set; } = false;

			public GameModelAttributeString(string name, bool hasQuotes = false) : base(name) {
				HasQuotes = hasQuotes;
			}

			public override string ValueFromString(string s) {
				return s.Trim('"');
			}

			public string GetValue(bool includeQuotes) {
				return includeQuotes ? Value : Value.Trim('"');
			}
		}
		public class GameModelAttributeInt : GameModelAttribute<int> {
			public GameModelAttributeInt(string name) : base(name) { }

			public override int ValueFromString(string s) {
				return int.Parse(s);
			}
		}
		public class GameModelAttributeBool : GameModelAttribute<bool> {
			public GameModelAttributeBool(string name) : base(name) { }

			public override bool ValueFromString(string s) {
				if (s.Equals("yes")) {
					return true;
				} else {
					return false;
				}
			}

			public override string ValueToString(bool o) {
				if (o) {
					return "yes";
				} else {
					return "no";
				}
			}
		}
	}
}
