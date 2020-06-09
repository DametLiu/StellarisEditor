using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.pdx.scriptobject
{
    /// <summary>
    /// 人口职业
    /// </summary>
    class PdxPopJob
    {
        public string Key;
        public PdxBoolean IsCappedByModifier;
        public PdxPopCategory Category;
        public string ConditionString;
        public string BuildingIcon;
        public PdxNumber ClothesTextureIndex;
        public string Icon;
        public LinkedList<PdxTrigger> PossiblePreTriggers;
        public LinkedList<PdxTrigger> Possible;
        public PdxResources Resources;

        public LinkedList<PdxTriggeredPlanetModifier> TriggeredPlanetModifiers;
        public LinkedList<PdxTriggeredCountryModifier> TriggeredCountryModifiers;
        public LinkedList<PdxModifier> PopModifiers;
        public PdxWeight Weight;
    }
}
