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

namespace StellarisEditor.editors.technology
{
    /// <summary>
    /// TechnologyItemEditorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TechnologyItemEditorWindow : Window
    {
        public TechnologyItemEditorWindow()
        {
            InitializeComponent();
        }

        private void Circulation_Click(object sender, RoutedEventArgs e)
        {
            if (Circulation.IsChecked.Value)
            {
                Cost_Pre_Level.IsEnabled = true;
            }
            else
            {
                Cost_Pre_Level.IsEnabled = false;
            }
        }

        private void Ship_Click(object sender, RoutedEventArgs e)
        {
            if (Ship.IsChecked.Value)
            {
                Ship_title.IsEnabled = true;
                Ship_desc.IsEnabled = true;
            }
            else
            {
                Ship_title.IsEnabled = false;
                Ship_desc.IsEnabled = false;
            }
        }

        private void Custom_Click(object sender, RoutedEventArgs e)
        {
            if (Custom.IsChecked.Value)
            {
                Custom_desc.IsEnabled = true;
                Custom_title.IsEnabled = true;
            }
            else
            {
                Custom_desc.IsEnabled = false;
                Custom_title.IsEnabled = false;
            }
        }

        private void Check_Gateway_Click(object sender, RoutedEventArgs e)
        {
            if (Check_Gateway.IsChecked.Value)
            {
                Gateway.IsEnabled = true;
            }
            else
            {
                Gateway.IsEnabled = false;
            }
        }

        private void Start_Technology_Click(object sender, RoutedEventArgs e)
        {
            if (Start_Technology.IsChecked.Value)
            {
                Cost.IsEnabled = false;
                Tier.IsEnabled = false;
                Ai_Weight.IsEnabled = false;
                Weight_Modifier.IsEnabled = false;
                Modifier.IsEnabled = false;
                Tier.Text = "0";
            }
            else
            {
                Cost.IsEnabled = true;
                Tier.IsEnabled = true;
                Ai_Weight.IsEnabled = true;
                Weight_Modifier.IsEnabled = true;
                Modifier.IsEnabled = true;
                Tier.Text = null;
            }
        }
    }
}
