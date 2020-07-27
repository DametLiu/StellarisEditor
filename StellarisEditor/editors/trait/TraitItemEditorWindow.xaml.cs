using StellarisEditor.pdx.parser;
using StellarisEditor.pdx.scriptobject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using StellarisEditor.editors.dataConversion;
using System.Collections.ObjectModel;
using System.Globalization;

namespace StellarisEditor.editors.trait
{
    /// <summary>
    /// TraitItemEditorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TraitItemEditorWindow : Window
    {
        public PdxTrait Trait { get; set; }

        public TraitItemEditorWindow(PdxTrait Trait)
        {
            this.Trait = Trait;
            InitializeComponent();
            changable = false;
            DataContext = Trait;
        }
        public TraitItemEditorWindow()
        {
            InitializeComponent();
        }

        

        private void DeleteSlaveCost(object sender, RoutedEventArgs e)
        {
            try
            {
                Trait.SlaveCost.RemoveAt(Trait.SlaveCost.Count - 1);
            }
            catch (Exception)
            {
                MessageBox.Show("没有条目可以被删除！");
            }
        }

        private void AddSlaveCost(object sender, RoutedEventArgs e)
        {
            Trait.SlaveCost.Add(new pdx.scriptobject.Expression() { Key = "", Operator = "", Value = "" });
        }

        private void DeleteModifier(object sender, RoutedEventArgs e)
        {
            try
            {
                Trait.Modifier.RemoveAt(Trait.Modifier.Count - 1);
            }
            catch (Exception)
            {
                MessageBox.Show("没有条目可以被删除！");
            }
        }

        private void AddModifier(object sender, RoutedEventArgs e)
        {
            Trait.Modifier.Add(new pdx.scriptobject.Expression() { Key = "", Operator = "", Value = "" });
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TreeViewTransition.TrimExpressionTransform(Trait.Modifier);
            TreeViewTransition.TrimExpressionTransform(Trait.LeaderPotentialAdd);
            TreeViewTransition.TrimExpressionTransform(Trait.SlaveCost);
            Modifier.IsEnabled = true;
            LeaderPotentialAdd.IsEnabled = true;
            SlaveCost.IsEnabled = true;
            changable = true;
        }

        private void IsLeaderTraitChecked(object sender, RoutedEventArgs e)
        {
            LeaderTrait.IsEnabled = true;
            LeaderClass.IsEnabled = true;
            LeaderPotentialAdd.IsEnabled = true;
            SlaveCost.IsEnabled = false;
            AddSlaveCostB.IsEnabled = false;
            DeleteSlaveCostB.IsEnabled = false;
            if (Trait.SlaveCost!= null)
            {
                if (Trait.SlaveCost.Count > 0)
                {
                    for (int i = 0; i < Trait.SlaveCost.Count; i++)
                    {
                        Trait.SlaveCost.RemoveAt(i);
                    }
                }
            }
        }

        private void IsLeaderTraitUnchecked(object sender, RoutedEventArgs e)
        {
            LeaderTrait.IsEnabled = false;
            Trait.LeaderTrait = null;
            LeaderClass.IsEnabled = false;
            Trait.LeaderClass = null;
            LeaderPotentialAdd.IsEnabled = false;
            SlaveCost.IsEnabled = true;
            AddSlaveCostB.IsEnabled = true;
            DeleteSlaveCostB.IsEnabled = true;
            if (Trait.LeaderPotentialAdd != null)
            {
                if (Trait.LeaderPotentialAdd.Count > 0)
                {
                    for (int i = 0; i < Trait.LeaderPotentialAdd.Count; i++)
                    {
                        Trait.LeaderPotentialAdd.RemoveAt(i);
                    }
                }
            }
        }

        bool changable;

        private void LeaderTraitChecked(object sender, RoutedEventArgs e)
        {
            ObservableCollection<string> vs = new ObservableCollection<string>();
            if (LeaderTraitRuler.IsChecked == true)
            {
                vs.Add("ruler");
            }
            if (LeaderTraitScientist.IsChecked == true)
            {
                vs.Add("scientist");
            }
            if (LeaderTraitAdmiral.IsChecked == true)
            {
                vs.Add("admiral");
            }
            if (LeaderTraitGeneral.IsChecked == true)
            {
                vs.Add("general");
            }
            if (LeaderTraitGovernor.IsChecked == true)
            {
                vs.Add("governor");
            }
            if (changable == true)
            {
                Trait.LeaderTrait = vs;
            }
        }
        private void LeaderTraitUnchecked(object sender, RoutedEventArgs e)
        {
            ObservableCollection<string> vs = new ObservableCollection<string>();
            if (LeaderTraitRuler.IsChecked == true)
            {
                vs.Add("ruler");
            }
            if (LeaderTraitScientist.IsChecked == true)
            {
                vs.Add("scientist");
            }
            if (LeaderTraitAdmiral.IsChecked == true)
            {
                vs.Add("admiral");
            }
            if (LeaderTraitGeneral.IsChecked == true)
            {
                vs.Add("general");
            }
            if (LeaderTraitGovernor.IsChecked == true)
            {
                vs.Add("governor");
            }
            Trait.LeaderTrait = vs;
        }
        private void LeaderClassChecked(object sender, RoutedEventArgs e)
        {
            ObservableCollection<string> vs = new ObservableCollection<string>();
            if (LeaderClassRuler.IsChecked == true)
            {
                vs.Add("ruler");
            }
            if (LeaderClassScientist.IsChecked == true)
            {
                vs.Add("scientist");
            }
            if (LeaderClassAdmiral.IsChecked == true)
            {
                vs.Add("admiral");
            }
            if (LeaderClassGeneral.IsChecked == true)
            {
                vs.Add("general");
            }
            if (LeaderClassGovernor.IsChecked == true)
            {
                vs.Add("governor");
            }
            if (changable == true)
            {
                Trait.LeaderClass = vs;
            }
        }
        private void LeaderClassUnchecked(object sender, RoutedEventArgs e)
        {
            ObservableCollection<string> vs = new ObservableCollection<string>();
            if (LeaderClassRuler.IsChecked == true)
            {
                vs.Add("ruler");
            }
            if (LeaderClassScientist.IsChecked == true)
            {
                vs.Add("scientist");
            }
            if (LeaderClassAdmiral.IsChecked == true)
            {
                vs.Add("admiral");
            }
            if (LeaderClassGeneral.IsChecked == true)
            {
                vs.Add("general");
            }
            if (LeaderClassGovernor.IsChecked == true)
            {
                vs.Add("governor");
            }
            Trait.LeaderClass = vs;
        }
    }




    public class GatherSearchRuler : IValueConverter//搜索集合中的ruler
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<string> vs = (ObservableCollection<string>)value;
            bool hasleader = false;
            if (vs == null)
            {
                return hasleader;
            }
            else
            {
                foreach (var item in vs)
                {
                    if (item == "ruler" || item == "all")
                    {
                        hasleader = true;
                    };
                }
                return hasleader;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class GatherSearchScientist : IValueConverter//搜索集合中的scientist
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<string> vs = (ObservableCollection<string>)value;
            bool hasleader = false;
            if (vs == null)
            {
                return hasleader;
            }
            else
            {
                foreach (var item in vs)
                {
                    if (item == "scientist" || item == "all")
                    {
                        hasleader = true;
                    };
                }
                return hasleader;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class GatherSearchAdmiral : IValueConverter//搜索集合中的Admiral
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<string> vs = (ObservableCollection<string>)value;
            bool hasleader = false;
            if (vs == null)
            {
                return hasleader;
            }
            else
            {
                foreach (var item in vs)
                {
                    if (item == "admiral" || item == "all")
                    {
                        hasleader = true;
                    };
                }
                return hasleader;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class GatherSearchGeneral : IValueConverter//搜索集合中的General
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<string> vs = (ObservableCollection<string>)value;
            bool hasleader = false;
            if (vs == null)
            {
                return hasleader;
            }
            else
            {
                foreach (var item in vs)
                {
                    if (item == "general" || item == "all")
                    {
                        hasleader = true;
                    };
                }
                return hasleader;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
    public class GatherSearchGovernor : IValueConverter//搜索集合中的Governor
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<string> vs = (ObservableCollection<string>)value;
            bool hasleader = false;
            if (vs == null)
            {
                return hasleader;
            }
            else
            {
                foreach (var item in vs)
                {
                    if (item == "governor"||item == "all")
                    {
                        hasleader = true;
                    };
                }
                return hasleader;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
