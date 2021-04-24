using Avalonia.Collections;
using ReactiveUI;

namespace GlobalActions.GUI.Views {
    public class ScriptSelectorViewModel : ReactiveObject{
        private AvaloniaList<string> _scripts;

        public AvaloniaList<string> Scripts {
            get => _scripts;
            set => this.RaiseAndSetIfChanged(ref _scripts, value);
        }

        private string _name;

        public string Name {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        private string _selectedScript;

        public string SelectedScript {
            get => _selectedScript;
            set => this.RaiseAndSetIfChanged(ref _selectedScript, value);
        }
    }
}