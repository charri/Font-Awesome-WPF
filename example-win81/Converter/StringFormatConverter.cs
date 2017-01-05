using System;
using Windows.UI.Xaml.Data;

namespace Example.FontAwesome.WIN81.Converter
{
    public class StringFormatConverter
        : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return string.Format(parameter.ToString(), value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
