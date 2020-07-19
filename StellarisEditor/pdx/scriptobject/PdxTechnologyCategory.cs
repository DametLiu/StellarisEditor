using StellarisEditor.ScriptEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.pdx.scriptobject
{
    public class PdxTechnologyCategory : PdxObject
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

        public String _Icon;
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

        public override bool Equals(object obj) => obj is PdxTechnologyCategory category && Key == category.Key && Icon == category.Icon;

        public override int GetHashCode() => base.GetHashCode();

        public override string ToString() => $"{Key} = {{ {(String.IsNullOrWhiteSpace(Icon) ? "" : "icon = " + Icon)} }}";

        public PdxTechnologyCategory Clone() => new PdxTechnologyCategory() { Key = Key, Icon = Icon, IsModData = IsModData };
    }
}
