using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.ScriptEngine
{
    /// <summary>
    /// 对象定义
    /// </summary>
    public class ObjectStatement : Statement
    {
        public string Key { get; set; }
        /// <summary>
        /// 对象集合
        /// </summary>
        public StatementCollection Statements { get; set; }

        public override string ToString()
        {
            return Content;
        }
    }
}
