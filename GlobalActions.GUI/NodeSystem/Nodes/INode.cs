using System;
using GlobalActions.Models;

namespace GlobalActions.GUI.NodeSystem.Nodes {
	public interface INode : ICloneable {
		public Node ToNode();

		public INodeSave ToSave();
	}
}