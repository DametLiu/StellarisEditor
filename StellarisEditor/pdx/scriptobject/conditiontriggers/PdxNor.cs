using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.pdx.scriptobject.conditiontriggers
{
    public class PdxNor : PdxConditionTrigger
    {
        public new String Key = "NOR";

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append($"{Key} = {{\n");
            foreach (PdxTrigger trigger in Children)
            {
                sb.Append(trigger.ToString());
            }
            sb.Append($"}}\n");
            return base.ToString();
        }
    }
}
