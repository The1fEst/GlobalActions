using System;

namespace GlobalActions.Models.Actions {
    public class DelayAction : IAction {
        public TimeSpan Delay { get; set; }
        
        public void RunAction() {
            throw new System.NotImplementedException();
        }
    }
}