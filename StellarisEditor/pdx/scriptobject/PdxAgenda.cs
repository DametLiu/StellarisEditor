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
    public class PdxAgenda : PdxObject
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

        private LinkedList<Modifier> _Modifier;
        public LinkedList<Modifier> Modifier
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
