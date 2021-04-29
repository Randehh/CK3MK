using Avalonia.Controls;
using CK3MK.Models.Game;
using CK3MK.Models.Game.Common;
using CK3MK.Models.Game.History;
using CK3MK.Services;
using CK3MK.ViewModels.RootPages;
using CK3MK.Views.GameModels.Attributes;
using CK3MK.Views.GameModels.Common;
using ReactiveUI;
using System;
using System.Collections.ObjectModel;
using System.Reactive;
using static CK3MK.Models.Game.GameModelAttributes;

namespace CK3MK.ViewModels.GameModels.Attributes {
	public class GameModelAttributeControlVM : ViewModelBase {

		private GameModelAttributeControl m_Window;

		private Grid m_AttributeDisplayDock;

		private IGameModelAttribute m_Attribute;
		public IGameModelAttribute Attribute {
			get => m_Attribute;
			set => this.RaiseAndSetIfChanged(ref m_Attribute, value);
		}

		private object m_AttributeContextObject;
		public object AttributeContextObject {
			get => m_AttributeContextObject;
			set => m_AttributeContextObject = value;
		}

		private Action m_PushDetailsCommand;
		public Action PushDetailsCommand {
			get => m_PushDetailsCommand;
			set => this.RaiseAndSetIfChanged(ref m_PushDetailsCommand, value);
		}

		public GameModelAttributeControlVM(GameModelAttributeControl window, IGameModelAttribute attribute) {
			m_Window = window;
			m_Attribute = attribute;

			m_AttributeDisplayDock = m_Window.FindControl<Grid>("AttributeDisplayDock");
			SetAttributeType(attribute);
		}

		private void SetAttributeType(IGameModelAttribute attribute) {
			if(attribute == null) {
				return;
			}

			IControl controlToAdd = null;
			if (attribute is GameModelAttributeString) {
				controlToAdd = new GameModelAttributeStringControl();
			} else if (attribute is GameModelAttributeInt) {
				controlToAdd = new GameModelAttributeIntegerControl();
			} else if (attribute is GameModelAttributeBool) {
				controlToAdd = new GameModelAttributeBooleanControl();
			} else if (attribute is GameModelAttributeCharacter) {
				controlToAdd = new GameModelAttributeCharacterControl();
				AttributeContextObject = ServiceLocator.ModelCacheService.Characters.GetObservableCollection();
			} else if (attribute is GameModelAttributeDynasty) {
				controlToAdd = new GameModelAttributeDynastyControl();
				AttributeContextObject = ServiceLocator.ModelCacheService.Dynasties.GetObservableCollection();
				PushDetailsCommand = () => {
					Dynasty dynasty = (Attribute as GameModelAttributeDynasty).Value;
					string dynastyName = dynasty.Name.StringValue;
					RootFlowPageVM.MainFlowPage.PushControl($"Dynasty - {dynastyName}", new DynastyDetailsControl() { DataContext = dynasty });
				};
			} else if (attribute is GameModelAttributeDynastyHouse) {
				controlToAdd = new GameModelAttributeDynastyHouseControl();
				AttributeContextObject = ServiceLocator.ModelCacheService.DynastyHouses.GetObservableCollection();
			} else {
				ServiceLocator.LoggingService.WriteLine($"Cannot find attribute control for attribute {attribute.GetType().Name}", LoggingService.LogSeverity.Critical);
				return;
			}

			if (controlToAdd != null) {
				m_AttributeDisplayDock.Children.Add(controlToAdd);
			}
		}

		private class CharacterConverter {
			public CharacterConverter(IGameModelAttribute attribute) {

			}
			public IGameModelAttribute Attribute { get; set; }
			public ObservableCollection<SimpleGameModel<Character>> Characters { get; set; } = ServiceLocator.ModelCacheService.Characters.GetObservableCollection();
		}

		private enum AttributeType {
			Unknown,
			String,
			Integer,
			Bool,
			Character,
			Dynasty,
			DynastyHouse
		}
	}
}
