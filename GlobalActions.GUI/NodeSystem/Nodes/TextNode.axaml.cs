using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GlobalActions.Models.Actions;

namespace GlobalActions.GUI.NodeSystem.Nodes {
	public class TextNode : UserControl, INode {
		private readonly TextNodeViewModel _vm;

		public TextNode() {
			DataContext = _vm = new TextNodeViewModel();
			InitializeComponent();
		}

		public TextNode(TextNodeViewModel vm) {
			DataContext = _vm = new TextNodeViewModel {
				DelayAfter = vm.DelayAfter,
				DelayBefore = vm.DelayBefore,
				Text = vm.Text,
			};

			InitializeComponent();
		}

		public object Clone() {
			return new TextNode(_vm);
		}

		public IAction ToAction() {
			return new TextAction {
				Text = _vm.Text,
				DelayBefore = _vm.DelayBefore,
				DelayAfter = _vm.DelayAfter,
			};
		}

		public INodeSave ToSave() {
			return new TextNodeSave {
				DelayAfter = _vm.DelayAfter,
				DelayBefore = _vm.DelayBefore,
				Text = _vm.Text,
			};
		}

		private void InitializeComponent() {
			AvaloniaXamlLoader.Load(this);
		}
	}
}