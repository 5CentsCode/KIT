using System.Windows;
using System.Windows.Controls.Primitives;

namespace ToolKIT;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public static readonly DependencyProperty PropertyHeaderWidthProperty = DependencyProperty.Register(
        "PropertyHeaderWidth", typeof(float),
        typeof(MainWindow));

    public MainWindow()
    {
        DataContext = new MainWindowVM();
        PropertyHeaderWidth = 100.0f;
        InitializeComponent();
    }

    public float PropertyHeaderWidth
    {
        get => (float)GetValue(PropertyHeaderWidthProperty);
        set => SetValue(PropertyHeaderWidthProperty, value);
    }

    public void GridSplitter_DragDelta(object sender, DragDeltaEventArgs e)
    {
        PropertyHeaderWidth += (float)e.HorizontalChange;
    }
}