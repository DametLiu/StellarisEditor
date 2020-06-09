using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.pdx.scriptobject
{
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
        public LinkedList<PdxModifier> PlanetModifier;
        public LinkedList<PdxModifier> CountryModifier;
        public LinkedList<PdxTriggeredPlanetModifier> TriggeredPlanetModifier;
        public LinkedList<PdxTriggeredPlanetModifier> TriggeredCountryModifier;
        public LinkedList<PdxModifier> PopModifier;
        public PdxWeight Weight;
    }
}
