using System;

namespace GlobalActions.Models {
  [Serializable]
  public class ObservableModel {
    protected void RaiseAndSet<T>(ref T field, T value, Action action) {
      field = value;
      action();
    }
  }
}
