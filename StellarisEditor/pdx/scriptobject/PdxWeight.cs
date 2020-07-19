using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.pdx.scriptobject
{
    public class PdxWeight
    {
        public PdxNumber Factor;
        public PdxNumber Weight;
        public LinkedList<WeightModifier> Modifier;
    }
}
