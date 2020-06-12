using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.ScriptEngine
{
    /// <summary>
    /// 脚本变量定义
    /// </summary>
    public class VariableExpression : Expression
    {
        public override string ToString()
        {
            return $"@{Content}";
        }
    }
}
