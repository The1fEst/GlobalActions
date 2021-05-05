namespace GlobalActions.Models.Actions {
	public interface IAction {
		public int DelayBefore { get; set; }

		public int DelayAfter { get; set; }

		void RunAction();
	}
}
