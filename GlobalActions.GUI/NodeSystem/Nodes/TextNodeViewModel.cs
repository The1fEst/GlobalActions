using ReactiveUI;

namespace GlobalActions.GUI.NodeSystem.Nodes {
	public class TextNodeViewModel : ReactiveObject {
		private int _delayAfter;

		private int _delayBefore;

		private string _text = "";

		public int DelayBefore {
			get => _delayBefore;
			set => this.RaiseAndSetIfChanged(ref _delayBefore, value);
		}

		public int DelayAfter {
			get => _delayAfter;
			set => this.RaiseAndSetIfChanged(ref _delayAfter, value);
		}

		public string Text {
			get => _text;
			set => this.RaiseAndSetIfChanged(ref _text, value);
		}
	}
}
