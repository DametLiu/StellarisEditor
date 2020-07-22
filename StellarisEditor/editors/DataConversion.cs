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
    public class TreeViewTransition//将适用于TreeView的数据类型转换
    {
        //Expression类转换为string
        public static string TriggerToStr(StellarisEditor.pdx.scriptobject.Expression expression)
        {
            string str = null;
            foreach (var item in expression.Children)
            {
                str += $"{item.Key} {item.Operator} {item.Value}";
                if (item.Children != null)
                {
                    str += TriggerToStr(item) + "\n";
                }
            }
            return str;
        }
        //整理Expression类使得所有Children的子节点元素全部集合到根节点上以便于在TreeView上显示
        public static void TrimModifierTransition(ObservableCollection<StellarisEditor.pdx.scriptobject.Expression> expressions)
        {
            int i = 0, j = 0;
            if (expressions != null)
            {
                for (i = 0; i < expressions.Count; i++)
                {
                    for (j = 0; j < expressions[i].Children.Count; j++)
                    {
                        if (expressions[i].Children[j] != null)
                        {
                            expressions.Add(new pdx.scriptobject.Expression()
                            {
                                Key = expressions[i].Children[j].Key,
                                Operator = expressions[i].Children[j].Operator,
                                Value = expressions[i].Children[j].Value
                            });
                            i = 0;
                        }
                    }
                    expressions.Remove(expressions[i]);
                }
            }
        }
    }
}
