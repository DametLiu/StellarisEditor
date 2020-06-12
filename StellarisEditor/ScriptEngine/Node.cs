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
        /// <summary>
        /// 行列标注
        /// </summary>
        public LinePragma Pragma { get; set; } = new LinePragma() { Row = -1, Col = -1 };

        /// <summary>
        /// 子节点集合
        /// </summary>
        public NodeCollection Objects { get; } = new NodeCollection();

        public string Content { get; set; }
    }
}
