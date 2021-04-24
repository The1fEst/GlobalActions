using System.Linq;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GlobalActions.GUI.NodeSystem.Nodes;
using GlobalActions.Models;

namespace GlobalActions.GUI.NodeSystem {
    public class ScriptEditor : UserControl {
        private readonly ScriptEditorViewModel _vm;

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
        }
    }
}