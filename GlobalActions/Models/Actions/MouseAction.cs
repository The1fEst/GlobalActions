using System;
using System.Threading;
using Avalonia.Input;
using static GlobalActions.Win32Interop;

namespace GlobalActions.Models.Actions {
	public class MouseAction : IAction {
		public int DelayBefore { get; set; }

		public int DelayAfter { get; set; }

		public MouseButton Key { get; set; }

		public InputType InputType { get; set; }

		public void RunAction() {
			Thread.Sleep(DelayBefore);

			var key = new MouseKey(Key);

			switch (InputType) {
				case InputType.Down:
					mouse_event(key.MouseEventDown, 0, 0, 0, 0);
					break;
				case InputType.Up:
					mouse_event(key.MouseEventUp, 0, 0, 0, 0);
					break;
				case InputType.Press:
					mouse_event(key.MouseEventDown, 0, 0, 0, 0);
					mouse_event(key.MouseEventUp, 0, 0, 0, 0);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			Thread.Sleep(DelayAfter);
		}
	}
}