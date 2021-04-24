using System;
using System.Linq;
using Avalonia.Collections;
using Avalonia.Input;
using GlobalActions.Models;
using ReactiveUI;

namespace GlobalActions.GUI.NodeSystem.Nodes {
    public class KeyboardNodeViewModel : ReactiveObject {
        private int _delayAfter;
        private int _delayBefore;

        public KeyboardNodeViewModel() {
            _delayAfter = 0;
            _delayBefore = 0;
        }

        public int DelayBefore {
            get => _delayBefore;
            set => this.RaiseAndSetIfChanged(ref _delayBefore, value);
        }

        public int DelayAfter {
            get => _delayAfter;
            set => this.RaiseAndSetIfChanged(ref _delayAfter, value);
        }

        public AvaloniaList<byte> HotKeys;

        private string _hotKey;

        public string HotKey {
            get => _hotKey;
            set => this.RaiseAndSetIfChanged(ref _hotKey, value);
        }
        
        public AvaloniaList<InputType> InputTypes =>
            new(Enum.GetValues(typeof(InputType)).Cast<InputType>());

        private InputType _inputType;

        public InputType InputType {
            get => _inputType;
            set => this.RaiseAndSetIfChanged(ref _inputType, value);
        }
    }
}