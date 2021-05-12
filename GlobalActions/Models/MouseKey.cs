using System;
using Avalonia.Input;
using static GlobalActions.Win32Interop;

namespace GlobalActions.Models {
  public class MouseKey {
    public MouseKey(MouseButton mouseButton) {
      switch (mouseButton) {
        case MouseButton.None:
          break;
        case MouseButton.Left:
          MouseEventDown = MouseEventF.LeftDown;
          MouseEventUp = MouseEventF.LeftUp;
          break;
        case MouseButton.Right:
          MouseEventDown = MouseEventF.RightDown;
          MouseEventUp = MouseEventF.RightUp;
          break;
        case MouseButton.Middle:
          MouseEventDown = MouseEventF.MiddleDown;
          MouseEventUp = MouseEventF.MiddleUp;
          break;
        default:
          throw new ArgumentOutOfRangeException(nameof(mouseButton), mouseButton, null);
      }
    }

    public MouseEventF MouseEventDown { get; set; }

    public MouseEventF MouseEventUp { get; set; }
  }
}
