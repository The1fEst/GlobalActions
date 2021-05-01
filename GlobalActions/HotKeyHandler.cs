using System;
using System.Collections.Generic;
using System.Linq;
using GlobalActions.Models;
using static GlobalActions.Win32Interop;

namespace GlobalActions {
	public static class HotKeyHandler {
		private static readonly List<HotKeyState> HotKeys = new();

		public static HotKeyState? HasKey(int key) {
			return HotKeys.FirstOrDefault(x => x.Key == key && x.ModifiersIsDown());
		}

		public static void TryRunAction(HotKeyState hotKey, WM keyState) {
			if (hotKey.GetKeyState(keyState) == KeyStates.Down) {
				hotKey.Action();
			}
		}

		public static KeyStates GetState(int key) {
			return HotKeys.FirstOrDefault(x => x.Key == key)?.CurrentState ?? KeyStates.None;
		}

		public static void RegisterHotKey(int id, Action action, int key, params int[] modifiers) {
			if (HotKeys.Any(x => x.Id == id || x.Key == key) || key == 0) {
				return;
			}

			HotKeys.Add(new HotKeyState(id, action, key, modifiers));
		}

		public static void UnregisterHotKey(int id) {
			var hotKey = HotKeys.FirstOrDefault(x => x.Id == id);

			if (hotKey == null) {
				return;
			}

			HotKeys.Remove(hotKey);
		}

		public static void UnregisterAllHotKeys() {
			HotKeys.Clear();
		}
	}

	public class HotKeyState : KeyState {
		public HotKeyState(int id, Action action, int key, params int[] modifiers) : base(key, modifiers) {
			Id = id;
			Action = action;
		}

		public int Id { get; set; }

		public Action Action { get; set; }
	}
}