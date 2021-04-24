using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using static GlobalActions.Win32Interop;

namespace GlobalActions.Models.Actions {
    public class KeyboardAction : IAction {
        public List<byte> Keys { get; set; }

        public int DelayBefore { get; set; }
        public int DelayAfter { get; set; }

        public void RunAction() {
            Thread.Sleep(DelayBefore);

            foreach (var key in Keys) {
                keybd_event(key, key, 0, 0);
                Console.WriteLine($"down {key}");
            }

            foreach (var key in Keys) {
                keybd_event(key, key, KEYEVENTF.KEYEVENTF_KEYUP, 0);
                Console.WriteLine($"up {key}");
            }

            Thread.Sleep(DelayAfter);
        }
    }
}