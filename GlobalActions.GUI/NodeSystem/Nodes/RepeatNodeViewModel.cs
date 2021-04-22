using System;
using System.Linq;
using Avalonia.Collections;
using Avalonia.Input;
using GlobalActions.GUI.ViewModels;
using ReactiveUI;

namespace GlobalActions.GUI.NodeSystem.Nodes {
    public class RepeatNodeViewModel : ReactiveObject {
        private uint _delayBefore;

        public uint DelayBefore {
            get => _delayBefore;
            set => this.RaiseAndSetIfChanged(ref _delayBefore, value);
        }

        private uint _delayAfter;

        public uint DelayAfter {
            get => _delayAfter;
            set => this.RaiseAndSetIfChanged(ref _delayAfter, value);
        }

        public AvaloniaList<Key> AvailableKeys =>
            new(Enum.GetValues(typeof(Key)).Cast<Key>());

        private Key _key = Key.None;

        public Key Key {
            get => _key;
            set => this.RaiseAndSetIfChanged(ref _key, value);
        }
    }
}