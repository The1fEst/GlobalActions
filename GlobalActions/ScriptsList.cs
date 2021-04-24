using System;
using System.Collections.Generic;
using System.Linq;
using GlobalActions.Models;

namespace GlobalActions {
	public class ScriptsList {
		private static ScriptsList? _instance;

		private readonly List<Script> _scripts = new();

		private ScriptsList() { }

		public static ScriptsList Instance => _instance ??= new ScriptsList();

		public void Add(string name) {
			var script = _scripts.FirstOrDefault(x => x.Name == name)
			             ?? new Script(name) {
				             Id = _scripts.Count + 1,
			             };

			_scripts.Add(script);
		}

		public void Remove(string name) {
			var script = GetScriptByName(name);

			_scripts.Remove(script);
		}

		public bool ToggleActive(string name) {
			var script = GetScriptByName(name);

			script.IsActive = !script.IsActive;

			return script.IsActive;
		}

		public void Toggle(int id) {
			var script = GetScriptById(id);
			script.Toggle();
		}

		public void Edit(string name, Action<Script> edit) {
			var script = GetScriptByName(name);
			edit(script);
		}

		private Script GetScriptByName(string name) {
			var script = _scripts.FirstOrDefault(x => x.Name == name);

			if (script == null) {
				throw new Exception();
			}

			return script;
		}

		private Script GetScriptById(int id) {
			var script = _scripts.FirstOrDefault(x => x.Id == id);

			if (script == null) {
				throw new Exception();
			}

			return script;
		}
	}
}