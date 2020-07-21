using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections.ObjectModel;
using System.Windows.Data;
using StellarisEditor.pdx.scriptobject;
using System.Globalization;

namespace StellarisEditor.editors.dataConversion
{
    public class StrToBoolDefultFalse : IValueConverter//string与bool相互转换，默认为false (string)<->(bool)(TwoWay)
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strToBool = (string)value;
            if (strToBool == "yes")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? boolToStr = (bool?)value;
            if (boolToStr == true)
            {
                return "yes";
            }
            else
            {
                return "no";
            }
        }
    }
    public class StrToBoolDefaultTure : IValueConverter//string与bool相互转换，默认为true (string)<->(bool)(TwoWay)
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = (string)value;
            if (str == "no")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? toStr = (bool?)value;
            if (toStr == false)
            {
                return "no";
            }
            else
            {
                return "yes";
            }
        }
    }

    public class GatherToStr : IValueConverter//string集合与string相互转换，通过\n进行分隔 (ObservaleCollection<string>)<->(string)(TwoWay)
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<string> vs = (ObservableCollection<string>)value;
            string str = null;
            if (vs == null)
            {
                return str;
            }
            else
            {
                foreach (var item in vs)
                {
                    str += (item.ToString() + "\n");
                }
                return str;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = (string)value;
            ObservableCollection<string> vs = new ObservableCollection<string>(str.Split('\n'));
            return vs;
        }
    }
    public class GatherToStrBlank : IValueConverter//集合与string相互转换，通过' '进行分隔 (ObservaleCollection<string>)<->(string)(TwoWay)
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<string> vs = (ObservableCollection<string>)value;
            string str = null;
            if (vs == null)
            {
                return str;
            }
            else
            {
                foreach (var item in vs)
                {
                    str += (item.ToString() + " ");
                }
                return str;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = (string)value;
            ObservableCollection<string> vs = new ObservableCollection<string>(str.Split(' '));
            return vs;
        }
    }
    public class VarTransition : IValueConverter//用于判断某项属性是否启用 (var)->(bool)(OneWay)
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            var vs = value;
            if (vs != null)
            {
                Type type = vs.GetType();
                return true;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
