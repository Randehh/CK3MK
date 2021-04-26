using Avalonia.Data;
using Avalonia.Data.Converters;
using CK3MK.Models.Game;
using CK3MK.Models.Game.Common;
using CK3MK.Services;
using System;
using System.Globalization;

namespace CK3MK.Utilities.Converters {
	public class DynastyModelConverter : IValueConverter {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if (value == null) return null;
			Dynasty dynasty = value as Dynasty;
			SimpleGameModel<Dynasty> simpleDynastyModel = ServiceLocator.ModelCacheService.Dynasties.GetSimpleModel(dynasty.Id.Value);
			return simpleDynastyModel;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			BindingNotification notification = new BindingNotification(new Exception("Cannot convert from full to simple"), BindingErrorType.Error);
			return notification;
		}
	}
}
