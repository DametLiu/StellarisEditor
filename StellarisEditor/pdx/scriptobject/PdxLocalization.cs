using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.pdx.scriptobject
{
    public class PdxLocalization : PdxObject
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

        private Int32 _Index;
        public Int32 Index
        {
            get
            {
                return _Index;
            }
            set
            {
                _Index = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("Index"));
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

        private String _ValueEnglish;
        public String ValueEnglish
        {
            get
            {
                return _ValueEnglish;
            }
            set
            {
                _ValueEnglish = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ValueEnglish"));
            }
        }
        public string ToStringValueEnglish()
        {
            return $" {Key}:0 \"{ValueEnglish}\"";
        }

        private String _ValueSimpChinese;
        public String ValueSimpChinese
        {
            get
            {
                return _ValueSimpChinese;
            }
            set
            {
                _ValueSimpChinese = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ValueSimpChinese"));
            }
        }
        public string ToStringValueSimpChinese()
        {
            return $" {Key}:0 \"{ValueSimpChinese}\"";
        }

        private String _ValuePolish;
        public String ValuePolish
        {
            get
            {
                return _ValuePolish;
            }
            set
            {
                _ValuePolish = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ValuePolish"));
            }
        }
        public string ToStringValuePolish()
        {
            return $" {Key}:0 \"{ValuePolish}\"";
        }

        private String _ValueBrazPor;
        public String ValueBrazPor
        {
            get
            {
                return _ValueBrazPor;
            }
            set
            {
                _ValueBrazPor = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ValueBrazPor"));
            }
        }
        public string ToStringValueBrazPor()
        {
            return $" {Key}:0 \"{ValueBrazPor}\"";
        }

        private String _ValueFrench;
        public String ValueFrench
        {
            get
            {
                return _ValueFrench;
            }
            set
            {
                _ValueFrench = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ValueFrench"));
            }
        }
        public string ToStringValueFrench()
        {
            return $" {Key}:0 \"{ValueFrench}\"";
        }

        private String _ValueGerman;
        public String ValueGerman
        {
            get
            {
                return _ValueGerman;
            }
            set
            {
                _ValueGerman = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ValueGerman"));
            }
        }
        public string ToStringValueGerman()
        {
            return $" {Key}:0 \"{ValueGerman}\"";
        }

        private String _ValueRussian;
        public String ValueRussian
        {
            get
            {
                return _ValueRussian;
            }
            set
            {
                _ValueRussian = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ValueRussian"));
            }
        }
        public string ToStringValueRussian()
        {
            return $" {Key}:0 \"{ValueRussian}\"";
        }

        private String _ValueSpanish;
        public String ValueSpanish
        {
            get
            {
                return _ValueSpanish;
            }
            set
            {
                _ValueSpanish = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs("ValueSpanish"));
            }
        }
        public string ToStringValueSpanish()
        {
            return $" {Key}:0 \"{ValueSpanish}\"";
        }

        public override string ToString()
        {
            return ToStringValueSimpChinese();
        }

        public override bool Equals(object obj)
        {
            return obj is PdxLocalization localization &&
                   Key == localization.Key &&
                   Index == localization.Index &&
                   ValueEnglish == localization.ValueEnglish &&
                   ValueSimpChinese == localization.ValueSimpChinese &&
                   ValuePolish == localization.ValuePolish &&
                   ValueBrazPor == localization.ValueBrazPor &&
                   ValueFrench == localization.ValueFrench &&
                   ValueGerman == localization.ValueGerman &&
                   ValueRussian == localization.ValueRussian &&
                   ValueSpanish == localization.ValueSpanish;
        }

        public PdxLocalization Clone()
        {
            PdxLocalization pdxLocalization = new PdxLocalization
            {
                Key = Key,
                Index = Index,
                FileName = FileName,
                ValueEnglish = ValueEnglish,
                ValueSimpChinese = ValueSimpChinese,
                ValueBrazPor = ValueBrazPor,
                ValueFrench = ValueFrench,
                ValueGerman = ValueGerman,
                ValuePolish = ValuePolish,
                ValueRussian = ValueRussian,
                ValueSpanish = ValueSpanish
            };

            return pdxLocalization;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
