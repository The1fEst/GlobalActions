using System.Runtime.InteropServices;

namespace GlobalActions {
    internal class Win32Interop {
        [DllImport("user32.dll")]
        public static extern short GetAsyncKeyState(int vKey);
    }
}