using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;

namespace GlobalActions.Models.Actions {
	[Serializable]
	public class TextAction : IAction {
		public string Text { get; set; } = "";

		public int DelayBefore { get; set; }

		public int DelayAfter { get; set; }

		public void RunAction() {
			Thread.Sleep(DelayBefore);

			Task.Run(async () => {
				await Application.Current.Clipboard.SetTextAsync(Text);

				const byte ctrl = (byte) Keys.LControlKey;
				const byte v = (byte) Keys.V;

				Win32Interop.keybd_event(ctrl, ctrl, 0, 0);
				Win32Interop.keybd_event(v, v, 0, 0);
				Win32Interop.keybd_event(v, v, Win32Interop.KEYEVENTF.KEYEVENTF_KEYUP, 0);
				Win32Interop.keybd_event(ctrl, ctrl, Win32Interop.KEYEVENTF.KEYEVENTF_KEYUP, 0);
			});

			Thread.Sleep(DelayAfter);
		}
	}
}
