using System;
using System.Linq;
using Avalonia.Collections;
using Avalonia.Input;
using GlobalActions.Models;
using ReactiveUI;

namespace GlobalActions.GUI.NodeSystem.Nodes {
  public class MouseMoveNodeViewModel : ReactiveObject {
    private int _delayAfter;

    private int _delayBefore;

    private MouseMoveType? _mouseMoveType;

    public int DelayBefore {
      get => _delayBefore;
      set => this.RaiseAndSetIfChanged(ref _delayBefore, value);
    }

    public int DelayAfter {
      get => _delayAfter;
      set => this.RaiseAndSetIfChanged(ref _delayAfter, value);
    }

    public AvaloniaList<MouseMoveType> MouseMoveTypes =>
      new(Enum.GetValues(typeof(MouseMoveType)).Cast<MouseMoveType>());

    public MouseMoveType? MouseMoveType {
      get => _mouseMoveType;
      set => this.RaiseAndSetIfChanged(ref _mouseMoveType, value);
    }

    private int _horizontal;

    public int Horizontal {
      get => _horizontal;
      set => this.RaiseAndSetIfChanged(ref _horizontal, value);
    }

    private int _vertical;

    public int Vertical {
      get => _vertical;
      set => this.RaiseAndSetIfChanged(ref _vertical, value);
    }
  }
}
