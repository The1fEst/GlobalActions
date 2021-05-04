using System.Collections.Generic;
using System.IO;
using System.Linq;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GlobalActions.GUI.NodeSystem.Nodes;
using GlobalActions.Models;

namespace GlobalActions.GUI.NodeSystem {
	public class ScriptEditor : UserControl {
		private ScriptEditorViewModel _vm;

		public ScriptEditor() {
			DataContext = _vm = new ScriptEditorViewModel();

			InitializeComponent();
		}

		private void InitializeComponent() {
			AvaloniaXamlLoader.Load(this);
		}


		private void AppendNode(object? sender, RoutedEventArgs e) {
			if (_vm.SelectedNode != null) {
				_vm.Nodes.Add((INode) _vm.SelectedNode.Clone());
			}
		}

		private void Save(object? sender, RoutedEventArgs e) {
			var scriptsList = ScriptsList.Instance;

			scriptsList.Add(_vm.Name);
			scriptsList.Edit(_vm.Name, script => {
				script.Mode = _vm.Mode;
				script.HotKey = _vm.HotKey;
				script.IsActive = true;
				script.NodePipe = _vm.Nodes.Select(node => node.ToNode()).ToList();
			});
		}

		private void ClearHotKey() {
			_vm.Keys = Keys.None.ToString();
			_vm.HotKey.Key = 0;
			_vm.HotKey.Modifiers = new List<int>();
		}

		private void OnGotFocus(object? sender, GotFocusEventArgs e) {
			InterceptKeys.Run();
			_vm.HotKey = new HotKey();

			InterceptKeys.KeyDown += key => {
				if (key == (int) Keys.Delete) {
					ClearHotKey();
					return;
				}

				if ((Keys) key is Keys.LShiftKey or Keys.LControlKey or Keys.LMenu
				    && _vm.HotKey.Modifiers.All(x => x != key)) {
					_vm.HotKey.Modifiers.Add(key);
				}
				else {
					_vm.HotKey.Key = key;
				}

				_vm.Keys = string.Empty;

				if (_vm.HotKey.Modifiers.Any()) {
					_vm.Keys = string.Join('+', _vm.HotKey.Modifiers.Select(x => (Keys) x)) + "+";
				}

				if (_vm.HotKey.Key != 0) {
					_vm.Keys += (Keys) _vm.HotKey.Key;
				}
			};
		}

		private void OnLostFocus(object? sender, RoutedEventArgs e) {
			InterceptKeys.Stop();
			InterceptKeys.KeyDown = null;

			if (_vm.HotKey.Key == 0) {
				ClearHotKey();
			}
		}
	}
}