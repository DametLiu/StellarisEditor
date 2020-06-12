using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.ScriptEngine
{
    /// <summary>
    /// 赋值定义
    /// </summary>
    public class AssignmentStatement : Statement
    {
        public Expression Key { get; set; }
        public Expression Equal { get; set; }
        public Expression Value { get; set; }

        public override string ToString()
        {
            return $"{Key} {Equal} {Value}";
        }
    }
}
