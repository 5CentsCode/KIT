using System.Runtime.InteropServices;

namespace KIT.Interop;

[StructLayout(LayoutKind.Sequential)]
internal struct MOUSEHOOKSTRUCT
{
    public POINT pt;
    public int hwnd;
    public int wHitTestCode;
    public int dwExtraInfo;
}