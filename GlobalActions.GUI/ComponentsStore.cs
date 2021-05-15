using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia.Controls;

namespace GlobalActions.GUI {
  public static class ComponentsStore {
    private static readonly List<ContentControl> Components = new();

    public static T? Get<T>() {
      return (T?) Convert.ChangeType(Components.FirstOrDefault(c => c.GetType() == typeof(T)), typeof(T));
    }

    public static void Add(ContentControl component) {
      Components.Add(component);
    }
  }
}
