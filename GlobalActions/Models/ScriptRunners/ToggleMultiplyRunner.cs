using System.Threading.Tasks;

namespace GlobalActions.Models.ScriptRunners {
    public class ToggleMultiplyRunner : IRunner {
        public bool RunnerState { get; set; }

        public void Run(Node nodePipe) {
            RunnerState = true;

            while (RunnerState) {
                nodePipe.Action.RunAction();

                if (nodePipe.NextNode != null) {
                    nodePipe = nodePipe.NextNode;
                    continue;
                }

                break;
            }
        }

        public void Stop() {
            RunnerState = false;
        }

        public void Toggle(Node nodePipe, HotKey hotKey) {
            Task.Run(() => {
                if (!RunnerState) {
                    Run(nodePipe);
                }
                else {
                    Stop();
                }
            });
        }
    }
}