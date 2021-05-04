using System;
using System.Linq;
using Avalonia.Collections;
using GlobalActions.GUI.NodeSystem.Nodes;
using GlobalActions.Models;
using ReactiveUI;

namespace GlobalActions.GUI.NodeSystem {
	public class ScriptEditorViewModel : ReactiveObject {
		private HotKey _hotKey = new();

		private string _keys = "";

		private ScriptMode _mode;

		private string _name = "";

		private AvaloniaList<INode> _nodes = new();

		private INode? _selectedNode;

		public string Name {
			get => _name;
			set => this.RaiseAndSetIfChanged(ref _name, value);
		}

		public AvaloniaList<ScriptMode> AvailableModes =>
			new(Enum.GetValues(typeof(ScriptMode)).Cast<ScriptMode>());

		public ScriptMode Mode {
			get => _mode;
			set => this.RaiseAndSetIfChanged(ref _mode, value);
		}

		public string Keys {
			get => _keys;
			set => this.RaiseAndSetIfChanged(ref _keys, value);
		}

		public HotKey HotKey {
			get => _hotKey;
			set => this.RaiseAndSetIfChanged(ref _hotKey, value);
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

		public bool IsActive { get; set; }

		public ScriptSave ToSave() {
			return new(Nodes
					.Select(node => node.ToSave())
					.ToArray(),
				HotKey,
				Keys,
				Mode,
				Name);
		}

		public static ScriptEditorViewModel FromSave(ScriptSave script) {
			return new() {
				HotKey = script.Key,
				Keys = script.Keys,
				Mode = script.Mode,
				Name = script.Name,
				Nodes = new AvaloniaList<INode>(script.Nodes.Select(node => node.FromSave())),
			};
		}
	}

	[Serializable]
	public class ScriptSave {
		public ScriptSave(INodeSave[] nodes, HotKey key, string keys, ScriptMode mode, string name) {
			Nodes = nodes;
			Key = key;
			Keys = keys;
			Mode = mode;
			Name = name;
		}

		public INodeSave[] Nodes { get; set; }

		public HotKey Key { get; set; }

		public string Keys { get; set; }

		public ScriptMode Mode { get; set; }

		public string Name { get; set; }
	}
}