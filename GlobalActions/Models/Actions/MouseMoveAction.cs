using System;
using System.Threading;
using static GlobalActions.Win32Interop;

namespace GlobalActions.Models.Actions {
	[Serializable]
	public class MouseMoveAction : IAction {
		public int Horizontal { get; set; }

		public int Vertical { get; set; }

		public MouseMoveType? MouseMoveType { get; set; }
		public int DelayBefore { get; set; }
		public int DelayAfter { get; set; }

		public void RunAction() {
			Thread.Sleep(DelayBefore);

			var basePoint = new POINT();
			if (MouseMoveType == Models.MouseMoveType.Relative) {
				GetCursorPos(out basePoint);
			}

			basePoint.x += Horizontal;
			basePoint.y += Vertical;

			SetCursorPos(basePoint.x, basePoint.y);

			Thread.Sleep(DelayAfter);
		}
	}
}
