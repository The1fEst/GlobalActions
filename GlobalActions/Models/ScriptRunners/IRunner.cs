using System.Collections.Generic;
using GlobalActions.Models.Actions;

namespace GlobalActions.Models.ScriptRunners {
	public interface IRunner {
		public bool RunnerState { get; set; }

		public void Run(List<IAction> actionPipe);

		public void Stop();

		void Toggle(List<IAction> actionPipe, HotKey hotKey);
	}
}
