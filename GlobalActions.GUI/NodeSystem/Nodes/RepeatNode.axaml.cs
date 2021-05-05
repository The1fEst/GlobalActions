using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GlobalActions.Models.Actions;

namespace GlobalActions.GUI.NodeSystem.Nodes {
	public class RepeatNode : UserControl, INode {
		private readonly RepeatNodeViewModel _vm;

		public RepeatNode() {
			DataContext = _vm = new RepeatNodeViewModel();
			InitializeComponent();
		}

		public RepeatNode(RepeatNodeViewModel vm) {
			DataContext = _vm = new RepeatNodeViewModel {
				DelayAfter = vm.DelayAfter,
				DelayBefore = vm.DelayBefore,
				RepeatCount = vm.RepeatCount,
				SelectedNodeType = vm.SelectedNodeType,
				SelectedNode = (INode?) vm.SelectedNode?.Clone(),
			};

			InitializeComponent();
		}

		public object Clone() {
			return new RepeatNode(_vm);
		}

		public IAction ToAction() {
			return new RepeatAction {
				DelayBefore = _vm.DelayBefore,
				DelayAfter = _vm.DelayAfter,
				RepeatCount = _vm.RepeatCount,
				Action = _vm.SelectedNode?.ToAction(),
			};
		}

		private void InitializeComponent() {
			AvaloniaXamlLoader.Load(this);
		}
	}
}
