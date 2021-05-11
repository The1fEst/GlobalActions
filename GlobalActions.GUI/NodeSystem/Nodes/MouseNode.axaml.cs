using Avalonia.Controls;
using Avalonia.Markup.Xaml;
using GlobalActions.Models.Actions;

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

    public IAction ToAction() {
      return new MouseAction {
        Key = _vm.Key,
        InputType = _vm.InputType,
        DelayBefore = _vm.DelayBefore,
        DelayAfter = _vm.DelayAfter,
      };
    }

    private void InitializeComponent() {
      AvaloniaXamlLoader.Load(this);
    }
  }
}
