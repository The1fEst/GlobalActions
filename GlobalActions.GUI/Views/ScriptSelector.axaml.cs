using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;

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
  }
}
