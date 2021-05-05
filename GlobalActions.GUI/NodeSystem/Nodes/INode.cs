using System;
using GlobalActions.Models.Actions;

namespace GlobalActions.GUI.NodeSystem.Nodes {
	public interface INode : ICloneable {
		public IAction ToAction();
	}
}
