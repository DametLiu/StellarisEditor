using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.pdx.scriptobject
{
    public class WeightModifier : PdxObject
    {
        public new event PropertyChangedEventHandler PropertyChanged;

        private String _Factor;
        public String Factor
        {
            get
            {
                return _Factor;
            }
            set
            {
                _Factor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Factor"));
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


        private ObservableCollection<Modifier> _Modifiers = new ObservableCollection<Modifier>();
        public ObservableCollection<Modifier> Modifiers
        {
            get
            {
                return _Modifiers;
            }
            set
            {
                _Modifiers = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Modifiers"));
            }
        }
        

        public class Modifier : PdxObject
        {
            public new event PropertyChangedEventHandler PropertyChanged;

            private String _Factor;
            public String Factor
            {
                get
                {
                    return _Factor;
                }
                set
                {
                    _Factor = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Factor"));
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

            private ObservableCollection<Expression> _Triggers = new ObservableCollection<Expression>();
            public ObservableCollection<Expression> Triggers
            {
                get
                {
                    return _Triggers = new ObservableCollection<Expression>();
                }
                set
                {
                    _Triggers = value;
                    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Triggers = new ObservableCollection<Trigger>()"));
                }
            }


        }
    }
}
