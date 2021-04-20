using System.Threading.Tasks;

namespace GlobalActions.Models.ScriptRunners {
    public class HoldMultiplyRunner : IRunner {
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
            var keyState = new KeyState(hotKey.Key, hotKey.Modifiers);
            
            Task.Run(() => {
                if (keyState.GetState() is KeyStates.Hold or KeyStates.Down) {
                    Run(nodePipe);
                }
                else {
                    Stop();
                }
            });
        }
    }
}