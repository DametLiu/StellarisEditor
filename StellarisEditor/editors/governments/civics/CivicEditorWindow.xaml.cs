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

namespace StellarisEditor.editors.governments.civics
{
    /// <summary>
    /// CivicEditorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class CivicEditorWindow : Window
    {
        public CivicEditorWindow()
        {
            InitializeComponent();
        }

        private void Is_Origin_Click(object sender, RoutedEventArgs e)
        {
            if (Is_Origin.IsChecked.Value)
            {
                Advanced_start.IsEnabled = true;
                Preferred_Planet.IsEnabled = true;
                Non_Colonizable_Planet_Class.IsEnabled = true;
                Picture.IsEnabled = true;
                Flag.IsEnabled = true;
                Icon.IsEnabled = true;
                Initializers.IsEnabled = true;
                Pickable_At_Start.IsEnabled = false;
                Modification.IsEnabled = false;
                Swap_Type.IsEnabled = false;
                Can_Build_Ruler_Ship.IsEnabled = false;
            }
            else
            {
                Advanced_start.IsEnabled = false;
                Preferred_Planet.IsEnabled = false;
                Non_Colonizable_Planet_Class.IsEnabled = false;
                Picture.IsEnabled = false;
                Flag.IsEnabled = false;
                Icon.IsEnabled = false;
                Initializers.IsEnabled = false;
                Pickable_At_Start.IsEnabled = true;
                Modification.IsEnabled = true;
                Swap_Type.IsEnabled = true;
                Can_Build_Ruler_Ship.IsEnabled = true;
            }
        }
    }
}
