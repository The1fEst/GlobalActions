namespace GlobalActions.Models.Actions {
    public class TextAction : IAction {
        public string Text { get; set; } = "";
        
        public void RunAction() {
            throw new System.NotImplementedException();
        }
    }
}