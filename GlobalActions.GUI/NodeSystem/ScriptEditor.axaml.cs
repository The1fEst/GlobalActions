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

		public void LoadFromFile(string name) {
			if (!Directory.Exists(Program.ScriptsDirectory)) {
				Directory.CreateDirectory(Program.ScriptsDirectory);

				DataContext = _vm = new ScriptEditorViewModel {
					Name = name,
				};

				return;
			}

			var filePath = Path.Combine(Program.ScriptsDirectory, name);
			if (!File.Exists(filePath)) {
				DataContext = _vm = new ScriptEditorViewModel {
					Name = name,
				};

				return;
			}

			var data = File.ReadAllBytes(filePath);
			var script = data.Deserializer<ScriptSave>();

			DataContext = _vm = ScriptEditorViewModel.FromSave(script);
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
				script.HotKey = new HotKey {Key = (int) _vm.Key};
				script.IsActive = true;
				script.NodePipe = _vm.Nodes.Select(node => node.ToNode()).ToList();
			});

			SaveToFile(_vm.Name);
		}

		private void SaveToFile(string name) {
			if (!Directory.Exists(Program.ScriptsDirectory)) {
				Directory.CreateDirectory(Program.ScriptsDirectory);
			}

			var filePath = Path.Combine(Program.ScriptsDirectory, name);
			if (!File.Exists(filePath)) {
				File.Create(filePath).Dispose();
			}

			var data = _vm.ToSave().Serialize();

			File.WriteAllBytes(filePath, data);
		}

		private void OnGotFocus(object? sender, GotFocusEventArgs e) {
			InterceptKeys.Run();

			InterceptKeys.KeyDown += key => { _vm.Key = (Keys) key; };
		}

		private void OnLostFocus(object? sender, RoutedEventArgs e) {
			InterceptKeys.Stop();
			InterceptKeys.KeyDown = null;
		}
	}
}