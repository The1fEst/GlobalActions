using System.IO;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GlobalActions.GUI.NodeSystem.Nodes;

namespace GlobalActions.GUI.NodeSystem {
    public class ScriptEditor : UserControl {
        private ScriptEditorViewModel _vm;

        public void LoadFromFile(string name) {
            if (!Directory.Exists(Program.ScriptsDirectory)) {
                Directory.CreateDirectory(Program.ScriptsDirectory);
                
                DataContext = _vm = new() {
                    Name = name
                };
                
                return;
            }

            var filePath = Path.Combine(Program.ScriptsDirectory, name);
            if (!File.Exists(filePath)) {
                DataContext = _vm = new() {
                    Name = name
                };
                
                return;
            }

            var data = File.ReadAllBytes(filePath);
            var script = data.Deserializer<ScriptSave>();

            DataContext = _vm = ScriptEditorViewModel.FromSave(script);
        }

        public ScriptEditor() {
            DataContext = _vm = new();

            InitializeComponent();
        }

        private void InitializeComponent() {
            AvaloniaXamlLoader.Load(this);
        }


        private void AppendNode(object? sender, RoutedEventArgs e) {
            if (_vm.SelectedNode != null) _vm.Nodes.Add((INode) _vm.SelectedNode.Clone());
        }

        private void Save(object? sender, RoutedEventArgs e) {
            var scriptsList = ScriptsList.Instance;

            scriptsList.Add(_vm.Name);
            scriptsList.Edit(_vm.Name, script => {
                script.Mode = _vm.Mode;
                script.HotKey = new() {Key = (int) _vm.Key};
                script.IsActive = true;
                script.NodePipe = _vm.Nodes.Select(node => node.ToNode()).ToList();
            });

            SaveToFile(_vm.Name);
        }

        private void SaveToFile(string name) {
            if (!Directory.Exists(Program.ScriptsDirectory)) {
                Directory.CreateDirectory(Program.ScriptsDirectory);
            }

            var filePath = Path.Combine(Program.ScriptsDirectory, name);
            if (!File.Exists(filePath)) {
                File.Create(filePath).Dispose();
            }

            var data = _vm.ToSave().Serialize();

            File.WriteAllBytes(filePath, data);
        }
    }
}