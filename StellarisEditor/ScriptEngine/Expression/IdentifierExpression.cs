using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.ScriptEngine
{
    /// <summary>
    /// 标识符表达式
    /// </summary>
    public class IdentifierExpression : Expression
    {
        public override string ToString()
        {
            return Content;
        }
    }
}
