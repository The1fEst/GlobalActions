using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalActions.Models.ScriptRunners {
	public class HoldMultiplyRunner : IRunner {
		public bool RunnerState { get; set; }

		public void Run(List<Node> nodePipe) {
			foreach (var node in nodePipe) {
				if (!RunnerState) {
					return;
				}

				node.Action.RunAction();
			}
		}

		public void Stop() {
			RunnerState = false;
		}

		public void Toggle(List<Node> nodePipe, HotKey hotKey) {
			var state = HotKeyHandler.GetState(hotKey.Key);

			RunnerState = !RunnerState;

			Task.Run(() => {
				while (RunnerState) {
					if (state is KeyStates.Hold or KeyStates.Down) {
						Console.WriteLine("here");
						Run(nodePipe);
					}
					else {
						Stop();
					}

					state = HotKeyHandler.GetState(hotKey.Key);
				}
			});
		}
	}
}