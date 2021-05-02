using System;
using Avalonia.Collections;
using Avalonia.Input;
using GlobalActions.Models;

namespace GlobalActions.GUI.NodeSystem.Nodes {
	[Serializable]
	public class TextNodeSave : INodeSave {
		public int DelayBefore { get; set; }

		public int DelayAfter { get; set; }

		public string Text { get; set; } = "";

		public INode FromSave() {
			return new TextNode(new TextNodeViewModel {
				DelayAfter = DelayAfter,
				DelayBefore = DelayBefore,
				Text = Text,
			});
		}
	}
}