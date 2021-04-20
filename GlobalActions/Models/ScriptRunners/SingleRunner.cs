namespace GlobalActions.Models.ScriptRunners {
    public class SingleRunner : IRunner {
        public bool RunnerState { get; set; }

        public void Run(Node nodePipe) {
            nodePipe.Action.RunAction();

            if (nodePipe.NextNode != null) {
                Run(nodePipe.NextNode);
            }
        }

        public void Stop() {
            throw new System.NotImplementedException();
        }

        public void Toggle(Node nodePipe, HotKey hotKey) {
            Run(nodePipe);
        }
    }
}