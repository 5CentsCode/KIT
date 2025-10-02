using System.ComponentModel;
using System.Windows.Media;

namespace ToolKIT.Docking;

[DisplayName("Test Panel")]
class TestDockPanel
{
    public TestDockPanel(Color fill)
    {
        Fill = new SolidColorBrush(fill);
    }
    public SolidColorBrush Fill { get; set; }
}

[DisplayName("Panel #2")]
class TestDockPanel2 : TestDockPanel
{
    public TestDockPanel2(Color fill)
        : base(fill)
    {
    }
}

[DisplayName("Third Panel")]
class TestDockPanel3 : TestDockPanel
{
    public TestDockPanel3(Color fill)
        : base(fill)
    {
    }
}