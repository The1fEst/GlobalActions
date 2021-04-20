namespace GlobalActions.Models.Actions {
    public class RepeaterAction : IAction {
        public int Count { get; set; }

        public Node? Node { get; set; }
        
        public void RunAction() {
            throw new System.NotImplementedException();
        }
    }
}