using System;
using System.Collections.Generic;
using GlobalActions.Models.Actions;

namespace GlobalActions.Models.ScriptRunners {
	[Serializable]
	public class SingleRunner : IRunner {
		public bool RunnerState { get; set; }

		public void Run(List<IAction> actionPipe) {
			foreach (var action in actionPipe) {
				action.RunAction();
			}
		}

		public void Stop() {
			throw new NotImplementedException();
		}

		public void Toggle(List<IAction> actionPipe, HotKey hotKey) {
			Console.WriteLine("here");
			Run(actionPipe);
		}
	}
}