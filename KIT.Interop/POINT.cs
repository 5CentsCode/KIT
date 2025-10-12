using System.Runtime.InteropServices;
using System.Windows;

namespace KIT.Interop;

[StructLayout(LayoutKind.Sequential)]
internal struct POINT
{
    public int X;
    public int Y;

    public readonly Point ToPoint()
    {
        return new Point(X, Y);
    }
}
