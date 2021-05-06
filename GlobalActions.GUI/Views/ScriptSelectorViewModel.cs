using System.Linq;
using Avalonia.Collections;
using GlobalActions.GUI.NodeSystem;
using GlobalActions.Models;
using ReactiveUI;

namespace GlobalActions.GUI.Views {
  public class ScriptSelectorViewModel : ReactiveObject {
    private string _name = "";

    public AvaloniaList<Script> Scripts {
      get => ScriptsList.Instance.Scripts;
      set => ScriptsList.Instance.Scripts = value;
    }

    public string Name {
      get => _name;
      set => this.RaiseAndSetIfChanged(ref _name, value);
    }

    public Script? SelectedScript {
      get => ScriptsList.Instance.SelectedScript;
      set => this.RaiseAndSetIfChanged(ref ScriptsList.Instance.SelectedScript, value);
    }

    public void Load(string name) {
      SelectedScript = ScriptsList.Instance.Scripts.FirstOrDefault(script => script.Name == name);

      if (SelectedScript != null) {
        ScriptEditor.Instance.Load(SelectedScript);
      }
    }
  }
}
