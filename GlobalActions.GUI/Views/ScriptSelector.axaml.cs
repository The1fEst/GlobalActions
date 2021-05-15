using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GlobalActions.GUI.NodeSystem;

namespace GlobalActions.GUI.Views {
  public class ScriptSelector : UserControl {
    private readonly ScriptSelectorViewModel _vm;

    public ScriptSelector() {
      DataContext = _vm = new ScriptSelectorViewModel();
      InitializeComponent();

      ComponentsStore.Add(this);
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

      var editor = ComponentsStore.Get<ScriptEditor>();
      editor?.Load(_vm.Name);
    }
  }
}
