using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static GlobalActions.Win32Interop;

namespace GlobalActions.Models.Actions {
    public class KeyboardAction : IAction {
        public List<byte> Keys { get; set; }

        public InputType InputType { get; set; }

        public int DelayBefore { get; set; }
        public int DelayAfter { get; set; }

        public void RunAction() {
            Thread.Sleep(DelayBefore);

            foreach (var key in Keys) {
                switch (InputType) {
                    case InputType.Down:
                        keybd_event(key, 0, 0, 0);
                        break;
                    case InputType.Up:
                        keybd_event(key, 0, KEYEVENTF.KEYEVENTF_KEYUP, 0);
                        break;
                    case InputType.Press:
                        keybd_event(key, 0, 0, 0);
                        keybd_event(key, 0, KEYEVENTF.KEYEVENTF_KEYUP, 0);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            Thread.Sleep(DelayAfter);
        }
    }
}