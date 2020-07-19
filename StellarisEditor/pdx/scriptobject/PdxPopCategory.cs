using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.pdx.scriptobject
{
    public class PdxPopCategory
    {
        public String Key;
        public PdxNumber Rank;
        public PdxNumber ClothesTextureIndex;
        public PdxNumber ChangeJobThreshold;
        public PdxBoolean KeepFromFormerJob;
        public PdxNumber DemotionTime;
        public LinkedList<Modifier> PopModifier;
        public PdxResources Resources;
        public PdxTriggeredPlanetModifier TriggeredPlanetModifier;
        public LinkedList<Expression> ShouldApplyUnemploymentPenalties;
        public LinkedList<Modifier> UnemploymentPenalties;
        public PdxResources UnemploymentResources;
        public LinkedList<Expression> AssignToPop;
        public LinkedList<Expression> AllowResettlement;
        public PdxResourceCost ResettlementCost;
        public PdxWeight Weight;
    }
}
