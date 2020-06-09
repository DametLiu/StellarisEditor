using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.pdx.scriptobject
{
    public class PdxTrigger
    {
        public String Key;
        public Boolean IsConditionTrigger;
        public LinkedList<PdxTrigger> Children;
    }
}
