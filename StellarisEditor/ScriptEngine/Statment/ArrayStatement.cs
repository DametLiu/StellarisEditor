using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.ScriptEngine
{
    public class ArrayStatement : Statement
    {
        public ExpressionCollection Elements { get; set; }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var item in Elements)
            {
                sb.Append(item.ToString()).Append(" ");
            }
            return sb.ToString();
        }
    }
}
