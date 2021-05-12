using System;
using System.Threading;
using System.Threading.Tasks;
using Avalonia;
using static GlobalActions.Win32Interop;

namespace GlobalActions.Models.Actions {
  [Serializable]
  public class TextAction : IAction {
    public string Text { get; set; } = "";

    public int DelayBefore { get; set; }

    public int DelayAfter { get; set; }

    public void RunAction() {
      Thread.Sleep(DelayBefore);

      Task.Run(async () => {
        await Application.Current.Clipboard.SetTextAsync(Text);

        const ushort ctrl = (ushort) Keys.LCtrl;
        const ushort v = (ushort) Keys.LCtrl;

        key_down(ctrl);

        await Task.Delay(10);

        key_press(v);

        await Task.Delay(10);

        key_up(ctrl);
      });

      Thread.Sleep(DelayAfter);
    }
  }
}
