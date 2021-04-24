using System;
using Avalonia.Collections;
using GlobalActions.Models;

namespace GlobalActions.GUI.NodeSystem.Nodes {
	[Serializable]
	public class KeyboardNodeSave : INodeSave {
		public int DelayBefore { get; set; }

		public int DelayAfter { get; set; }

		public byte[] HotKeys { get; set; } = new byte[0];

		public InputType InputType { get; set; }

		public string HotKey { get; set; } = "";

		public INode FromSave() {
			return new KeyboardNode(new KeyboardNodeViewModel {
				DelayAfter = DelayAfter,
				DelayBefore = DelayBefore,
				HotKey = HotKey,
				InputType = InputType,
				HotKeys = new AvaloniaList<byte>(HotKeys),
			});
		}
	}
}