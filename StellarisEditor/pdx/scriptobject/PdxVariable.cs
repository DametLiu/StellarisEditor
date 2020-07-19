using StellarisEditor.ScriptEngine;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.pdx.scriptobject
{
    public class Variable : PdxObject
    {
        public new event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// 变量名称
        /// </summary>
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

        /// <summary>
        /// 变量得值，如果引用不为空，才应该取值，否则应该取引用得变量得值
        /// </summary>
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

        /// <summary>
        /// 所属文件名称
        /// </summary>
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

        /// <summary>
        /// 引用值。该值指向了一个变量
        /// </summary>
        private Variable _Reference;
        public Variable Reference
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

        /// <summary>
        /// 是否是浮点类型
        /// </summary>
        /// <returns></returns>
        public bool IsDouble() => double.Parse(Value) % 1 != 0;

        /// <summary>
        /// 是否是整数
        /// </summary>
        /// <returns></returns>
        public bool IsInt() => double.Parse(Value) % 1 == 0;

        /// <summary>
        /// 是否是引用类型
        /// </summary>
        /// <returns></returns>
        public bool IsReference() => Reference != null;

        

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        public override string ToString()
        {
            if (Reference is Variable variable)
                return $"@{Key} = @{variable.Key}";
            return $"@{Key} = {Value}";
        }

        public override bool Equals(object obj)
        {
            var variable = obj as Variable;
            return variable != null &&
                   Key == variable.Key &&
                   Value == variable.Value &&
                   FileName == variable.FileName &&
                   EqualityComparer<Variable>.Default.Equals(Reference, variable.Reference);
        }

        public Variable Clone() => new Variable() { Key = Key, Value = Value, FileName = FileName, IsModData = IsModData, Reference = Reference };

    }
}
