using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GlobalActions.Models.Actions;

namespace GlobalActions.Models.ScriptRunners {
	[Serializable]
	public class HoldMultiplyRunner : IRunner {
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

		public void Toggle(List<IAction> nodePipe, HotKey hotKey) {
			var state = HotKeyHandler.GetState(hotKey.Key);

			RunnerState = !RunnerState;

			Task.Run(() => {
				while (RunnerState) {
					if (state is KeyStates.Hold or KeyStates.Down) {
						Console.WriteLine("here");
						Run(nodePipe);
					} else {
						Stop();
					}

					state = HotKeyHandler.GetState(hotKey.Key);
				}
			});
		}
	}
}
