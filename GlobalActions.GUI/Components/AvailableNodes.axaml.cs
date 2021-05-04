using System;
using System.Linq;
using Avalonia;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GlobalActions.GUI.NodeSystem.Nodes;

namespace GlobalActions.GUI.Components {
	public class AvailableNodes : UserControl {
		public static readonly DirectProperty<AvailableNodes, AvaloniaList<INode>> NodesProperty =
			AvaloniaProperty.RegisterDirect<AvailableNodes, AvaloniaList<INode>>(
				nameof(Nodes),
				o => o.Nodes);

		public static readonly StyledProperty<INode?> SelectedNodeProperty =
			AvaloniaProperty.Register<AvailableNodes, INode?>(nameof(SelectedNode));

		public AvailableNodes() {
			InitializeComponent();
		}

		public AvaloniaList<INode> Nodes {
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
			get => GetValue(SelectedNodeProperty);
			set => SetValue(SelectedNodeProperty, value);
		}

		private void InitializeComponent() {
			AvaloniaXamlLoader.Load(this);
		}
	}
}