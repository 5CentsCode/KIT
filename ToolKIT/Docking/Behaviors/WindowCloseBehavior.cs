using Microsoft.Xaml.Behaviors;
using System.Windows;
using ToolKIT.Extensions;

namespace ToolKIT.Docking.Behaviors;
internal class WindowCloseBehavior : Behavior<Window>
{
    public static readonly DependencyProperty ShouldCloseProperty =
        DependencyProperty.Register(
            "ShouldClose",
            typeof(bool),
            typeof(WindowCloseBehavior),
            new PropertyMetadata(false, OnCloseTriggerChanged));

    public bool ShouldClose
    {
        get => ShouldCloseProperty.GetValue<bool>(this);
        set => ShouldCloseProperty.SetValue(this, value);
    }

    private static void OnCloseTriggerChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        if (d is WindowCloseBehavior behavior)
        {
            behavior.OnCloseTriggerChanged();
        }
    }

    private void OnCloseTriggerChanged()
    {
        if (ShouldClose)
        {
            this.AssociatedObject.Close();
        }
    }
}
