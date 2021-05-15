using System;
using System.Collections.Generic;
using System.Threading;
using static GlobalActions.Win32Interop;

namespace GlobalActions.Models.Actions {
  [Serializable]
  public class KeyboardAction : IAction {
    public List<byte> Keys { get; set; } = new();

    public InputType InputType { get; set; }

    public int DelayBefore { get; set; }

    public int DelayAfter { get; set; }

    public void RunAction() {
      Thread.Sleep(DelayBefore);

      foreach (var key in Keys) {
        switch (InputType) {
          case InputType.Down:
            key_down(key);
            break;
          case InputType.Up:
            key_up(key);
            break;
          case InputType.Press:
            key_press(key);
            break;
          default:
            throw new ArgumentOutOfRangeException();
        }
      }

      Thread.Sleep(DelayAfter);
    }
  }
}
