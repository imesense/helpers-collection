using System.Runtime.InteropServices;

namespace ImeSense.Helpers.Wpf.Interop;

internal unsafe static class NativeFunctions {
    [DllImport("user32.dll")]
    public static extern bool SetParent(IntPtr hWnd, IntPtr hWndNewParent);
}
