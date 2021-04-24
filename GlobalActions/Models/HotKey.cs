using System.Collections.Generic;
using System.Linq;

namespace GlobalActions.Models {
    public class HotKey {
        public int Key { get; set; }
        public int[] Modifiers { get; set; } = new int[0];
    }
}