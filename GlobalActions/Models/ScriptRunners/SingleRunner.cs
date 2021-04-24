using System;
using System.Collections.Generic;

namespace GlobalActions.Models.ScriptRunners {
    public class SingleRunner : IRunner {
        public bool RunnerState { get; set; }

        public void Run(List<Node> nodePipe) {
            foreach (var node in nodePipe) node.Action.RunAction();
        }

        public void Stop() {
            throw new NotImplementedException();
        }

        public void Toggle(List<Node> nodePipe, HotKey hotKey) {
            Console.WriteLine("here");
            Run(nodePipe);
        }
    }
}