using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using static GlobalActions.Win32Interop;

namespace GlobalActions.GUI {
  public class InterceptKeys {
    public delegate void OnKeyDown(int key);

    private static readonly LowLevelKeyboardProc Proc = HookCallback;

    private static IntPtr _hookId = IntPtr.Zero;

    private static bool _isRunning;

    public static OnKeyDown? KeyDown;

    public static void Run() {
      if (Toggle()) {
        Task.Run(() => {
          _hookId = SetHook(Proc);
          while (GetMessage(out _, IntPtr.Zero, 0, 0)) {
          }
        });
      }
    }

    public static void Stop() {
      if (!Toggle()) {
        UnhookWindowsHookEx(_hookId);
      }
    }

    private static bool Toggle() {
      return _isRunning = !_isRunning;
    }

    private static IntPtr SetHook(LowLevelKeyboardProc proc) {
      return SetWindowsHookEx(WH_KEYBOARD_LL, proc, IntPtr.Zero, 0);
    }

    private static IntPtr HookCallback(int nCode, IntPtr wParam, IntPtr lParam) {
      if (nCode < 0) {
        return CallNextHookEx(_hookId, nCode, wParam, lParam);
      }

      var vkCode = Marshal.ReadInt32(lParam);
      var wmParam = (WM) wParam;
      var isDown = wmParam is WM.WM_KEYDOWN or WM.WM_SYSKEYDOWN;
      var isUp = wmParam is WM.WM_KEYUP or WM.WM_SYSKEYUP;

      if (isDown || isUp) {
        var hotKey = HotKeyHandler.HasKey(vkCode);

        if (hotKey != null) {
          Task.Run(() => HotKeyHandler.TryRunAction(hotKey, wmParam));
          return IntPtr.Parse("1");
        }
      }

      if (isDown) {
        KeyDown?.Invoke(vkCode);
      }

      return CallNextHookEx(_hookId, nCode, wParam, lParam);
    }
  }
}
