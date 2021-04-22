using Avalonia;
using Avalonia.Controls;
using Avalonia.Markup.Xaml;

namespace GlobalActions.GUI.NodeSystem.Nodes {
    public class KeyboardNode : UserControl, INode {
        private readonly KeyboardNodeViewModel _vm;

        public KeyboardNode() {
            DataContext = _vm = new();
            InitializeComponent();
        }

        public KeyboardNode(KeyboardNodeViewModel vm) {
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
            return new KeyboardNode(_vm);
        }
    }
}