using Microsoft.Xaml.Behaviors;
using System.Collections;
using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ToolKIT.Extensions;

namespace ToolKIT.Docking.Behaviors;
internal class TabControlReorderTabItemBehavior : Behavior<TabControl>
{
    private bool m_intersectingWithSelection;

    public TabControlReorderTabItemBehavior()
    {
        m_intersectingWithSelection = false;
    }

    protected override void OnAttached()
    {
        AssociatedObject.PreviewMouseMove += OnPreviewMouseMove;
        base.OnAttached();
    }

    protected override void OnDetaching()
    {
        AssociatedObject.PreviewMouseMove -= OnPreviewMouseMove;
        base.OnDetaching();
    }

    private void OnPreviewMouseMove(object sender, MouseEventArgs e)
    {
        if (e.LeftButton == MouseButtonState.Pressed)
        {
            int selectedIndex = AssociatedObject.SelectedIndex;
            TabItem? selectedTabItem = AssociatedObject.GetVisualChildrenOfType<TabItem>()
                .FirstOrDefault(tabItem => tabItem.Content == AssociatedObject.SelectedContent);
            if (selectedTabItem == null)
            {
                return;
            }

            Rect elementRect = new Rect(new Point(0.0, 0.0), selectedTabItem.RenderSize);
            Point relativePosition = Mouse.GetPosition(selectedTabItem);

            m_intersectingWithSelection |= elementRect.Contains(relativePosition);

            if (m_intersectingWithSelection)
            {
                int? swapIndex = null;

                if (relativePosition.X < 0 &&
                    selectedIndex - 1 >= 0)
                {
                    swapIndex = selectedIndex - 1;
                    m_intersectingWithSelection = false;
                    Debug.WriteLine("Move Left");
                }
                else if (relativePosition.X >= selectedTabItem.RenderSize.Width &&
                    selectedIndex + 1 < AssociatedObject.Items.Count)
                {
                    swapIndex = selectedIndex + 1;
                    m_intersectingWithSelection = false;
                    Debug.WriteLine("Move Right");
                }

                if (swapIndex != null)
                {
                    if (AssociatedObject.ItemsSource is IList itemsSourceList)
                    {
                        itemsSourceList.Swap(selectedIndex, swapIndex.Value);
                        AssociatedObject.SelectedIndex = swapIndex.Value;
                    }
                }
            }
        }
    }
}
