using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Media;

namespace ToolKIT.Docking;

public class DockingContainerVM : BaseVM, IDisposable
{
    public DockingContainerVM(object vm)
    {
        DockedPanelTitles = new ObservableCollection<object>();
        DockedPanelTitles.Add(vm);
        DockedPanelTitles.CollectionChanged += OnCollectionChanged;

        m_shouldClose = false;
    }

    public DockingContainerVM()
    {
        DockedPanelTitles = new ObservableCollection<object>();

        DockedPanelTitles.Add(new TestDockPanel(Colors.Purple));
        DockedPanelTitles.Add(new TestDockPanel2(Colors.Pink));
        DockedPanelTitles.Add(new TestDockPanel3(Colors.Magenta));

        DockedPanelTitles.CollectionChanged += OnCollectionChanged;
    }

    public ObservableCollection<object> DockedPanelTitles { get; set; }

    public bool ShouldClose
    {
        get => m_shouldClose;
        set => SetProperty(ref m_shouldClose, value);
    }
    private bool m_shouldClose;

    public void Dispose()
    {
        DockedPanelTitles.CollectionChanged -= OnCollectionChanged;
        GC.SuppressFinalize(this);
    }

    private void OnCollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        ShouldClose = DockedPanelTitles.Count <= 0;
    }
}
