using System;
using System.Linq;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GlobalActions.Models;
using GlobalActions.Models.Actions;

namespace GlobalActions.GUI.NodeSystem.Nodes {
  [Serializable]
  public class KeyboardNode : UserControl, INode {
    private readonly KeyboardNodeViewModel _vm;

    public KeyboardNode() {
      DataContext = _vm = new KeyboardNodeViewModel {
        Key = Keys.None.ToString(),
      };

      InitializeComponent();
    }

    public KeyboardNode(KeyboardNodeViewModel vm) {
      DataContext = _vm = new KeyboardNodeViewModel {
        DelayAfter = vm.DelayAfter,
        DelayBefore = vm.DelayBefore,
        Keys = vm.Keys,
        InputType = vm.InputType,
      };

      _vm.SetKeys();

      InitializeComponent();
    }

    public object Clone() {
      return new KeyboardNode(_vm);
    }

    public IAction ToAction() {
      return new KeyboardAction {
        Keys = _vm.Keys.ToList(),
        InputType = _vm.InputType,
        DelayBefore = _vm.DelayBefore,
        DelayAfter = _vm.DelayAfter,
      };
    }

    private void InitializeComponent() {
      AvaloniaXamlLoader.Load(this);
    }

    private void OnGotFocus(object? sender, GotFocusEventArgs e) {
      _vm.Keys = new AvaloniaList<byte>();

      InterceptKeys.KeyDown += key => {
        if (key == (int) Keys.Delete) {
          _vm.Keys = new AvaloniaList<byte>();
          _vm.SetKeys();
          return;
        }

        _vm.Keys.Add((byte) key);
        _vm.SetKeys();
      };
    }

    private void OnLostFocus(object? sender, RoutedEventArgs e) {
      InterceptKeys.KeyDown = null;
    }
  }
}
