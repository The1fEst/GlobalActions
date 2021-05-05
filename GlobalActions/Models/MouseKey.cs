using System;
using Avalonia.Input;
using static GlobalActions.Win32Interop;

namespace GlobalActions.Models {
	public class MouseKey {
		public MouseKey(MouseButton mouseButton) {
			switch (mouseButton) {
				case MouseButton.None:
					break;
				case MouseButton.Left:
					MouseEventDown = MOUSEEVENTF.MOUSEEVENTF_LEFTDOWN;
					MouseEventUp = MOUSEEVENTF.MOUSEEVENTF_LEFTUP;
					break;
				case MouseButton.Right:
					MouseEventDown = MOUSEEVENTF.MOUSEEVENTF_RIGHTDOWN;
					MouseEventUp = MOUSEEVENTF.MOUSEEVENTF_RIGHTUP;
					break;
				case MouseButton.Middle:
					MouseEventDown = MOUSEEVENTF.MOUSEEVENTF_MIDDLEDOWN;
					MouseEventUp = MOUSEEVENTF.MOUSEEVENTF_MIDDLEUP;
					break;
				default:
					throw new ArgumentOutOfRangeException(nameof(mouseButton), mouseButton, null);
			}
		}

		public MOUSEEVENTF MouseEventDown { get; set; }

		public MOUSEEVENTF MouseEventUp { get; set; }
	}
}
