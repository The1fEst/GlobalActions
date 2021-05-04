using System;
using System.Linq;
using Avalonia.Collections;
using ReactiveUI;

namespace GlobalActions.GUI.NodeSystem.Nodes {
	public class RepeatNodeViewModel : ReactiveObject {
		private int _delayAfter;

		private int _delayBefore;

		private int _repeatCount;

		private INode? _selectedNode;

		private Type? _selectedNodeType;

		public int DelayBefore {
			get => _delayBefore;
			set => this.RaiseAndSetIfChanged(ref _delayBefore, value);
		}

		public int DelayAfter {
			get => _delayAfter;
			set => this.RaiseAndSetIfChanged(ref _delayAfter, value);
		}

		public int RepeatCount {
			get => _repeatCount;
			set => this.RaiseAndSetIfChanged(ref _repeatCount, value);
		}

		public AvaloniaList<Type> AvailableNodes {
			get {
				var type = typeof(INode);
				var list = new AvaloniaList<Type>(AppDomain.CurrentDomain.GetAssemblies()
					.SelectMany(s => s.GetTypes())
					.Where(p => type.IsAssignableFrom(p) && p.IsClass)
					.Select(x => x));
				return list;
			}
		}

		public Type? SelectedNodeType {
			get => _selectedNodeType;

			set {
				this.RaiseAndSetIfChanged(ref _selectedNodeType, value);

				if (value != null) {
					SelectedNode = (INode?) Activator.CreateInstance(value);
				}
			}
		}

		public INode? SelectedNode {
			get => _selectedNode;
			set => this.RaiseAndSetIfChanged(ref _selectedNode, value);
		}
	}
}