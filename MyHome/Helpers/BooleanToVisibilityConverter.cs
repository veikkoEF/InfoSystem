using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Data;

namespace MyHome.Helpers
{
    public class BooleanToVisibilityConverter : IValueConverter
    {
#pragma warning disable CA1822 // Member als statisch markieren

        private object GetVisibility(object value)
#pragma warning restore CA1822 // Member als statisch markieren
        {
            if (!(value is bool))
                return Visibility.Collapsed;
            bool objValue = (bool)value;
            if (objValue)
            {
                return Visibility.Visible;
            }
            return Visibility.Collapsed;
        }

        public object Convert(object value, Type targetType, object parameter, string language)
        {
            return GetVisibility(value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}