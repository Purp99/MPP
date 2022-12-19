using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;
using System.Windows.Markup;

namespace Application
{
    [ValueConversion(typeof(double), typeof(string))]
    public class DoubleConverter : MarkupExtension, IValueConverter
    {

        private static DoubleConverter _converter = null;
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var doubleVal = (double)value;
            return doubleVal.ToString("0.00");
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            if (_converter == null)
                _converter = new DoubleConverter();
            return _converter;
        }
    }
}
