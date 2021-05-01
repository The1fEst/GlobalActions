using Avalonia.Collections;
using ReactiveUI;

namespace GlobalActions.GUI.Views {
	public class ScriptSelectorViewModel : ReactiveObject {
		private string _name = "";

		private AvaloniaList<string> _scripts = new();

		private string _selectedScript = "";

		public AvaloniaList<string> Scripts {
			get => _scripts;
			set => this.RaiseAndSetIfChanged(ref _scripts, value);
		}

		public string Name {
			get => _name;
			set => this.RaiseAndSetIfChanged(ref _name, value);
		}

		public string SelectedScript {
			get => _selectedScript;
			set => this.RaiseAndSetIfChanged(ref _selectedScript, value);
		}
	}
}