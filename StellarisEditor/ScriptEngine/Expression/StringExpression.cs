using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.ScriptEngine
{
    public class StringExpression : Expression
    {
        public String Key { get; set; }

        public override string ToString()
        {
            return Key;
        }
    }
}
