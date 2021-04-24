using System;
using System.Linq;
using Avalonia.Collections;
using Avalonia.Input;
using ReactiveUI;

namespace GlobalActions.GUI.NodeSystem.Nodes {
	public class MouseNodeViewModel : ReactiveObject {
		private uint _delayAfter;

		private uint _delayBefore;

		private MouseButton _key = MouseButton.None;

		public uint DelayBefore {
			get => _delayBefore;
			set => this.RaiseAndSetIfChanged(ref _delayBefore, value);
		}

		public uint DelayAfter {
			get => _delayAfter;
			set => this.RaiseAndSetIfChanged(ref _delayAfter, value);
		}

		public AvaloniaList<MouseButton> AvailableKeys =>
			new(Enum.GetValues(typeof(MouseButton)).Cast<MouseButton>());

		public MouseButton Key {
			get => _key;
			set => this.RaiseAndSetIfChanged(ref _key, value);
		}
	}
}