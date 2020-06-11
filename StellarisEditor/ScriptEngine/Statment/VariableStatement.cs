using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.ScriptEngine.Statment
{
    /// <summary>
    /// 变量定义
    /// </summary>
    public class VariableStatement
    {
        /// <summary>
        /// 变量表达式
        /// </summary>
        public ScriptVariableExpression Expression { get; set; }
    }
}
