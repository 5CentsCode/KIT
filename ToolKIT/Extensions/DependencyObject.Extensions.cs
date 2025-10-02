using System.Windows;
using System.Windows.Media;

namespace ToolKIT.Extensions;
public static class DependencyObjectExtensions
{
    public static DependencyObject GetParent(this DependencyObject dependencyObject)
    {
        return VisualTreeHelper.GetParent(dependencyObject);
    }

    public static T? GetAncestor<T>(this DependencyObject dependencyObject)
        where T : DependencyObject
    {
        DependencyObject? parent = dependencyObject.GetParent();
        if (parent != null &&
            parent is not T)
        {
            parent = parent.GetAncestor<T>();
        }

        T? tParent = parent?.ThrowIfNotType<T>();
        return tParent;
    }

    public static IEnumerable<T> GetChildrenOfType<T>(this DependencyObject dependencyObject)
    {
        for (int i = 0; i < VisualTreeHelper.GetChildrenCount(dependencyObject); i++)
        {
            DependencyObject? child = VisualTreeHelper.GetChild(dependencyObject, i);
            if (child is T childT)
            {
                yield return childT;
            }

            IEnumerable<T> childrenOfChild = GetChildrenOfType<T>(child);
            foreach (T childOfChild in childrenOfChild)
            {
                yield return childOfChild;
            }
        }
    }
}
