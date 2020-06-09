using StellarisEditor.data;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.pdx.scriptobject
{
    public class PdxNumber : PdxObject
    {
        public new event PropertyChangedEventHandler PropertyChanged;

        private String _Name = "";
        public String Name
        {
            get
            {
                return _Name;
            }
            set
            {
                _Name = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Name"));
            }
        }

        private String _Value = "";
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

        private String _File = "";
        public String File
        {
            get
            {
                return _File;
            }
            set
            {
                _File = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("File"));
            }
        }

        public override String ToString()
        {
            return String.Format("@{0} = {1}", Name, Value);
        }

        public override int GetHashCode()
        {
            var hashCode = -10147690;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Name);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Value);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(File);
            hashCode = hashCode * -1521134295 + IsModData.GetHashCode();
            return hashCode;
        }

        public override bool Equals(object obj)
        {
            return obj is PdxNumber number &&
                   Name == number.Name &&
                   Value == number.Value &&
                   File == number.File &&
                   IsModData == number.IsModData;
        }
    }
}
