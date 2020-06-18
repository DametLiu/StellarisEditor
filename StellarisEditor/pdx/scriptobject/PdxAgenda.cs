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
    public class PdxAgenda
    {
        public new event PropertyChangedEventHandler PropertyChanged;

        public static PdxAgenda Parse(ObjectStatement objectStatement)
        {
            PdxAgenda agenda = new PdxAgenda { Key = objectStatement.Key };

            foreach (var item in objectStatement.Statements)
            {
                if (item is ObjectStatement oi)
                {
                    if (oi.Key == "weight_modifier" && oi.Statements.Count > 0) 
                    {
                       
                        
                    }
                }
            }
            return agenda;
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

        private LinkedList<PdxModifier> _Modifier;
        public LinkedList<PdxModifier> Modifier
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
