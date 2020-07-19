using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.pdx.scriptobject
{
    /**
     * 建筑
     */
    public class PdxBuilding
    {
        public String Key;
        public PdxLocalization Localization;
        public PdxNumber BaseBuildtime;
        public PdxNumber BaseCapAmount;
        public PdxResources Resources;
        public PdxBuilding Icon;
        public PdxBoolean Capital;
        public PdxBoolean CanBuild;
        public PdxBoolean CanDemolish;
        public PdxBoolean CanBeDisabled;
        public PdxBoolean CanBeRuined;
        public PdxBoolean AddToFirstBuildingSlot;
        public PdxBoolean PlanetaryFtlInhibitor;
        public PdxBoolean BranchOfficeBuilding;
        public PdxBoolean IsCappedByModifier;
        public PdxBuildingCategory Category;
        public LinkedList<Technology> Prerequisites;
        public LinkedList<Expression> ShowTechUnlockIf;
        public LinkedList<Expression> Potential; 
        public LinkedList<Expression> Allow;
        public LinkedList<Expression> DestroyTrigger;
        public LinkedList<PdxBuilding> ConvertTo;
        public LinkedList<Modifier> PlanetModifier;
        public LinkedList<PdxTriggeredPlanetModifier> TriggeredPlanetModifier;
        public LinkedList<Modifier> CountryModifier;
        public LinkedList<PdxTriggeredPlanetModifier> TriggeredCountryModifier;
        public LinkedList<PdxTriggeredDesc> TriggeredDesc;
        public LinkedList<PdxBuilding> Upgrades;
        public LinkedList<PdxEffect> OnBuilt;
        public LinkedList<PdxEffect> OnDestroy;
        public LinkedList<PdxEffect> OnQueued;
        public LinkedList<PdxEffect> OnUnqueued;
        public PdxWeight AiWeight;
        public PdxAiResourceProduction AiResourceProduction;
    }
}
