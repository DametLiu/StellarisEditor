using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.ScriptEngine
{
    /// <summary>
    /// 脚本标识，用来标注行列
    /// </summary>
    public class ScriptLinePragma
    {
        /// <summary>
        /// 当前行数
        /// </summary>
        public int Row { get; set; }
        /// <summary>
        /// 当前列数
        /// </summary>
        public int Col { get; set; }
    }
}
