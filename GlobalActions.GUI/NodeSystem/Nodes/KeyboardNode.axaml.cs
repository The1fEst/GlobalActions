using System;
using System.Linq;
using Avalonia.Collections;
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Avalonia.Markup.Xaml;
using GlobalActions.Models;
using GlobalActions.Models.Actions;

namespace GlobalActions.GUI.NodeSystem.Nodes {
	[Serializable]
	public class KeyboardNode : UserControl, INode {
		private readonly KeyboardNodeViewModel _vm;

		public KeyboardNode() {
			DataContext = _vm = new KeyboardNodeViewModel {
				HotKey = Keys.None.ToString(),
			};

			InitializeComponent();
		}

		public KeyboardNode(KeyboardNodeViewModel vm) {
			DataContext = _vm = new KeyboardNodeViewModel {
				DelayAfter = vm.DelayAfter,
				DelayBefore = vm.DelayBefore,
				HotKeys = vm.HotKeys,
				HotKey = vm.HotKey,
				InputType = vm.InputType,
			};

			InitializeComponent();
		}

		public object Clone() {
			return new KeyboardNode(_vm);
		}

		public Node ToNode() {
			return new() {
				Action = new KeyboardAction {
					Keys = _vm.HotKeys.ToList(),
					InputType = _vm.InputType,
					DelayBefore = _vm.DelayBefore,
					DelayAfter = _vm.DelayAfter,
				},
			};
		}

		public INodeSave ToSave() {
			return new KeyboardNodeSave {
				DelayAfter = _vm.DelayAfter,
				DelayBefore = _vm.DelayBefore,
				HotKey = _vm.HotKey,
				InputType = _vm.InputType,
				HotKeys = _vm.HotKeys.ToArray(),
			};
		}

		private void InitializeComponent() {
			AvaloniaXamlLoader.Load(this);
		}

		private void OnGotFocus(object? sender, GotFocusEventArgs e) {
			_vm.HotKeys = new AvaloniaList<byte>();

			InterceptKeys.KeyDown += key => {
				if (key == (int) Keys.Delete) {
					_vm.HotKey = Keys.None.ToString();
					return;
				}

				_vm.HotKeys.Add((byte) key);
				_vm.HotKey = string.Join('+', _vm.HotKeys.Select(x => (Keys) x));
			};
		}

		private void OnLostFocus(object? sender, RoutedEventArgs e) {
			InterceptKeys.KeyDown = null;
		}
	}
}