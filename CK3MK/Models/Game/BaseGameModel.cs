using CK3MK.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Reflection;
using static CK3MK.Models.Game.GameModelAttributes;

namespace CK3MK.Models.Game {
	public abstract class BaseGameModel {

		public static string ScopeType { get; } = "";
		public static GameType Scope => ServiceLocator.GameDumpService.GetGameType(ScopeType);

		public string CacheId => $"{FileSourceName}_{Id.Value}";

		//Base attributes
		public string FileSourceName { get; set; }
		public GameModelAttributeString Id { get; set; }
		public GameModelAttributeString Name { get; set; }

		private List<Tuple<IGameModelAttribute, string, bool>> m_PostLinkPairs = new List<Tuple<IGameModelAttribute, string, bool>>();

		public BaseGameModel(string fileName) {
			FileSourceName = fileName;
			Id = RegisterAttribute(new GameModelAttributeString(this, "ID"));
			Name = RegisterAttribute(new GameModelAttributeString(this, "Name", true));
		}

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

		public void SetAttributeValue(string attributeKey, string value, bool isAssigned = true) {
			string propertyKey = TitleCaseConvert(attributeKey);
			Type thisType = GetType();
			PropertyInfo property = thisType.GetProperty(propertyKey);
			if(property != null) {
				IGameModelAttribute attribute = property.GetValue(this) as IGameModelAttribute;
				if (attribute != null) {
					if (attribute.IsPostLinkAttribute) {
						attribute.RawStringValue = value;
						m_PostLinkPairs.Add(new Tuple<IGameModelAttribute, string, bool>(attribute, value, isAssigned));
					} else {
						attribute.StringValue = value;
						attribute.IsAssigned = isAssigned;
					}
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

		public void DoPostLinkAttributes() {
			foreach(Tuple<IGameModelAttribute, string, bool> pair in m_PostLinkPairs) {
				pair.Item1.StringValue = pair.Item2;
				pair.Item1.IsAssigned = pair.Item3;
			}
			m_PostLinkPairs.Clear();
		}

		private void OnAttributeChanged() {
			OnModelChanged();
		}

		public virtual string GetListEntryName() {
			return Name.Value;
		}
	}
}
