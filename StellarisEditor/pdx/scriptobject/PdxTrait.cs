using System;
using StellarisEditor.data;
using StellarisEditor.mod.data;
using StellarisEditor.ScriptEngine;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StellarisEditor.pdx.scriptobject
{
    public class PdxTrait:PdxObject
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

        public String _PotentialCrossBreedingChance;
        public String PotentialCrossBreedingChance
        {
            get
            {
                return _PotentialCrossBreedingChance;
            }
            set
            {
                _PotentialCrossBreedingChance = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("PotentialCrossBreedingChance"));
            }
        }

        private String _Initial;
        public String Initial
        {
            get
            {
                return _Initial;
            }
            set
            {
                _Initial = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("StartTech"));
            }
        }

        private String _Randomized;
        public String Randomized
        {
            get
            {
                return _Randomized;
            }
            set
            {
                _Randomized = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Randomized"));
            }
        }

        private String _Modification;
        public String Modification
        {
            get
            {
                return _Modification;
            }
            set
            {
                _Modification = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Modification"));
            }
        }

        private String _AdvancedTrait;
        public String AdvancedTrait
        {
            get
            {
                return _AdvancedTrait;
            }
            set
            {
                _AdvancedTrait = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AdvancedTrait"));
            }
        }

        public String _Customtooltip;
        public String Customtooltip
        {
            get
            {
                return _Customtooltip;
            }
            set
            {
                _Customtooltip = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Customtooltip"));
            }
        }

        public String _AllowedArchetypes;
        public String AllowedArchetypes
        {
            get
            {
                return _AllowedArchetypes;
            }
            set
            {
                _AllowedArchetypes = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("AllowedArchetypes"));
            }
        }

        public ObservableCollection<String> _LeaderTrait;
        public ObservableCollection<String> LeaderTrait
        {
            get
            {
                return _LeaderTrait;
            }
            set
            {
                _LeaderTrait = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LeaderTrait"));
            }
        }

        public ObservableCollection<String> _LeaderClass;
        public ObservableCollection<String> LeaderClass
        {
            get
            {
                return _LeaderClass;
            }
            set
            {
                _LeaderClass = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LeaderClass"));
            }
        }


        private ObservableCollection<String> _Opposites;
        public ObservableCollection<String> Opposites
        {
            get
            {
                return _Opposites;
            }
            set
            {
                _Opposites = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Opposites"));
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

        private ObservableCollection<Expression> _Modifier = new ObservableCollection<Expression>() { new pdx.scriptobject.Expression() { Key = "", Operator = "", Value = "" } };
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

        private ObservableCollection<Expression> _SlaveCost = new ObservableCollection<Expression>() { new pdx.scriptobject.Expression() { Key = "", Operator = "", Value = "" } };
        public ObservableCollection<Expression> SlaveCost
        {
            get
            {
                return _SlaveCost;
            }
            set
            {
                _SlaveCost = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("SlaveCost"));
            }
        }

        private ObservableCollection<Expression> _LeaderPotentialAdd;
        public ObservableCollection<Expression> LeaderPotentialAdd
        {
            get
            {
                return _LeaderPotentialAdd;
            }
            set
            {
                _LeaderPotentialAdd = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("LeaderPotentialAdd"));
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
}
