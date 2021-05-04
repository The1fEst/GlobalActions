using System;
using System.Linq;
using Avalonia.Collections;
using Avalonia.Input;
using GlobalActions.Models;
using ReactiveUI;

namespace GlobalActions.GUI.NodeSystem.Nodes {
	public class MouseNodeViewModel : ReactiveObject {
		private int _delayAfter;

		private int _delayBefore;

		private InputType _inputType;

		private MouseButton _key = MouseButton.None;

		public int DelayBefore {
			get => _delayBefore;
			set => this.RaiseAndSetIfChanged(ref _delayBefore, value);
		}

		public int DelayAfter {
			get => _delayAfter;
			set => this.RaiseAndSetIfChanged(ref _delayAfter, value);
		}

		public AvaloniaList<MouseButton> AvailableKeys =>
			new(Enum.GetValues(typeof(MouseButton)).Cast<MouseButton>());

		public MouseButton Key {
			get => _key;
			set => this.RaiseAndSetIfChanged(ref _key, value);
		}

		public AvaloniaList<InputType> InputTypes =>
			new(Enum.GetValues(typeof(InputType)).Cast<InputType>());

		public InputType InputType {
			get => _inputType;
			set => this.RaiseAndSetIfChanged(ref _inputType, value);
		}
	}
}