using System;

namespace GlobalActions.Models.Actions {
    public class TextAction : IAction {
        public string Text { get; set; } = "";

        public int DelayBefore { get; set; }
        public int DelayAfter { get; set; }

        public void RunAction() {
            throw new NotImplementedException();
        }
    }
}