using System.Globalization;
using System.Windows.Data;
using ToolKIT.Extensions;

namespace ToolKIT.ValueConverters;

[ValueConversion(typeof(bool), typeof(bool))]
public class InverseBooleanConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        targetType.ThrowIfNotType<bool>();
        return !(bool)value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        targetType.ThrowIfNotType<bool>();
        return !(bool)value;
    }
}