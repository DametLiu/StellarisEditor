using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.pdx.scriptobject
{
    public class Expression : PdxObject
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

        private Boolean _IsVariableExpression;
        public Boolean IsVariableExpression
        {
            get
            {
                return _IsVariableExpression;
            }
            set
            {
                _IsVariableExpression = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsVariableExpression"));
            }
        }

        private Boolean _IsArrayExpression;
        public Boolean IsArrayExpression
        {
            get
            {
                return _IsArrayExpression;
            }
            set
            {
                _IsArrayExpression = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("IsArrayExpression"));
            }
        }



        private ObservableCollection<Expression> _Children = new ObservableCollection<Expression>();
        public ObservableCollection<Expression> Children
        {
            get
            {
                return _Children;
            }
            set
            {
                _Children = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Children"));
            }
        }



        public override string ToString()
        {
            return $"{Key} {Operator} {Value}";
        }
    }
}
