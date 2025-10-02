using System.Windows;

namespace ToolKIT.Docking;

/// <summary>
/// Interaction logic for DockingWindow.xaml
/// </summary>
public partial class DockingWindow : Window
{
    public DockingWindow()
    {
        DataContext = new DockingContainerVM();
        InitializeComponent();
    }
}
