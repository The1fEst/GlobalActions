using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GlobalActions.Models.ScriptRunners {
	public class ToggleMultiplyRunner : IRunner {
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
			RunnerState = !RunnerState;

			if (RunnerState) {
				Task.Run(() => {
					while (RunnerState) {
						Console.WriteLine("here");
						Run(nodePipe);
					}

					Stop();
				});
			}
		}
	}
}