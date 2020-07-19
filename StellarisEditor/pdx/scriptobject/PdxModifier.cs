using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.pdx.scriptobject
{
    /// <summary>
    /// 修正，每一个对象是一个修正
    /// </summary>
    public class Modifier : PdxObject
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
}
