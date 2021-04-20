using System;
using GlobalActions.Models.Actions;

namespace GlobalActions.Models {
    public class Node : ObservableModel {
        private ActionType _actionType;

        public ActionType ActionType {
            get => _actionType;
            set => RaiseAndSet(ref _actionType, value, Initialize);
        }

        public IAction Action { get; set; } = null!;

        public Node? NextNode { get; set; }

        private void Initialize() {
            Action = ActionType switch {
                ActionType.Keyboard => new KeyboardAction(),
                ActionType.Mouse => new MouseAction(),
                ActionType.Text => new TextAction(),
                ActionType.Repeater => new RepeaterAction(),
                ActionType.Delay => new DelayAction(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }
    }
}