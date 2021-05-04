using Avalonia.Collections;
using GlobalActions.Models;
using ReactiveUI;

namespace GlobalActions.GUI.Views {
	public class ScriptSelectorViewModel : ReactiveObject {
		private string _name = "";

		private Script? _selectedScript;

		public AvaloniaList<Script> Scripts {
			get => ScriptsList.Instance.Scripts;
			set => ScriptsList.Instance.Scripts = value;
		}

		public string Name {
			get => _name;
			set => this.RaiseAndSetIfChanged(ref _name, value);
		}

		public Script? SelectedScript {
			get => _selectedScript;
			set => this.RaiseAndSetIfChanged(ref _selectedScript, value);
		}
	}
}