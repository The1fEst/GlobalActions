using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using GlobalActions.Models.Actions;
using GlobalActions.Models.ScriptRunners;

namespace GlobalActions.Models {
	[Serializable]
	public class Script : ObservableModel {
		public const string ScriptsDirectory = "Scripts";

		private HotKey _hotKey = new();

		private bool _isActive;

		private ScriptMode _mode;

		private IRunner _scriptRunner = new SingleRunner();

		public Script(string name) {
			Name = name;
		}

		public int Id { get; set; }

		public string Name { get; }

		public List<IAction> ActionPipe { get; set; } = new();

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

		private void Load() {
			ScriptsList.Instance.SelectedScript = this;
		}

		private void Remove() {
			ScriptsList.Instance.Remove(Name);
		}

		private void ChangeHotKey() {
			HotKeyHandler.UnregisterHotKey(Id);
			HotKeyHandler.RegisterHotKey(Id, Toggle, HotKey.Key, HotKey.Modifiers.ToArray());
		}

		private void ToggleActive() {
			if (_isActive) {
				HotKeyHandler.RegisterHotKey(Id, Toggle, HotKey.Key, HotKey.Modifiers.ToArray());
			} else {
				HotKeyHandler.UnregisterHotKey(Id);
			}
		}

		public static Script? LoadFromFile(string name) {
			if (!Directory.Exists(ScriptsDirectory)) {
				Directory.CreateDirectory(ScriptsDirectory);
				return null;
			}

			var filePath = Path.Combine(ScriptsDirectory, name);
			if (!File.Exists(filePath)) {
				return null;
			}

			var data = File.ReadAllBytes(filePath);
			var script = data.Length > 0
				? data.Deserializer<Script>()
				: new Script(name);

			return script;
		}

		public void SaveToFile() {
			if (!Directory.Exists(ScriptsDirectory)) {
				Directory.CreateDirectory(ScriptsDirectory);
			}

			var filePath = Path.Combine(ScriptsDirectory, Name);
			if (!File.Exists(filePath)) {
				File.Create(filePath).Dispose();
			}

			var data = this.Serialize(); // _vm.ToSave().Serialize();

			File.WriteAllBytes(filePath, data);
		}

		public void Toggle() {
			if (!ActionPipe.Any() || !_isActive) {
				return;
			}

			_scriptRunner.Toggle(ActionPipe, HotKey);
		}

		private void SelectRunner() {
			if (_scriptRunner is { RunnerState: true }) {
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
