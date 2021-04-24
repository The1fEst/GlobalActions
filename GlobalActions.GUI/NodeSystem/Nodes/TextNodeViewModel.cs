using ReactiveUI;

namespace GlobalActions.GUI.NodeSystem.Nodes {
    public class TextNodeViewModel : ReactiveObject {
        private uint _delayAfter;
        private uint _delayBefore;

        private string _text;

        public uint DelayBefore {
            get => _delayBefore;
            set => this.RaiseAndSetIfChanged(ref _delayBefore, value);
        }

        public uint DelayAfter {
            get => _delayAfter;
            set => this.RaiseAndSetIfChanged(ref _delayAfter, value);
        }

        public string Text {
            get => _text;
            set => this.RaiseAndSetIfChanged(ref _text, value);
        }
    }
}