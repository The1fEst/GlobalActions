using System;
using System.Linq;
using Avalonia.Collections;
using Avalonia.Input;
using GlobalActions.GUI.ViewModels;
using ReactiveUI;

namespace GlobalActions.GUI.NodeSystem.Nodes {
    public class TextNodeViewModel : ReactiveObject {
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

        private string _text;

        public string Text {
            get => _text;
            set => this.RaiseAndSetIfChanged(ref _text, value);
        }
    }
}