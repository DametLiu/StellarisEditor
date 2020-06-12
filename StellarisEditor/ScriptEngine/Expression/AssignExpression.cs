using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.ScriptEngine
{
    /// <summary>
    /// 赋值表达式
    /// </summary>
    public class AssignExpression : Expression
    {
        /// <summary>
        /// 赋值的目标
        /// </summary>
        public Expression Target { get; set; }

        /// <summary>
        /// 赋值表达式
        /// </summary>
        public Expression Express { get; set; }
    }
}
