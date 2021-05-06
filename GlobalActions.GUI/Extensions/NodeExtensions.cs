using System;
using Avalonia.Collections;
using GlobalActions.GUI.NodeSystem.Nodes;
using GlobalActions.Models.Actions;

namespace GlobalActions.GUI.Extensions {
  public static class NodeExtensions {
    public static INode FromAction(this IAction action) {
      return action switch {
        DelayAction delayAction => throw new NotImplementedException(),
        KeyboardAction keyboardAction => new KeyboardNode(new KeyboardNodeViewModel {
          Keys = new AvaloniaList<byte>(keyboardAction.Keys),
          DelayAfter = keyboardAction.DelayAfter,
          DelayBefore = keyboardAction.DelayBefore,
          InputType = keyboardAction.InputType,
        }),
        MouseAction mouseAction => new MouseNode(new MouseNodeViewModel {
          Key = mouseAction.Key,
          DelayAfter = mouseAction.DelayAfter,
          DelayBefore = mouseAction.DelayBefore,
          InputType = mouseAction.InputType,
        }),
        MouseMoveAction mouseMoveAction => new MouseMoveNode(new MouseMoveNodeViewModel {
          DelayAfter = mouseMoveAction.DelayAfter,
          DelayBefore = mouseMoveAction.DelayBefore,
          Horizontal = mouseMoveAction.Horizontal,
          Vertical = mouseMoveAction.Vertical,
          MouseMoveType = mouseMoveAction.MouseMoveType,
        }),
        RepeatAction repeatAction => new RepeatNode(new RepeatNodeViewModel {
          DelayAfter = repeatAction.DelayAfter,
          DelayBefore = repeatAction.DelayBefore,
          RepeatCount = repeatAction.RepeatCount,
          SelectedNode = repeatAction.Action?.FromAction(),
        }),
        TextAction textAction => new TextNode(new TextNodeViewModel {
          Text = textAction.Text,
          DelayAfter = textAction.DelayAfter,
          DelayBefore = textAction.DelayBefore,
        }),
        _ => throw new ArgumentOutOfRangeException(nameof(action)),
      };
    }
  }
}
