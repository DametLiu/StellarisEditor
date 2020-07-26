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
        }
    }
}
