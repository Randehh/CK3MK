using CK3MK.Models.Game.History;
using CK3MK.Services;
using ReactiveUI;
using System;

namespace CK3MK.Models.Game {
	public class GameModelAttributes {
		public interface IGameModelAttribute {
			public BaseGameModel Model { get; set; }
			public Action OnValueChanged { get; set; }
			public string StringValue { get; set; }
			public bool IsPostLinkAttribute { get; set; }
		}

		public abstract class GameModelAttribute<T> : ReactiveObject, IGameModelAttribute {
			public BaseGameModel Model { get; set; }
			public string Name { get; set; }
			private T m_Value;
			public T Value {
				get => m_Value;
				set {
					this.RaiseAndSetIfChanged(ref m_Value, value);
					this.RaisePropertyChanged(nameof(StringValue));
					m_Value = value;
					if (!IsAssigned) {
						IsAssigned = m_Value != null;
					}
				}
			}
			public Action OnValueChanged { get; set; } = delegate { };
			public bool IsPostLinkAttribute { get; set; }

			public GameModelAttribute(BaseGameModel model, string name, bool postLinkAttribute = false) {
				Model = model;
				Name = name;
				IsPostLinkAttribute = postLinkAttribute;
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

			public GameModelAttributeString(BaseGameModel model, string name, bool hasQuotes = false) : base(model, name) {
				HasQuotes = hasQuotes;
			}

			public override string ValueFromString(string s) {
				return s;
			}

			public string GetValue(bool includeQuotes) {
				return Value;
			}
		}
		
		public class GameModelAttributeInt : GameModelAttribute<int> {
			public GameModelAttributeInt(BaseGameModel model, string name) : base(model, name) { }

			public override int ValueFromString(string s) {
				int result = 0;
				if( int.TryParse(s, out result)) {
					return result;
				} else {
					ServiceLocator.LoggingService.WriteLine($"Error parsing an integer to string in attribute {Name}: {s}", LoggingService.LogSeverity.Error);
					return 0;
				}
			}
		}
		
		public class GameModelAttributeBool : GameModelAttribute<bool> {
			public GameModelAttributeBool(BaseGameModel model, string name) : base(model, name) { }

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

		public class GameModelAttributeCharacter : GameModelAttribute<Character> {
			public string FileName { get; set; }
			public GameModelAttributeCharacter(BaseGameModel model, string name, string fileName) : base(model, name, true) {
				FileName = fileName;
			}

			public override Character ValueFromString(string s) {
				return ServiceLocator.GameModelService.GetCharacter(FileName, s);
			}

			public override string ValueToString(Character o) {
				if(o != null) {
					return o.Name.StringValue;
				}
				return null;
			}
		}
	}
}
