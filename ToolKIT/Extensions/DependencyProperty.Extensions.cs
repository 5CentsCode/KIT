using System.Windows;

namespace ToolKIT.Extensions;
public static class DependencyPropertyExtensions
{
    public static T GetValue<T>(this DependencyProperty dependencyProperty, DependencyObject owner)
    {
        object objValue = owner.GetValue(dependencyProperty);
        objValue.ThrowIfNotType<T>();
        T tValue = (T)objValue;
        return tValue;
    }

    public static bool SetValue<T>(this DependencyProperty dependencyProperty, DependencyObject owner, T value)
    {
        bool change = dependencyProperty.GetValue<T>(owner)?.Equals(value) ?? false;
        if (change)
        {
            owner.SetValue(dependencyProperty, value);
        }

        return change;
    }
}
