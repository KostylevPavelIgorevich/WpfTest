using System;
using System.Globalization;
using System.Windows.Data;

namespace WpfTest.Converters
{
    public class BoolToResultConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is bool b)
                return b ? "Правильно" : "Неправильно";
            return "Неправильно";
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
