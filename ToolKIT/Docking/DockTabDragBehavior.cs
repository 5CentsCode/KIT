using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Input;

namespace ToolKIT.Docking;

public class DockTabDragBehavior : Behavior<UIElement>
{
    private Point? m_startDrag;

    public DockTabDragBehavior()
    {
        m_startDrag = null;
    }

    public EventHandler? FloatTabRequest;

    protected override void OnAttached()
    {
        AssociatedObject.MouseUp += OnElementMouseUp;
        AssociatedObject.MouseDown += OnElementMouseDown;
        AssociatedObject.MouseMove += OnElementMouseMove;
        base.OnAttached();
    }

    protected override void OnDetaching()
    {
        AssociatedObject.MouseUp -= OnElementMouseUp;
        AssociatedObject.MouseDown -= OnElementMouseDown;
        AssociatedObject.MouseMove -= OnElementMouseMove;
        base.OnDetaching();
    }

    private void OnElementMouseUp(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton != MouseButtonState.Pressed)
        {
            m_startDrag = null;
        }
    }

    private void OnElementMouseDown(object sender, MouseButtonEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            m_startDrag = e.GetPosition(AssociatedObject);
        }
    }

    private void OnElementMouseMove(object sender, MouseEventArgs e)
    {
        if (m_startDrag != null)
        {
            Point currentPosition = e.GetPosition(AssociatedObject);
            Vector delta = (Point)m_startDrag! - currentPosition;

            double length = Math.Abs(delta.X) + Math.Abs(delta.Y);

            if (length > 5)
            {
                AssociatedObject.MouseUp -= OnElementMouseUp;
                AssociatedObject.MouseDown -= OnElementMouseDown;
                AssociatedObject.MouseMove -= OnElementMouseMove;
                FloatTabRequest?.Invoke(AssociatedObject, EventArgs.Empty);
            }
        }
    }
}
