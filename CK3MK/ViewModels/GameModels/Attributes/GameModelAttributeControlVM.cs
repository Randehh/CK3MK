using CK3MK.Models.Game.Common;
using CK3MK.Models.Game.History;
using CK3MK.Services;
using CK3MK.Views;
using ReactiveUI;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CK3MK.Models.Game.GameModelAttributes;

namespace CK3MK.ViewModels.GameModels.Attributes {
	public class GameModelAttributeControlVM : ViewModelBase {

		private GameModelAttributeControl m_Window;

		private AttributeType m_AttributeType = AttributeType.Unknown;

		public IGameModelAttribute Attribute { get; set; }
		public bool IsStringAttribute => m_AttributeType == AttributeType.String;
		public bool IsIntegerAttribute => m_AttributeType == AttributeType.Integer;
		public bool IsBoolAttribute => m_AttributeType == AttributeType.Bool;
		public bool IsCharacterAttribute => m_AttributeType == AttributeType.Character;
		public bool IsDynastyAttribute => m_AttributeType == AttributeType.Dynasty;

		//Character specific
		private GameModelAttributeCharacter m_CharacterAttribute;
		public ObservableCollection<Character> CharacterList { get; set; }
		public Character SelectedCharacter {
			get => m_CharacterAttribute != null ? m_CharacterAttribute.Value : null;
			set {
				if (m_CharacterAttribute != null) m_CharacterAttribute.Value = value;
			}
		}

		//Dynasty specific
		private GameModelAttributeDynasty m_DynastyAttribute;
		public ObservableCollection<Dynasty> DynastyList { get; set; }
		public Dynasty SelectedDynasty {
			get => m_DynastyAttribute != null ? m_DynastyAttribute.Value : null;
			set {
				if (m_DynastyAttribute != null) m_DynastyAttribute.Value = value;
			}
		}

		public GameModelAttributeControlVM(GameModelAttributeControl window, IGameModelAttribute attribute) {
			m_Window = window;

			SetAttributeType(attribute);
			Attribute = attribute;
		}

		private void SetAttributeType(IGameModelAttribute attribute) {
			if (attribute is GameModelAttributeString) m_AttributeType = AttributeType.String;
			else if (attribute is GameModelAttributeInt)
				m_AttributeType = AttributeType.Integer;
			else if (attribute is GameModelAttributeBool) m_AttributeType = AttributeType.Bool;
			else if (attribute is GameModelAttributeCharacter) {
				m_AttributeType = AttributeType.Character;
				CharacterList = ServiceLocator.GameModelService.GetCharacters();
				m_CharacterAttribute = attribute as GameModelAttributeCharacter;
				SelectedCharacter = m_CharacterAttribute.Value;
			} else if (attribute is GameModelAttributeDynasty) {
				m_AttributeType = AttributeType.Dynasty;
				DynastyList = ServiceLocator.GameModelService.GetDynasties();
				m_DynastyAttribute = attribute as GameModelAttributeDynasty;
				SelectedDynasty = m_DynastyAttribute.Value;
			}
		}


		private enum AttributeType {
			Unknown,
			String,
			Integer,
			Bool,
			Character,
			Dynasty
		}
	}
}
