using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GlobalActions.Models;
using GlobalActions.Models.Actions;
using static GlobalActions.Win32Interop;

namespace GlobalActions.GUI.NodeSystem.Nodes {
  public class MouseMoveNode : UserControl, INode {
    private readonly MouseMoveNodeViewModel _vm;

    private bool _waitingToPos;

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

    private void GetMousePosition(object? sender, RoutedEventArgs e) {
      if (_waitingToPos) {
        return;
      }

      _waitingToPos = true;
      InterceptKeys.KeyDown += GetMousePositionInternal;
    }

    private void GetMousePositionInternal(int key) {
      if (key != (int) Keys.Z || GetAsyncKeyState((int) Keys.LAlt) == 0) {
        return;
      }

      GetCursorPos(out var point);

      _vm.Horizontal = point.X;
      _vm.Vertical = point.Y;

      InterceptKeys.KeyDown -= GetMousePositionInternal;
      _waitingToPos = false;
    }
  }
}
