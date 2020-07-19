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
        public LinkedList<Expression> PossiblePreTriggers;

        public LinkedList<Expression> Possible;
        public PdxResources Resources;
        public LinkedList<Modifier> PlanetModifier;
        public LinkedList<Modifier> CountryModifier;
        public LinkedList<PdxTriggeredPlanetModifier> TriggeredPlanetModifier;
        public LinkedList<PdxTriggeredPlanetModifier> TriggeredCountryModifier;
        public LinkedList<Modifier> PopModifier;
        public PdxWeight Weight;
    }
}
