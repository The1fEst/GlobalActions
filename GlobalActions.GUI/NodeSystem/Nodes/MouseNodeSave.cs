using System;
using Avalonia.Input;
using GlobalActions.Models;

namespace GlobalActions.GUI.NodeSystem.Nodes {
	[Serializable]
	public class MouseNodeSave : INodeSave {
		public int DelayBefore { get; set; }

		public int DelayAfter { get; set; }

		public MouseButton Key { get; set; }

		public InputType InputType { get; set; }

		public INode FromSave() {
			return new MouseNode(new MouseNodeViewModel {
				DelayAfter = DelayAfter,
				DelayBefore = DelayBefore,
				Key = Key,
				InputType = InputType,
			});
		}
	}
}