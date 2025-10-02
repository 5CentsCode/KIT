using Microsoft.Xaml.Behaviors;
using System.Collections;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ToolKIT.Extensions;

namespace ToolKIT.Docking.Behaviors;

internal class TabControlFloatTabItemBehavior : Behavior<TabControl>
{
    private readonly Size m_dragDistance = new Size(SystemParameters.MinimumHorizontalDragDistance * 8.0d, SystemParameters.MinimumVerticalDragDistance * 8.0d);

    private bool m_draggingTabItem;

    public TabControlFloatTabItemBehavior()
    {
        m_draggingTabItem = false;
    }

    protected override void OnAttached()
    {
        AssociatedObject.PreviewMouseMove += OnPreviewMouseMove;
        AssociatedObject.PreviewMouseLeftButtonUp += OnMouseLeftButtonUp;
        AssociatedObject.PreviewMouseLeftButtonDown += OnMouseLeftButtonDown;
        base.OnAttached();
    }

    protected override void OnDetaching()
    {
        AssociatedObject.PreviewMouseMove -= OnPreviewMouseMove;
        AssociatedObject.PreviewMouseLeftButtonUp -= OnMouseLeftButtonUp;
        AssociatedObject.PreviewMouseLeftButtonDown -= OnMouseLeftButtonDown;
        base.OnDetaching();
    }

    private void OnMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
    {
        m_draggingTabItem = false;
        AssociatedObject.ReleaseMouseCapture();
    }

    private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
    {
        IEnumerable<TabItem> tabItems = AssociatedObject.GetChildrenOfType<TabItem>();

        foreach (TabItem tabItem in tabItems)
        {
            Rect elementRect = new Rect(new Point(0.0, 0.0), tabItem.RenderSize);
            Point relativePosition = Mouse.GetPosition(tabItem);

            m_draggingTabItem = elementRect.Contains(relativePosition);
            if (m_draggingTabItem)
            {
                break;
            }
        }
    }

    private void OnPreviewMouseMove(object sender, MouseEventArgs e)
    {
        if (m_draggingTabItem &&
            e.LeftButton == MouseButtonState.Pressed)
        {
            AssociatedObject.CaptureMouse();

            int selectedIndex = AssociatedObject.SelectedIndex;
            TabItem selectedTabItem = AssociatedObject.GetChildrenOfType<TabItem>()
                .First(tabItem => tabItem.Content == AssociatedObject.SelectedContent);

            Rect elementRect = new Rect(new Point(0, 0), selectedTabItem.RenderSize);
            elementRect.Inflate(m_dragDistance);
            Point currentPosition = Mouse.GetPosition(selectedTabItem);

            if (!elementRect.Contains(currentPosition))
            {
                // TODO: Add to Docking Manager.
                DockingContainerVM dockingContainer = new DockingContainerVM(selectedTabItem.Content);
                DockingWindow window = new DockingWindow();
                window.DataContext = dockingContainer;
                window.Show();

                if (AssociatedObject.ItemsSource is IList itemsSourceList)
                {
                    itemsSourceList.RemoveAt(AssociatedObject.SelectedIndex);
                    AssociatedObject.SelectedIndex = Math.Max(0, AssociatedObject.SelectedIndex - 1);
                }

                m_draggingTabItem = false;
                AssociatedObject.ReleaseMouseCapture();
            }
        }
    }
}
