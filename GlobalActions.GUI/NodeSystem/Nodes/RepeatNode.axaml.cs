using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GlobalActions.GUI.NodeSystem.Nodes {
    public class RepeatNode : UserControl, INode {
        private readonly RepeatNodeViewModel _vm;

        public RepeatNode() {
            DataContext = _vm = new();
            InitializeComponent();
        }

        public RepeatNode(RepeatNodeViewModel vm) {
            DataContext = _vm = new() {
                DelayAfter = vm.DelayAfter,
                DelayBefore = vm.DelayBefore,
                Key = vm.Key,
            };

            InitializeComponent();
        }

        private void InitializeComponent() {
            AvaloniaXamlLoader.Load(this);
        }

        public object Clone() {
            return new RepeatNode(_vm);
        }
    }
}