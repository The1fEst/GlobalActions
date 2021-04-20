using System.Linq;
using static GlobalActions.Win32Interop;

namespace GlobalActions.Models {
    public class KeyState {
        private readonly int[] _modifiers;

        public KeyState(int key, params int[] modifiers) {
            _modifiers = modifiers;
            Key = key;
        }

        private int Key { get; }
        private bool State { get; set; }

        public bool IsDown => GetState() == KeyStates.Down;

        private KeyStates GetKeyState(int key) {
            if (key == 0) return KeyStates.None;

            var state = GetAsyncKeyState(key);
            var down = state == -32767;
            var hold = state == -32768;
            var release = state == 0;

            if (down && !State) {
                State = true;
                return KeyStates.Down;
            }

            if ((hold || down) && State) return KeyStates.Hold;

            if (!release || !State) return KeyStates.None;

            State = false;
            return KeyStates.Release;
        }

        public KeyStates GetState() {
            var keyState = GetKeyState(Key);
            var modifiersState = _modifiers.Select(GetKeyState).ToList();

            if (!modifiersState.Any() || modifiersState.All(x => x is KeyStates.Down or KeyStates.Hold))
                return keyState;

            return KeyStates.None;
        }
    }
}