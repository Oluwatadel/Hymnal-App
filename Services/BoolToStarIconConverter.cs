using System.Globalization;

namespace Hymn_Book.Services
{
    public class BoolToStarIconConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture) =>
            (value is bool isFavorite && isFavorite)
                ? "star_filled.png"
                : "star_outline.png";

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture) =>
            throw new NotSupportedException();
    }
}
