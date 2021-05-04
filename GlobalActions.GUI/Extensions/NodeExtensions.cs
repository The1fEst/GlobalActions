using System;
using GlobalActions.GUI.NodeSystem.Nodes;
using GlobalActions.Models;
using GlobalActions.Models.Actions;

namespace GlobalActions.GUI.Extensions {
	public static class NodeExtensions {
		public static INode FromAction(this IAction action) {
			return action switch {
				DelayAction delayAction => throw new NotImplementedException(),
				KeyboardAction keyboardAction => new KeyboardNode(new() {
					Keys = new(keyboardAction.Keys),
					DelayAfter = keyboardAction.DelayAfter,
					DelayBefore = keyboardAction.DelayBefore,
					InputType = keyboardAction.InputType,
				}),
				MouseAction mouseAction => new MouseNode(new() {
					Key = mouseAction.Key,
					DelayAfter = mouseAction.DelayAfter,
					DelayBefore = mouseAction.DelayBefore,
					InputType = mouseAction.InputType,
				}),
				RepeatAction repeatAction => new RepeatNode(new() {
					DelayAfter = repeatAction.DelayAfter,
					DelayBefore = repeatAction.DelayBefore,
					RepeatCount = repeatAction.RepeatCount,
					SelectedNode = repeatAction.Action?.FromAction(),
				}),
				TextAction textAction => new TextNode(new() {
					Text = textAction.Text,
					DelayAfter = textAction.DelayAfter,
					DelayBefore = textAction.DelayBefore,
				}),
				_ => throw new ArgumentOutOfRangeException(nameof(action))
			};
		}
	}
}