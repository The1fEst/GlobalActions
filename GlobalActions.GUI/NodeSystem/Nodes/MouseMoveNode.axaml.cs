using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GlobalActions.Models.Actions;

namespace GlobalActions.GUI.NodeSystem.Nodes {
	public class MouseMoveNode : UserControl, INode {
		private readonly MouseMoveNodeViewModel _vm;

		public MouseMoveNode() {
			DataContext = _vm = new MouseMoveNodeViewModel();
			InitializeComponent();
		}

		public MouseMoveNode(MouseMoveNodeViewModel vm) {
			DataContext = _vm = new MouseMoveNodeViewModel {
				DelayAfter = vm.DelayAfter,
				DelayBefore = vm.DelayBefore,
				Horizontal = vm.Horizontal,
				Vertical = vm.Vertical,
				MouseMoveType = vm.MouseMoveType,
			};

			InitializeComponent();
		}

		public object Clone() {
			return new MouseMoveNode(_vm);
		}

		public IAction ToAction() {
			return new MouseMoveAction {
				DelayBefore = _vm.DelayBefore,
				DelayAfter = _vm.DelayAfter,
				Horizontal = _vm.Horizontal,
				Vertical = _vm.Vertical,
				MouseMoveType = _vm.MouseMoveType,
			};
		}

		private void InitializeComponent() {
			AvaloniaXamlLoader.Load(this);
		}
	}
}
