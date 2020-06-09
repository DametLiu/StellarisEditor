using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.pdx.scriptobject
{
    public class PdxWeightModifier
    {
        public PdxNumber Factor;
        public PdxNumber Weight;
        public LinkedList<PdxTrigger> Trigger;
    }
}
