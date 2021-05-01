using System;
using System.Collections.Generic;
using System.Linq;
using GlobalActions.Models.ScriptRunners;

namespace GlobalActions.Models {
	public class Script : ObservableModel {
		private bool _isActive;

		private ScriptMode _mode;

		private IRunner _scriptRunner = new SingleRunner();

		public Script(string name) {
			Name = name;
		}

		public int Id { get; set; }

		public string Name { get; }

		public List<Node> NodePipe { get; set; }

		private HotKey _hotKey = new();  

		public HotKey HotKey { 
			get => _hotKey;
			set => RaiseAndSet(ref _hotKey, value, ChangeHotKey);
		}

		public ScriptMode Mode {
			get => _mode;
			set => RaiseAndSet(ref _mode, value, SelectRunner);
		}

		public bool IsActive {
			get => _isActive;
			set => RaiseAndSet(ref _isActive, value, ToggleActive);
		}

		private void ChangeHotKey() {
			HotKeyHandler.UnregisterHotKey(Id);
			HotKeyHandler.RegisterHotKey(Id, Toggle, HotKey.Key, HotKey.Modifiers.ToArray());
		}

		private void ToggleActive() {
			if (_isActive) {
				HotKeyHandler.RegisterHotKey(Id, Toggle, HotKey.Key, HotKey.Modifiers.ToArray());
			}
			else {
				HotKeyHandler.UnregisterHotKey(Id);
			}
		}

		public void Toggle() {
			if (!NodePipe.Any() || !_isActive) {
				return;
			}

			_scriptRunner.Toggle(NodePipe, HotKey);
		}

		private void SelectRunner() {
			if (_scriptRunner is {RunnerState: true}) {
				_scriptRunner.Stop();
			}

			_scriptRunner = Mode switch {
				ScriptMode.Single => new SingleRunner(),
				ScriptMode.HoldMultiply => new HoldMultiplyRunner(),
				ScriptMode.ToggleMultiply => new ToggleMultiplyRunner(),
				_ => throw new ArgumentOutOfRangeException(),
			};
		}
	}
}