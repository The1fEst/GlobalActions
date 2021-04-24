using System;

namespace GlobalActions.Models.Actions {
	public class DelayAction : IAction {
		public TimeSpan Delay { get; set; }

		public int DelayBefore { get; set; }

		public int DelayAfter { get; set; }

		public void RunAction() {
			throw new NotImplementedException();
		}
	}
}