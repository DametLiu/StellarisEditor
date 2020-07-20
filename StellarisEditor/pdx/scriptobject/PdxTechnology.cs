using StellarisEditor.data;
using StellarisEditor.mod.data;
using StellarisEditor.ScriptEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StellarisEditor.pdx.scriptobject
{
    public class Technology : PdxObject
    {
        public new event PropertyChangedEventHandler PropertyChanged;

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


        private String _Icon;
        public String Icon
        {
            get
            {
                return _Icon;
            }
            set
            {
                _Icon = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Icon"));
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


        private String _Tier;
        public String Tier
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


        private ObservableCollection<String> _Category;
        public ObservableCollection<String> Category
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


        private ObservableCollection<String> _Prerequisites;
        public ObservableCollection<String> Prerequisites
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

        private ObservableCollection<Expression> _Modifier;
        public ObservableCollection<Expression> Modifier
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


        private PrereqforDesc _PrereqforDesc;
        public PrereqforDesc PrereqforDesc
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


        private ObservableCollection<Expression> _Potential;
        public ObservableCollection<Expression> Potential
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


        private WeightModifier _WeightModifier;
        public WeightModifier WeightModifier
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


        private WeightModifier _AiWeight;
        public WeightModifier AiWeight
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


        private ObservableCollection<WeightGroupPicked> _ModWeightIfGroupPicked;
        public ObservableCollection<WeightGroupPicked> ModWeightIfGroupPicked
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


        private String _IsRare;
        public String IsRare
        {
            get
            {
                return _IsRare;
            }
            set
            {
                _IsRare = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsRare"));
            }
        }


        private String _IsDangerous;
        public String IsDangerous
        {
            get
            {
                return _IsDangerous;
            }
            set
            {
                _IsDangerous = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsDangerous"));
            }
        }


        private String _IsReverseEngineerable;
        public String IsReverseEngineerable
        {
            get
            {
                return _IsReverseEngineerable;
            }
            set
            {
                _IsReverseEngineerable = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsReverseEngineerable"));
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

    public class WeightGroupPicked : PdxObject
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

        private String _Operator;
        public String Operator
        {
            get
            {
                return _Operator;
            }
            set
            {
                _Operator = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Operator"));
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

    public class PrereqforDesc : PdxObject
    {
        public new event PropertyChangedEventHandler PropertyChanged;

        private String _HidePrereqForDesc;
        public String HidePrereqForDesc
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


        private ObservableCollection<PrereqforDescCetegory> _Cotegories = new ObservableCollection<PrereqforDescCetegory>();
        public ObservableCollection<PrereqforDescCetegory> Cotegories
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

    public class PrereqforDescCetegory : PdxObject
    {
        public new event PropertyChangedEventHandler PropertyChanged;
        
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
