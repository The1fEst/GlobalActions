using System.Linq;
using static GlobalActions.Win32Interop;

namespace GlobalActions.Models {
	public class KeyState {
		public static readonly int[] DefaultModifiers = { 0xA0, 0xA1, 0xA2, 0xA3, 0xA4, 0xA5 };

		private readonly int[] _modifiers;

		protected KeyState(int key, params int[] modifiers) {
			_modifiers = modifiers;
			Key = key;
		}

		public int Key { get; }

		public KeyStates CurrentState { get; set; }

		private bool State { get; set; }

		public bool ModifiersIsDown() {
			return _modifiers.Any()
				? _modifiers.Select(x =>
						GetAsyncKeyState(x) != 0)
					.All(x => x)
				: DefaultModifiers.Select(x =>
						GetAsyncKeyState(x) != 0)
					.All(x => !x);
		}

		public KeyStates GetKeyState(WM state) {
			var down = !State && state == WM.WM_KEYDOWN;
			var hold = State && state == WM.WM_KEYDOWN;
			var release = State && state == WM.WM_KEYUP;

			var modifiersIsDown = ModifiersIsDown();

			if ((down || hold) && modifiersIsDown && !State) {
				State = true;
				return CurrentState = KeyStates.Down;
			}

			if ((hold || down) && modifiersIsDown && State) {
				return CurrentState = KeyStates.Hold;
			}

			if (!release || !State) {
				return CurrentState = KeyStates.None;
			}

			State = false;
			return CurrentState = KeyStates.Release;
		}
	}
}
