using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Input;

namespace KIT.Interop;
public static partial class Mouse
{
    internal const int WH_MOUSE_LL = 14;
    internal const int WM_MOUSEMOVE = 0x0200;

    internal static event MouseEventHandler? MouseEventHandler;

    private delegate IntPtr MouseProc(int nCode, IntPtr wParam, ref MOUSEHOOKSTRUCT lParam);
    private static MouseProc? MouseCallback;
    private static IntPtr MouseCallbackID;

    [LibraryImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    internal static partial bool GetCursorPos(out POINT position);

    [LibraryImport("user32.dll", EntryPoint = "SetWindowsHookExA", SetLastError = true)]
    private static partial IntPtr SetWindowsHookEx(int idHook, MouseProc lpfn, IntPtr hMod, uint dwThreadId);

    [LibraryImport("user32.dll", SetLastError = true)]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static partial bool UnhookWindowsHookEx(IntPtr hhk);

    [LibraryImport("user32.dll", SetLastError = true)]
    private static partial IntPtr CallNextHookEx(IntPtr hhk, int nCode, IntPtr wParam, ref MOUSEHOOKSTRUCT lParam);

    [LibraryImport("kernel32.dll", EntryPoint = "GetModuleHandleA", SetLastError = true, StringMarshalling = StringMarshalling.Custom, StringMarshallingCustomType = typeof(System.Runtime.InteropServices.Marshalling.AnsiStringMarshaller))]
    private static partial IntPtr GetModuleHandle(string lpModuleName);

    public static Point GetPosition()
    {
        if (GetCursorPos(out POINT position))
        {
            return position.ToPoint();
        }

        throw new Exception();
    }

    public static void AddMouseEventHandler(MouseEventHandler mouseEventHandler)
    {
        bool startHook = mouseEventHandler.GetInvocationList().Length > 0;

        MouseEventHandler += mouseEventHandler;
        if (startHook)
        {
            StartHook();
        }
    }

    public static bool RemoveMouseEventHandler(MouseEventHandler mouseEventHandler)
    {
        Delegate[] mouseDelegates = MouseEventHandler?.GetInvocationList() ?? Array.Empty<Delegate>();

        bool removed = mouseDelegates.Contains(mouseEventHandler);

        if (removed)
        {
            MouseEventHandler -= mouseEventHandler;

            if (mouseDelegates.Length - 1 <= 0)
            {
                StopHook();
            }
        }

        return removed;
    }

    private static void StartHook()
    {
        MouseCallback = HookCallback;

        using (Process curProcess = Process.GetCurrentProcess())
        using (ProcessModule? curModule = curProcess.MainModule)
        {
            MouseCallbackID = SetWindowsHookEx(WH_MOUSE_LL, MouseCallback, GetModuleHandle(curModule!.ModuleName), 0);
        }
    }

    private static void StopHook()
    {
        UnhookWindowsHookEx(MouseCallbackID);
        MouseCallback = null;
    }

    private static IntPtr HookCallback(int nCode, IntPtr wParam, ref MOUSEHOOKSTRUCT lParam)
    {
        if (nCode >= 0 && wParam == WM_MOUSEMOVE)
        {
            // Raise the MouseMove event
            MouseEventHandler?.Invoke(MouseCallbackID, new MouseEventArgs(InputManager.Current.PrimaryMouseDevice, Environment.TickCount));
        }

        return CallNextHookEx(MouseCallbackID, nCode, wParam, ref lParam);
    }
}
