using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GlobalActions.Models.Actions;

namespace GlobalActions.Models.ScriptRunners {
	[Serializable]
	public class ToggleMultiplyRunner : IRunner {
		public bool RunnerState { get; set; }

		public void Run(List<IAction> actionPipe) {
			foreach (var action in actionPipe) {
				if (!RunnerState) {
					return;
				}

				action.RunAction();
			}
		}

		public void Stop() {
			RunnerState = false;
		}

		public void Toggle(List<IAction> actionPipe, HotKey hotKey) {
			RunnerState = !RunnerState;

			if (RunnerState) {
				Task.Run(() => {
					while (RunnerState) {
						Console.WriteLine("here");
						Run(actionPipe);
					}

					Stop();
				});
			}
		}
	}
}