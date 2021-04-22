using System;
using System.Linq;
using Avalonia.Collections;
using GlobalActions.GUI.NodeSystem.Nodes;
using GlobalActions.Models;
using ReactiveUI;

namespace GlobalActions.GUI.NodeSystem {
    public class ScriptEditorViewModel : ReactiveObject {
        public AvaloniaList<ScriptMode> AvailableModes =>
            new(Enum.GetValues(typeof(ScriptMode)).Cast<ScriptMode>());

        private ScriptMode _mode;

        public ScriptMode Mode {
            get => _mode;
            set => this.RaiseAndSetIfChanged(ref _mode, value);
        }

        public AvaloniaList<INode> AvailableNodes {
            get {
                var type = typeof(INode);
                var list =  new AvaloniaList<INode>(AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => type.IsAssignableFrom(p) && p.IsClass)
                    .Select(x=> (INode)Activator.CreateInstance(x)!));
                return list;
            }
        }

        private INode? _selectedNode;

        public INode? SelectedNode {
            get => _selectedNode;
            set => this.RaiseAndSetIfChanged(ref _selectedNode, value);
        }

        private AvaloniaList<INode> _nodes = new();

        public AvaloniaList<INode> Nodes {
            get => _nodes;
            set => this.RaiseAndSetIfChanged(ref _nodes, value);
        }
    }
}