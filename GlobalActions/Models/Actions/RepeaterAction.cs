using System;
using System.Threading;

namespace GlobalActions.Models.Actions {
	[Serializable]
	public class RepeatAction : IAction {
		public int RepeatCount { get; set; }

		public IAction? Action { get; set; }

		public int DelayBefore { get; set; }

		public int DelayAfter { get; set; }

		public void RunAction() {
			Thread.Sleep(DelayBefore);

			for (var i = 0; i < RepeatCount; i++) {
				Action?.RunAction();
			}

			Thread.Sleep(DelayAfter);
		}
	}
}
