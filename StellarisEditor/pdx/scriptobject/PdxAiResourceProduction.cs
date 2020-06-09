using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.pdx.scriptobject
{
    public class PdxAiResourceProduction
    {
        public Dictionary<PdxStrategicResource, PdxNumber> Resource;
        public LinkedList<PdxTrigger> Trigger;
    }
}
