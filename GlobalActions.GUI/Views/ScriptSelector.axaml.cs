using System.IO;
using System.Linq;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GlobalActions.GUI.NodeSystem;

namespace GlobalActions.GUI.Views {
    public class ScriptSelector : UserControl {
        private readonly ScriptSelectorViewModel _vm;
        
        public ScriptSelector() {
            DataContext = _vm = new();
            
            InitializeComponent();
        }

        private void InitializeComponent() {
            AvaloniaXamlLoader.Load(this);
            LoadScripts();
        }
        
        private void Add(object? sender, RoutedEventArgs e) {
            if (string.IsNullOrEmpty(_vm.Name)) return;
            
            _vm.Scripts.Add(_vm.Name);
        }

        private void LoadScripts() {
            if (!Directory.Exists(Program.ScriptsDirectory)) {
                Directory.CreateDirectory(Program.ScriptsDirectory);
                return;
            }

            var files = Directory.GetFiles(Program.ScriptsDirectory)
                .Select(file => Path.GetFileName(file)!)
                .ToArray();

            _vm.Scripts = new(files);
        }

        private void LoadScript(object? sender, RoutedEventArgs e) {
            this.FindControl<ScriptEditor>("ScriptEditor").LoadFromFile(_vm.SelectedScript);
        }
    }
}