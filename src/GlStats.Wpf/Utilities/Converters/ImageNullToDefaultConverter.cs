using System.Globalization;
using System.Windows.Data;

namespace GlStats.Wpf.Utilities.Converters;

public class ImageNullToDefaultConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var avatarUrl = value as Uri;
        if (avatarUrl == null)
            return new Uri("https://placeholder.com/image");

        return avatarUrl;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}