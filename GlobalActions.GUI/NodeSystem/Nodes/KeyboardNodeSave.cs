using System;
using System.Collections.Generic;

namespace GlobalActions.GUI.NodeSystem.Nodes {
    [Serializable]
    public class KeyboardNodeSave : INodeSave {
        public int DelayBefore { get; set; }
        public int DelayAfter { get; set; }
        public byte[] HotKeys { get; set; } = new byte[0];
        public string HotKey { get; set; } = "";
        
        public INode FromSave() {
            return new KeyboardNode(new() {
                DelayAfter = DelayAfter,
                DelayBefore = DelayBefore,
                HotKey = HotKey,
                HotKeys = new(HotKeys)
            });
        }
    }
}