using System;
using GlobalActions.Models.ScriptRunners;

namespace GlobalActions.Models {
    public class Script : ObservableModel {
        public Script(string name) {
            Name = name;
        }

        public int Id { get; set; }
        public string Name { get; }
        public Node? NodePipe { get; set; }

        public HotKey HotKey { get; set; } = null!;

        private ScriptMode _mode;

        public ScriptMode Mode {
            get => _mode;
            set => RaiseAndSet(ref _mode, value, SelectRunner);
        }

        private bool _isActive;

        public bool IsActive {
            get => _isActive;
            set => RaiseAndSet(ref _isActive, value, ToggleActive);
        }

        private IRunner _scriptRunner = new SingleRunner();

        private void ToggleActive() {
            if (_isActive) {
                HotKeyHandler.RegisterHotKey(Id, HotKey.Key, HotKey.Modifiers);
            }
            else {
                HotKeyHandler.UnregisterHotKey(Id);
            }
        }

        public void Toggle() {
            if (NodePipe == null || !_isActive) return;
            _scriptRunner.Toggle(NodePipe, HotKey);
        }

        private void SelectRunner() {
            if (_scriptRunner is {RunnerState: true})
                _scriptRunner.Stop();

            _scriptRunner = Mode switch {
                ScriptMode.Single => new SingleRunner(),
                ScriptMode.HoldMultiply => new HoldMultiplyRunner(),
                ScriptMode.ToggleMultiply => new ToggleMultiplyRunner(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}