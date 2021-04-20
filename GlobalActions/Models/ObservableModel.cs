using System;
using System.ComponentModel;

namespace GlobalActions.Models {
    public class ObservableModel {
        public void RaiseAndSet<T>(ref T field, T value, Action action) {
            field = value;
            action();
        }
    }
}