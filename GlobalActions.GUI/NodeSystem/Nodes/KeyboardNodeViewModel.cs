using System;
using System.Linq;
using Avalonia.Collections;
using GlobalActions.Models;
using ReactiveUI;

namespace GlobalActions.GUI.NodeSystem.Nodes {
  public class KeyboardNodeViewModel : ReactiveObject {
    private int _delayAfter;

    private int _delayBefore;

    private InputType _inputType;

    private string _key = "";

    public AvaloniaList<byte> Keys = new();

    public int DelayBefore {
      get => _delayBefore;
      set => this.RaiseAndSetIfChanged(ref _delayBefore, value);
    }

    public int DelayAfter {
      get => _delayAfter;
      set => this.RaiseAndSetIfChanged(ref _delayAfter, value);
    }

    public string Key {
      get => _key;
      set => this.RaiseAndSetIfChanged(ref _key, value);
    }

    public AvaloniaList<InputType> InputTypes =>
      new(Enum.GetValues(typeof(InputType)).Cast<InputType>());

    public InputType InputType {
      get => _inputType;
      set => this.RaiseAndSetIfChanged(ref _inputType, value);
    }

    public void SetKeys() {
      Key = string.Join('+', Keys.Select(x => (Keys) x));
    }
  }
}
