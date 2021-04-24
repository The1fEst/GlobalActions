using System;
using GlobalActions.Models.Actions;

namespace GlobalActions.Models {
    public class Node : ObservableModel {
        public IAction Action { get; set; } = null!;
    }
}