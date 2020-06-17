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

namespace StellarisEditor.editors.technology
{
    /// <summary>
    /// TechnologyItemEditorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TechnologyItemEditorWindow : Window
    {
        public PdxTechnology Technology { get; set; }

        public TechnologyItemEditorWindow(PdxTechnology Technology)
        {
            this.Technology = Technology;
            InitializeComponent();
            DataContext = Technology;
        }

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
        private void Component_Click(object sender, RoutedEventArgs e)
        {
            if (Component.IsChecked.Value)
            {
                Component_title.IsEnabled = true;
                Component_desc.IsEnabled = true;
            }
            else
            {
                Component_title.IsEnabled = false;
                Component_desc.IsEnabled = false;
            }
        }
        private void Diplo_Action_Click(object sender, RoutedEventArgs e)
        {
            if (Diplo_Action.IsChecked.Value)
            {
                Diplo_Action_desc.IsEnabled = true;
                Diplo_Action_title.IsEnabled = true;
            }
            else
            {
                Diplo_Action_desc.IsEnabled = false;
                Diplo_Action_title.IsEnabled = false;
            }

        }
        private void Feature_Click(object sender, RoutedEventArgs e)
        {
            if (Feature.IsChecked.Value)
            {
                Feature_title.IsEnabled = true;
                Feature_desc.IsEnabled = true;
            }
            else
            {
                Feature_title.IsEnabled = false;
                Feature_desc.IsEnabled = false;
            }
        }
        private void Resource_Click(object sender, RoutedEventArgs e)
        {
            if (Resource.IsChecked.Value)
            {
                Resource_title.IsEnabled = true;
                Resource_desc.IsEnabled = true;
            }
            else
            {
                Resource_title.IsEnabled = false;
                Resource_desc.IsEnabled = false;
            }

        }

        private void Check_Hide_Prereq_For_Desc_Click(object sender, RoutedEventArgs e)
        {
            if (Check_Hide_Prereq_For_Desc.IsChecked.Value)
            {
                Ship.IsEnabled = true;
                Resource.IsEnabled = true;
                Custom.IsEnabled = true;
                Component.IsEnabled = true;
                Feature.IsEnabled = true;
                Diplo_Action.IsEnabled = true;
            }
            else
            {
                Ship.IsEnabled = false;
                Resource.IsEnabled = false;
                Custom.IsEnabled = false;
                Component.IsEnabled = false;
                Feature.IsEnabled = false;
                Diplo_Action.IsEnabled = false;
            }
        }
    }
}
