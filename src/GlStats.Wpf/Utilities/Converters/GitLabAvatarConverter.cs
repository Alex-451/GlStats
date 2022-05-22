using System.Globalization;
using System.Windows.Data;
using GlStats.Core.Boundaries.Infrastructure;
using GlStats.Infrastructure;
using Microsoft.Extensions.DependencyInjection;

namespace GlStats.Wpf.Utilities.Converters;

public class GitLabAvatarConverter : IValueConverter
{
    private readonly JsonConfiguration? _auth;
    public GitLabAvatarConverter()
    {
        _auth = new JsonConfiguration();
    }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var gitLabUrl = _auth.GetConfig().GitLabUrl;

        var avatarUrl = value as Uri;
        if (avatarUrl.IsAbsoluteUri)
            return avatarUrl;

        return new Uri(gitLabUrl + avatarUrl);
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}