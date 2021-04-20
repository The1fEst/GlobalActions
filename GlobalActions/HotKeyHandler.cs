using System;
using System.Collections.Generic;
using System.Linq;
using GlobalActions.Models;

namespace GlobalActions {
    public class HotKeyHandler {
        private static readonly List<HotKey> HotKeys = new();

        public static bool GetHotKey<T>(out T outHotKey) where T : struct {
            outHotKey = default;
            try {
                var hotkey = HotKeys.FirstOrDefault(x => x.IsDown);

                if (hotkey == null) return true;

                outHotKey = (T) Convert.ChangeType(hotkey.Id, typeof(T));
                return true;
            }
            catch {
                return true;
            }
        }

        public static void RegisterHotKey<T>(T id, int key, params int[] modifiers) where T : struct {
            if (HotKeys.Any(x => x.Id.Equals(id))) return;
            HotKeys.Add(new HotKey(id, key, modifiers));
        }

        public static void UnregisterHotKey<T>(T id) where T : struct {
            var hotKey = HotKeys.FirstOrDefault(x => x.Id.Equals(id));

            if (hotKey == null) {
                return;
            }

            HotKeys.Remove(hotKey);
        }

        public static void UnregisterAllHotKeys() {
            HotKeys.Clear();
        }

        private class HotKey : KeyState {
            public HotKey(object id, int key, params int[] modifiers) : base(key, modifiers) {
                Id = id;
            }

            public object Id { get; }
        }
    }
}