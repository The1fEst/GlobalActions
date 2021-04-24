using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Collections;
using GlobalActions.GUI.NodeSystem.Nodes;
using GlobalActions.Models;
using ReactiveUI;

namespace GlobalActions.GUI.NodeSystem {
    public class ScriptEditorViewModel : ReactiveObject {
        private ScriptMode _mode;

        private AvaloniaList<INode> _nodes = new();

        private INode? _selectedNode;

        private string _name;

        public string Name {
            get => _name;
            set => this.RaiseAndSetIfChanged(ref _name, value);
        }

        public AvaloniaList<Keys> AvailableKeys =>
            new(Enum.GetValues(typeof(Keys)).Cast<Keys>());

        public AvaloniaList<ScriptMode> AvailableModes =>
            new(Enum.GetValues(typeof(ScriptMode)).Cast<ScriptMode>());

        public ScriptMode Mode {
            get => _mode;
            set => this.RaiseAndSetIfChanged(ref _mode, value);
        }

        private Keys _key;

        public Keys Key {
            get => _key;
            set => this.RaiseAndSetIfChanged(ref _key, value);
        }

        public AvaloniaList<INode> AvailableNodes {
            get {
                var type = typeof(INode);
                var list = new AvaloniaList<INode>(AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany(s => s.GetTypes())
                    .Where(p => type.IsAssignableFrom(p) && p.IsClass)
                    .Select(x => (INode) Activator.CreateInstance(x)!));
                return list;
            }
        }

        public INode? SelectedNode {
            get => _selectedNode;
            set => this.RaiseAndSetIfChanged(ref _selectedNode, value);
        }

        public AvaloniaList<INode> Nodes {
            get => _nodes;
            set => this.RaiseAndSetIfChanged(ref _nodes, value);
        }

        public ScriptSave ToSave() {
            return new(Nodes
                    .Select(node => node.ToSave())
                    .ToArray(),
                Key,
                Mode,
                Name);
        }

        public static ScriptEditorViewModel FromSave(ScriptSave script) {
            return new() {
                Key = script.Key,
                Mode = script.Mode,
                Name = script.Name,
                Nodes = new(script.Nodes.Select(node => node.FromSave()))
            };
        }
    }

    [Serializable]
    public class ScriptSave {
        public ScriptSave(INodeSave[] nodes, Keys key, ScriptMode mode, string name) {
            Nodes = nodes;
            Key = key;
            Mode = mode;
            Name = name;
        }

        public INodeSave[] Nodes { get; set; }
        public Keys Key { get; set; }
        public ScriptMode Mode { get; set; }
        public string Name { get; set; }
    }
}