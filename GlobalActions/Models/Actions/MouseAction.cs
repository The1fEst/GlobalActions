using System;

namespace GlobalActions.Models.Actions {
	public class MouseAction : IAction {
		public int DelayBefore { get; set; }

		public int DelayAfter { get; set; }

		public void RunAction() {
			throw new NotImplementedException();
		}
	}
}