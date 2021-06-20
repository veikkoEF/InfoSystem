using System;
using System.IO;
using Windows.UI.Xaml.Data;

namespace MyHome.Helpers
{
    public class FileNameConverter : IValueConverter
    {
        // Define the Convert method to change a DateTime object to
        public object Convert(object value, Type targetType,
            object parameter, string language)
        {
            // The value parameter is the data from the source object.
            string fileName = (string)value;
            return Path.GetFileNameWithoutExtension(fileName);
        }

        // ConvertBack is not implemented for a OneWay binding.
        public object ConvertBack(object value, Type targetType,
            object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}