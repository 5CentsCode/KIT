using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ToolKIT.Extensions;
using InteropMouse = KIT.Interop.Mouse;

namespace ToolKIT.Docking.Behaviors;
internal class WindowTabItemDragBehavior : Behavior<Window>
{
    private readonly Point m_tabRelativeDragPosition;
    private TabItem? m_dragTab;

    public WindowTabItemDragBehavior(Point tabRelativeDragPosition)
    {
        m_tabRelativeDragPosition = tabRelativeDragPosition;
        m_dragTab = null;
    }

    protected override void OnAttached()
    {
        AssociatedObject.CaptureMouse();
        AssociatedObject.MouseLeftButtonUp += OnLeftMouseButtonUp;
        InteropMouse.AddMouseEventHandler(OnMouseMove);

        IEnumerable<TabItem> tabItems = AssociatedObject.GetVisualChildrenOfType<TabItem>();
        m_dragTab = tabItems.FirstOrDefault();

        base.OnAttached();
    }

    protected override void OnDetaching()
    {
        AssociatedObject.ReleaseMouseCapture();
        AssociatedObject.MouseLeftButtonUp -= OnLeftMouseButtonUp;
        InteropMouse.RemoveMouseEventHandler(OnMouseMove);
        base.OnDetaching();
    }

    private void OnLeftMouseButtonUp(object sender, MouseButtonEventArgs e)
    {
        AssociatedObject.RemoveBehavior(this);
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
        m_dragTab.ThrowIfNull();

        Point mousePosition = InteropMouse.GetPosition();
        Point expectedDragScreenPosition = m_dragTab.PointToScreen(m_tabRelativeDragPosition);
        Vector dragDelta = mousePosition - expectedDragScreenPosition;

        AssociatedObject.Left += dragDelta.X;
        AssociatedObject.Top += dragDelta.Y;
    }
}
