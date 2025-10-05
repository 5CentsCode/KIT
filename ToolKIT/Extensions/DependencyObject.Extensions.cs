using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Media;

namespace ToolKIT.Extensions;
public static class DependencyObjectExtensions
{
    public static DependencyObject GetVisualParent(this DependencyObject dependencyObject)
    {
        DependencyObject parent = VisualTreeHelper.GetParent(dependencyObject);
        return parent;
    }

    public static T? GetVisualAncestor<T>(this DependencyObject dependencyObject)
        where T : DependencyObject
    {
        DependencyObject? parent = dependencyObject.GetVisualParent();
        if (parent != null &&
            parent is not T)
        {
            parent = parent.GetVisualAncestor<T>();
        }

        T? tParent = parent?.ThrowIfNotType<T>();
        return tParent;
    }

    public static IEnumerable<T> GetVisualChildrenOfType<T>(this DependencyObject dependencyObject)
    {
        int children = VisualTreeHelper.GetChildrenCount(dependencyObject);
        for (int i = 0; i < children; i++)
        {
            DependencyObject? child = VisualTreeHelper.GetChild(dependencyObject, i);
            if (child is T childT)
            {
                yield return childT;
            }

            IEnumerable<T> childrenOfChild = GetVisualChildrenOfType<T>(child);
            foreach (T childOfChild in childrenOfChild)
            {
                yield return childOfChild;
            }
        }
    }

    public static void AddBehavior(this DependencyObject dependencyObject, Behavior behavior)
    {
        if (dependencyObject is BehaviorCollection collection)
        {
            collection.Add(behavior);
        }
        else
        {
            dependencyObject.GetBehaviors().Add(behavior);
        }
    }

    public static void RemoveBehavior(this DependencyObject dependencyObject, Behavior behavior)
    {
        if (dependencyObject is BehaviorCollection collection)
        {
            collection.Remove(behavior);
        }
        else
        {
            dependencyObject.GetBehaviors().Remove(behavior);
        }
    }

    public static BehaviorCollection GetBehaviors(this DependencyObject dependencyObject)
    {
        BehaviorCollection collection = Interaction.GetBehaviors(dependencyObject);
        return collection;
    }
}
