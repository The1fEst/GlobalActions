using System;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GlobalActions.Models;

namespace GlobalActions.GUI.NodeSystem.Nodes {
	public class MouseNode : UserControl, INode {
		private readonly MouseNodeViewModel _vm;

		public MouseNode() {
			DataContext = _vm = new MouseNodeViewModel();
			InitializeComponent();
		}

		public MouseNode(MouseNodeViewModel vm) {
			DataContext = _vm = new MouseNodeViewModel {
				DelayAfter = vm.DelayAfter,
				DelayBefore = vm.DelayBefore,
				Key = vm.Key,
			};

			InitializeComponent();
		}

		public object Clone() {
			return new MouseNode(_vm);
		}

		public Node ToNode() {
			throw new NotImplementedException();
		}

		public INodeSave ToSave() {
			throw new NotImplementedException();
		}

		private void InitializeComponent() {
			AvaloniaXamlLoader.Load(this);
		}
	}
}