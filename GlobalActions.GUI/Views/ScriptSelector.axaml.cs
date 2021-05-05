using System.Linq;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GlobalActions.GUI.Extensions;
using GlobalActions.GUI.NodeSystem;
using GlobalActions.GUI.NodeSystem.Nodes;

namespace GlobalActions.GUI.Views {
  public class ScriptSelector : UserControl {
    private readonly ScriptSelectorViewModel _vm;

    public ScriptSelector() {
      DataContext = _vm = new ScriptSelectorViewModel();
      InitializeComponent();
    }

    private void InitializeComponent() {
      AvaloniaXamlLoader.Load(this);
      ScriptsList.Instance.LoadScripts();
    }

    private void Add(object? sender, RoutedEventArgs e) {
      if (string.IsNullOrEmpty(_vm.Name)) {
        return;
      }

      ScriptsList.Instance.Add(_vm.Name);
    }

    private void Load(object? sender, RoutedEventArgs e) {
      var button = (Button) sender!;
      var text = (TextBlock) button.Parent.Parent.VisualChildren[0].VisualChildren[0];
      var name = text.Text;

      var script = ScriptsList.Instance.Scripts.FirstOrDefault(x => x.Name == name);

      if (script == null) {
        return;
      }

      var control = this.FindControl<Panel>("ScriptEditor");
      control.Children.Clear();

      var vm = new ScriptEditorViewModel {
        Mode = script.Mode,
        HotKey = script.HotKey,
        Name = script.Name,
        Nodes = new AvaloniaList<INode>(script.ActionPipe.Select(x => x.FromAction())),
      };

      vm.SetKeys();

      var scriptEditor = new ScriptEditor(vm);

      control.Children.Add(scriptEditor);
    }
  }
}
