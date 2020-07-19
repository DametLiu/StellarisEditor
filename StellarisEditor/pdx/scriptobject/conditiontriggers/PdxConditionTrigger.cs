using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.pdx.scriptobject.conditiontriggers
{
    public class PdxConditionTrigger : Expression
    {
        public new Boolean IsConditionTrigger = true;
    }

    public enum Operator
    {
        Greater, GreaterOrEqual, Equal, Less, LessOrEqual
    }
}
