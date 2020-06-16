using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.pdx.scriptobject
{
    public class PdxAgenda
    {
        public String Key;
        public LinkedList<PdxModifier> Modifier;
        public PdxWeightModifier WeightModifier;
        public PdxLocalization Localization;
    }
}
