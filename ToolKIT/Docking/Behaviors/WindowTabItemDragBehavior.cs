using Microsoft.Xaml.Behaviors;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ToolKIT.Extensions;

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
        AssociatedObject.MouseMove += OnMouseMove;
        AssociatedObject.MouseLeftButtonUp += OnLeftMouseButtonUp;

        IEnumerable<TabItem> tabItems = AssociatedObject.GetVisualChildrenOfType<TabItem>();
        m_dragTab = tabItems.FirstOrDefault();

        base.OnAttached();
    }

    protected override void OnDetaching()
    {
        AssociatedObject.ReleaseMouseCapture();
        AssociatedObject.MouseMove -= OnMouseMove;
        AssociatedObject.MouseLeftButtonUp -= OnLeftMouseButtonUp;
        base.OnDetaching();
    }

    private void OnLeftMouseButtonUp(object sender, MouseButtonEventArgs e)
    {
        AssociatedObject.RemoveBehavior(this);
    }

    private void OnMouseMove(object sender, MouseEventArgs e)
    {
        m_dragTab.ThrowIfNull();

        Point currentDragScreenPosition = m_dragTab.PointToScreen(Mouse.GetPosition(m_dragTab));
        Point expectedDragScreenPosition = m_dragTab.PointToScreen(m_tabRelativeDragPosition);
        Vector dragDelta = currentDragScreenPosition - expectedDragScreenPosition;

        AssociatedObject.Left += dragDelta.X;
        AssociatedObject.Top += dragDelta.Y;
    }
}
