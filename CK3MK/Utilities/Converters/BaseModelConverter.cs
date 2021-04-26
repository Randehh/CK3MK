using Avalonia.Data;
using Avalonia.Data.Converters;
using CK3MK.Models.Game;
using CK3MK.Services;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CK3MK.Utilities.Converters {
	public abstract class BaseModelConverter<T> : IValueConverter where T : BaseGameModel {
		public object Convert(object value, Type targetType, object parameter, CultureInfo culture) {
			if (value == null) return null;
			T dynasty = value as T;
			SimpleGameModel<T> simpleModel = GetSimpleModel(dynasty);
			return simpleModel;
		}

		public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) {
			BindingNotification notification = new BindingNotification(new Exception("Cannot convert from full to simple"), BindingErrorType.Error);
			return notification;
		}

		public abstract SimpleGameModel<T> GetSimpleModel(T fullModel);
	}
}
