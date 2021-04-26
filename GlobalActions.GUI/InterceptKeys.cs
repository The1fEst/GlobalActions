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
					while (GetMessage(out _, IntPtr.Zero, 0, 0)) { }
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
			if (nCode < 0 || (int) wParam is not (WM_KEYDOWN or WM_SYSKEYDOWN)) {
				return CallNextHookEx(_hookId, nCode, wParam, lParam);
			}

			var vkCode = Marshal.ReadInt32(lParam);
			KeyDown?.Invoke(vkCode);

			if (HotKeyHandler.HasKey(vkCode)) {
				return IntPtr.Zero;
			}

			return CallNextHookEx(_hookId, nCode, wParam, lParam);
		}
	}
}