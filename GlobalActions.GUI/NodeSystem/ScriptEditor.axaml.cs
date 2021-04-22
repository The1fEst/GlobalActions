using System;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GlobalActions.GUI.NodeSystem.Nodes;

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
            if (_vm.SelectedNode != null) {
                _vm.Nodes.Add((INode) _vm.SelectedNode.Clone());
            }
        }
    }
}