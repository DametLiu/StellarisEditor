using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.pdx.scriptobject
{
    public class PdxVariable : PdxObject
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

        private Double _Value;
        public Double Value
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

        private PdxVariable _Reference;
        public PdxVariable Reference
        {
            get
            {
                return _Reference;
            }
            set
            {
                _Reference = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Reference"));
            }
        }

        public bool IsDouble() => Value % 1 != 0;

        public bool IsInt() => Value % 1 == 0;

        public bool IsReference() => Reference != null;

        

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            if (Reference is PdxVariable variable)
                return $"@{Key} = @{variable.Key}";
            return $"@{Key} = {(Value % 1 == 0 ? ((int)Value).ToString() : String.Format("%0", Value.ToString()))}";
        }

        public override bool Equals(object obj)
        {
            var variable = obj as PdxVariable;
            return variable != null &&
                   Key == variable.Key &&
                   Value == variable.Value &&
                   FileName == variable.FileName &&
                   EqualityComparer<PdxVariable>.Default.Equals(Reference, variable.Reference);
        }

        public PdxVariable Clone() => new PdxVariable() { Key = Key, Value = Value, FileName = FileName, IsModData = IsModData, Reference = Reference };
    }
}
