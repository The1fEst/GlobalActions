using System;
using System.Collections.Generic;

namespace GlobalActions.Models {
  [Serializable]
  public class HotKey {
    public int Key { get; set; }

    public List<int> Modifiers { get; set; } = new();
  }
}
