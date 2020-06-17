using StellarisEditor.data;
using StellarisEditor.ScriptEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.pdx.scriptobject
{
    public class PdxTechnology : PdxObject
    {
        public new event PropertyChangedEventHandler PropertyChanged;

        public static PdxTechnology Parse(ObjectStatement objectStatement)
        {
            PdxTechnology technology = new PdxTechnology { Key = objectStatement.Key };

            foreach (var item in objectStatement.Statements)
            {
                if (item is AssignmentStatement ai)
                {
                    if (ai.Key.Content == "cost")
                        technology.Cost = ai.Value.Content;
                    else if (ai.Key.Content == "area")
                        technology.Area = ai.Value.Content;
                    
                }
                else if (item is ObjectStatement oi)
                {
                    if (oi.Key == "tier" && oi.Statements.Count > 0)
                    {
                        var t = oi.Statements.First() as AssignmentStatement;
                        technology.Tier = new PdxTechnologyTier() { Key = t.Key.Content, PreviouslyUnlocked = t.Value.Content };
                    }
                    else if (oi.Key == "category" && oi.Statements.Count > 0 && oi.Statements.First() is ArrayStatement ari)
                    {
                        foreach (var element in ari.Elements)
                        {
                            var a = PdxGlobalData.TechnologyCategories.Where(tc => tc.Key == element.Content);
                            if (a.Count() > 0)
                                technology.Category.Add(a.First());
                            else
                            {

                            }
                        }
                    }
                }
            }


            return technology;
        }

        private String _Key;
        public String Key
        {
            get
            {
                return _Key;
            }
            set
            {
                _Key = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Key"));
            }
        }

        public String _Cost;
        public String Cost
        {
            get
            {
                return _Cost;
            }
            set
            {
                _Cost = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Cost"));
            }
        }


        private String _Area;
        public String Area
        {
            get
            {
                return _Area;
            }
            set
            {
                _Area = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Area"));
            }
        }


        private PdxTechnologyTier _Tier;
        public PdxTechnologyTier Tier
        {
            get
            {
                return _Tier;
            }
            set
            {
                _Tier = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Tier"));
            }
        }


        private ObservableCollection<PdxTechnologyCategory> _Category;
        public ObservableCollection<PdxTechnologyCategory> Category
        {
            get
            {
                return _Category;
            }
            set
            {
                _Category = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Category"));
            }
        }


        private String _Levels;
        public String Levels
        {
            get
            {
                return _Levels;
            }
            set
            {
                _Levels = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Levels"));
            }
        }


        private String _CostPerLevel;
        public String CostPerLevel
        {
            get
            {
                return _CostPerLevel;
            }
            set
            {
                _CostPerLevel = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CostPerLevel"));
            }
        }


        private ObservableCollection<PdxTechnology> _Prerequisites;
        public ObservableCollection<PdxTechnology> Prerequisites
        {
            get
            {
                return _Prerequisites;
            }
            set
            {
                _Prerequisites = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Prerequisites"));
            }
        }


        private String _Weight;
        public String Weight
        {
            get
            {
                return _Weight;
            }
            set
            {
                _Weight = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Weight"));
            }
        }


        private String _Gateway;
        public String Gateway
        {
            get
            {
                return _Gateway;
            }
            set
            {
                _Gateway = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Gateway"));
            }
        }


        private String _AiUpdateType;
        public String AiUpdateType
        {
            get
            {
                return _AiUpdateType;
            }
            set
            {
                _AiUpdateType = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AiUpdateType"));
            }
        }


        private String _StartTech;
        public String StartTech
        {
            get
            {
                return _StartTech;
            }
            set
            {
                _StartTech = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StartTech"));
            }
        }


        private ObservableCollection<PdxModifier> _Modifier;
        public ObservableCollection<PdxModifier> Modifier
        {
            get
            {
                return _Modifier;
            }
            set
            {
                _Modifier = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Modifier"));
            }
        }


        private ObservableCollection<String> _FeatureFlags;
        public ObservableCollection<String> FeatureFlags
        {
            get
            {
                return _FeatureFlags;
            }
            set
            {
                _FeatureFlags = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FeatureFlags"));
            }
        }


        private PdxPrereqforDesc _PrereqforDesc;
        public PdxPrereqforDesc PrereqforDesc
        {
            get
            {
                return _PrereqforDesc;
            }
            set
            {
                _PrereqforDesc = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PrereqforDesc"));
            }
        }


        private ObservableCollection<PdxTrigger> _Potential;
        public ObservableCollection<PdxTrigger> Potential
        {
            get
            {
                return _Potential;
            }
            set
            {
                _Potential = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Potential"));
            }
        }


        private PdxWeightModifier _WeightModifier;
        public PdxWeightModifier WeightModifier
        {
            get
            {
                return _WeightModifier;
            }
            set
            {
                _WeightModifier = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WeightModifier"));
            }
        }


        private PdxWeightModifier _AiWeight;
        public PdxWeightModifier AiWeight
        {
            get
            {
                return _AiWeight;
            }
            set
            {
                _AiWeight = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AiWeight"));
            }
        }


        private ObservableCollection<String> _WeightGroups;
        public ObservableCollection<String> WeightGroups
        {
            get
            {
                return _WeightGroups;
            }
            set
            {
                _WeightGroups = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WeightGroups"));
            }
        }


        private ObservableCollection<PdxWeightGroupPicked> _ModWeightIfGroupPicked;
        public ObservableCollection<PdxWeightGroupPicked> ModWeightIfGroupPicked
        {
            get
            {
                return _ModWeightIfGroupPicked;
            }
            set
            {
                _ModWeightIfGroupPicked = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ModWeightIfGroupPicked"));
            }
        }



        private String _FileName;
        public String FileName
        {
            get
            {
                return _FileName;
            }
            set
            {
                _FileName = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("FileName"));
            }
        }
    }

    public class PdxWeightGroupPicked : PdxObject
    {
        public new event PropertyChangedEventHandler PropertyChanged;

        private String _Group;
        public String Group
        {
            get
            {
                return _Group;
            }
            set
            {
                _Group = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Group"));
            }
        }


        private String _Value;
        public String Value
        {
            get
            {
                return _Value;
            }
            set
            {
                _Value = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Value"));
            }
        }


    }

    public enum PrereqforDescCetegory
    {
        Ship, Component, Custom, DiploAction, Feature, Resource
    }

    public class PdxPrereqforDesc : PdxObject
    {
        public new event PropertyChangedEventHandler PropertyChanged;

        private PrereqforDescCetegory _HidePrereqForDesc;
        public PrereqforDescCetegory HidePrereqForDesc
        {
            get
            {
                return _HidePrereqForDesc;
            }
            set
            {
                _HidePrereqForDesc = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("HidePrereqForDesc"));
            }
        }


        private ObservableCollection<PdxPrereqforDescCetegory> _Cotegories;
        public ObservableCollection<PdxPrereqforDescCetegory> Cotegories
        {
            get
            {
                return _Cotegories;
            }
            set
            {
                _Cotegories = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Cotegories"));
            }
        }


    }

    public class PdxPrereqforDescCetegory : PdxObject
    {
        public new event PropertyChangedEventHandler PropertyChanged;

        private String _Title;
        public String Title
        {
            get
            {
                return _Title;
            }
            set
            {
                _Title = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Title"));
            }
        }

        private String _Desc;
        public String Desc
        {
            get
            {
                return _Desc;
            }
            set
            {
                _Desc = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Desc"));
            }
        }
    }
}
