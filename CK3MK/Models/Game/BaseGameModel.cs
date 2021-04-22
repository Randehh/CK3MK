using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using static CK3MK.Models.Game.GameModelAttributes;

namespace CK3MK.Models.Game {
	public class BaseGameModel {
		private List<IGameModelAttribute> m_Attributes = new List<IGameModelAttribute>();
		public List<IGameModelAttribute> Attributes {
			get => m_Attributes;
		}

		public Action OnModelChanged { get; set; } = delegate { };

		public T RegisterAttribute<T>(T attribute) where T : IGameModelAttribute {
			m_Attributes.Add(attribute);
			attribute.OnValueChanged += OnAttributeChanged;
			return attribute;
		}

		public void SetAttributeValue(string attributeKey, string value) {
			string propertyKey = TitleCaseConvert(attributeKey);
			Type thisType = GetType();
			PropertyInfo property = thisType.GetProperty(propertyKey);
			if(property != null) {
				IGameModelAttribute attribute = property.GetValue(this) as IGameModelAttribute;
				if (attribute != null) {
					attribute.StringValue = value;
				} else {
					throw new Exception($"Attribute of type {attributeKey} is not derived from {nameof(IGameModelAttribute)}");
				}
			} else {
				//throw new Exception($"Attribute of type {attributeKey} is not found in game model of type {thisType.Name}");
			}
		}

		private static string TitleCaseConvert(string title) {
			return new CultureInfo("en").TextInfo.ToTitleCase(title.ToLower().Replace("_", " ")).Replace(" ", "");
		}

		private void OnAttributeChanged() {
			OnModelChanged();
		}
	}
}
