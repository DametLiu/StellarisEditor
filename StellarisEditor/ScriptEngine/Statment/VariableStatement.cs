using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.ScriptEngine
{
    /// <summary>
    /// 变量定义
    /// </summary>
    public class VariableStatement : Statement
    {
        /// <summary>
        /// 变量表达式
        /// </summary>
        public Expression Key { get; set; }

        /// <summary>
        /// 赋值表达式
        /// </summary>
        public Expression Equal { get; set; }

        public Expression Value { get; set; }

        public override string ToString()
        {
            return $"{Key} {Equal} {Value}";
        }
    }
}
