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
    public class PdxArmy:PdxObject
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

        private String _Defensive;
        public String Defensive
        {
            get
            {
                return _Defensive;
            }
            set
            {
                _Defensive = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Defensive"));
            }
        }
        private String _IsPopSpawned;
        public String IsPopSpawned
        {
            get
            {
                return _IsPopSpawned;
            }
            set
            {
                _IsPopSpawned = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsPopSpawned"));
            }
        }

        private String _Damage;
        public String Damage
        {
            get
            {
                return _Damage;
            }
            set
            {
                _Damage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Damage"));
            }
        }


        private String _Health;
        public String Health
        {
            get
            {
                return _Health;
            }
            set
            {
                _Health = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Health"));
            }
        }


        private String _Moracle;
        public String Moracle
        {
            get
            {
                return _Moracle;
            }
            set
            {
                _Moracle = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Moracle"));
            }
        }


        private String _MoracleDamage;
        public String MoracleDamage
        {
            get
            {
                return _MoracleDamage;
            }
            set
            {
                _MoracleDamage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("MoracleDamage"));
            }
        }

        private String _CollateralDamage;
        public String CollateralDamage
        {
            get
            {
                return _CollateralDamage;
            }
            set
            {
                _CollateralDamage = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("CollateralDamage"));
            }
        }

        private String _WarExhaustion;
        public String WarExhaustion
        {
            get
            {
                return _WarExhaustion;
            }
            set
            {
                _WarExhaustion = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("WarExhaustion"));
            }
        }

        private String _Time;
        public String Time
        {
            get
            {
                return _Time;
            }
            set
            {
                _Time = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Time"));
            }
        }

        private String _IconFrame;
        public String IconFrame
        {
            get
            {
                return _IconFrame;
            }
            set
            {
                _IconFrame = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IconFrame"));
            }
        }

        private ObservableCollection<Expression> _ShowTechUnlockIf;
        public ObservableCollection<Expression> ShowTechUnlockIf
        {
            get
            {
                return _ShowTechUnlockIf;
            }
            set
            {
                _ShowTechUnlockIf = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ShowTechUnlockIf"));
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
