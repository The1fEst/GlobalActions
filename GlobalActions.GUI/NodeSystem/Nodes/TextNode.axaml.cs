using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GlobalActions.Models;

namespace GlobalActions.GUI.NodeSystem.Nodes {
    public class TextNode : UserControl, INode {
        private readonly TextNodeViewModel _vm;

        public TextNode() {
            DataContext = _vm = new();
            InitializeComponent();
        }

        public TextNode(TextNodeViewModel vm) {
            DataContext = _vm = new() {
                DelayAfter = vm.DelayAfter,
                DelayBefore = vm.DelayBefore,
                Text = vm.Text
            };

            InitializeComponent();
        }

        private void InitializeComponent() {
            AvaloniaXamlLoader.Load(this);
        }

        public object Clone() {
            return new TextNode(_vm);
        }

        public Node ToNode() {
            throw new System.NotImplementedException();
        }

        public INodeSave ToSave() {
            throw new System.NotImplementedException();
        }
    }
}