using System.Collections.Generic;

namespace GlobalActions.Models.ScriptRunners {
    public interface IRunner {
        public bool RunnerState { get; set; }
        public void Run(List<Node> nodePipe);

        public void Stop();
        void Toggle(List<Node> nodePipe, HotKey hotKey);
    }
}