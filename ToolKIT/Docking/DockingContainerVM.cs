using System.Collections.ObjectModel;
using System.Windows.Media;

namespace ToolKIT.Docking;

public class DockingContainerVM : BaseVM
{
    public DockingContainerVM(object vm)
    {
        DockedPanelTitles = new ObservableCollection<object>()
        {
            vm
        };

        DockedPanelTitles.Add(vm);
    }

    public DockingContainerVM()
    {
        DockedPanelTitles = new ObservableCollection<object>();

        DockedPanelTitles.Add(new TestDockPanel(Colors.Purple));
        DockedPanelTitles.Add(new TestDockPanel2(Colors.Pink));
        DockedPanelTitles.Add(new TestDockPanel3(Colors.Magenta));
    }

    public ObservableCollection<object> DockedPanelTitles { get; set; }
}
