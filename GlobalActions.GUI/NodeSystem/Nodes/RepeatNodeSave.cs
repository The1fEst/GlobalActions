using System;

namespace GlobalActions.GUI.NodeSystem.Nodes {
	[Serializable]
	public class RepeatNodeSave : INodeSave {
		public int DelayBefore { get; set; }

		public int DelayAfter { get; set; }

		public INodeSave? SelectedNode { get; set; }

		public string? SelectedNodeType { get; set; } = "";

		public int RepeatCount { get; set; }

		public INode FromSave() {
			return new RepeatNode(new RepeatNodeViewModel {
				DelayAfter = DelayAfter,
				DelayBefore = DelayBefore,
				RepeatCount = RepeatCount,
				SelectedNodeType = SelectedNodeType != null
					? Type.GetType(SelectedNodeType)
					: null,
				SelectedNode = SelectedNode?.FromSave(),
			});
		}
	}
}