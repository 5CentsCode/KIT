using System.Globalization;
using System.Windows.Data;
using ToolKIT.Extensions;

namespace ToolKIT.Converters;

public abstract class TypedValueConverter<TSource, TTarget> : IValueConverter
    where TSource : Type
    where TTarget : Type
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        value?.GetType().ThrowIfNotType<TSource>();
        TTarget targetValue = Convert(value as TSource, parameter, culture);
        return targetValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        value?.GetType().ThrowIfNotType<TTarget>();
        TSource sourceValue = ConvertBack(value as TTarget, parameter, culture);
        return sourceValue;
    }

    public abstract TTarget Convert(TSource? value, object parameter, CultureInfo culture);

    public abstract TSource ConvertBack(TTarget? value, object parameter, CultureInfo culture);

}
