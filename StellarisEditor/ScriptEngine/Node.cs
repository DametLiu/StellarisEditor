using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.ScriptEngine
{
    /// <summary>
    /// 脚本节点对象
    /// </summary>
    public class Node
    {
        public string Key { get; set; }
        public string Operator { get; set; }
        public string Value { get; set; }

        public NodeCollection Nodes { get; private set; } = new NodeCollection();
    }
}
