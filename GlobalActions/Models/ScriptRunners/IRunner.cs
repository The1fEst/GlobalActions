namespace GlobalActions.Models.ScriptRunners {
    public interface IRunner {
        public bool RunnerState { get; set; }
        public void Run(Node nodePipe);

        public void Stop();
        void Toggle(Node nodePipe, HotKey hotKey);
    }
}