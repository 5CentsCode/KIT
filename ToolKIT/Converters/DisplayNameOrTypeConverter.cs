using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Windows.Data;

namespace ToolKIT.Converters;

[ValueConversion(typeof(object), typeof(string))]
internal class DisplayNameOrTypeConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value == null)
        {
            return string.Empty;
        }

        // value.ThrowIfNull();
        Type valueType = value.GetType();
        string targetValue = valueType.ToString();

        DisplayNameAttribute? displayNameAttribute = valueType.GetCustomAttribute<DisplayNameAttribute>();
        if (displayNameAttribute != null)
        {
            targetValue = displayNameAttribute.DisplayName;
        }

        return targetValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotSupportedException();
    }
}
