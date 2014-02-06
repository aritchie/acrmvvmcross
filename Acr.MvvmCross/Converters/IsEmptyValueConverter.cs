using System;
using System.Globalization;
using Cirrious.CrossCore.Converters;


namespace Acr.MvvmCross.Converters {
    
    public class IsEmptyConverter : MvxValueConverter<string, bool> {

        protected override bool Convert(string value, Type targetType, object parameter, CultureInfo culture) {
            return String.IsNullOrWhiteSpace(value);
        }
    }
}