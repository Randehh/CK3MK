using Avalonia.Data;
using Avalonia.Data.Converters;
using CK3MK.Models.Game;
using CK3MK.Models.Game.History;
using CK3MK.Services;
using System;
using System.Globalization;

namespace CK3MK.Utilities.Converters {
	public class CharacterModelConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if (value == null) return null;
			Character character = value as Character;
			SimpleGameModel<Character> simpleCharacterModel = ServiceLocator.ModelCacheService.Characters.GetSimpleModel(character.Id.Value);
			return simpleCharacterModel;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			BindingNotification notification = new BindingNotification(new Exception("Cannot convert from full to simple"), BindingErrorType.Error);
			return notification;
		}
	}
}
