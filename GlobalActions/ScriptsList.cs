using System;
using System.IO;
using System.Linq;
using Avalonia.Collections;
using GlobalActions.Models;

namespace GlobalActions {
	public class ScriptsList {
		private static ScriptsList? _instance;

		public AvaloniaList<Script> Scripts = new();

		public Script? SelectedScript;

		private ScriptsList() { }

		public static ScriptsList Instance => _instance ??= new ScriptsList();

		public void LoadScripts() {
			if (!Directory.Exists(Script.ScriptsDirectory)) {
				Directory.CreateDirectory(Script.ScriptsDirectory);
				return;
			}

			var files = Directory.GetFiles(Script.ScriptsDirectory)
				.Select(file => Path.GetFileName(file)!)
				.ToArray();

			foreach (var file in files) {
				var script = Script.LoadFromFile(file);

				if (script != null) {
					Add(script);
				}
			}
		}

		public void Add(string name) {
			if (Scripts.Any(x => x.Name == name)) {
				return;
			}

			var script = new Script(name) {
				Id = Scripts.Count + 1,
			};

			Scripts.Add(script);
		}

		public void Add(Script script) {
			var storedScript = GetScriptByName(script.Name) ?? new Script(script.Name) {
				Id = Scripts.Any() ? Scripts.Max(x => x.Id) + 1 : 1,
			};

			storedScript.Mode = script.Mode;
			storedScript.HotKey = script.HotKey;
			storedScript.IsActive = script.IsActive;
			storedScript.ActionPipe = script.ActionPipe;

			Scripts.Add(storedScript);
		}

		public void Remove(string name) {
			var script = GetScriptByName(name);

			if (script == null) {
				return;
			}

			Scripts.Remove(script);
		}

		public bool ToggleActive(string name) {
			var script = GetScriptByName(name);

			if (script == null) {
				return false;
			}

			script.IsActive = !script.IsActive;

			return script.IsActive;
		}

		public void Toggle(int id) {
			var script = GetScriptById(id);
			script.Toggle();
		}

		public void Edit(string name, Action<Script> edit) {
			var script = GetScriptByName(name);

			if (script == null) {
				return;
			}

			edit(script);
			script.SaveToFile();
		}

		private Script? GetScriptByName(string name) {
			var script = Scripts.FirstOrDefault(x => x.Name == name);

			if (script == null) {
				return null;
			}

			return script;
		}

		private Script GetScriptById(int id) {
			var script = Scripts.FirstOrDefault(x => x.Id == id);

			if (script == null) {
				throw new Exception();
			}

			return script;
		}
	}
}
