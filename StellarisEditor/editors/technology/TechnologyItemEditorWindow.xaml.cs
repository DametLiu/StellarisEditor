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
        public Technology Technology { get; set; }
        public string test = null;

        public TechnologyItemEditorWindow(Technology Technology)
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
                CycleIndex.IsEnabled = true;
            }
            else
            {
                Cost_Pre_Level.IsEnabled = false;
                CycleIndex.IsEnabled = false;
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

        private void Prereqfor_Desc_Click(object sender, RoutedEventArgs e)
        {
            if (Prereqfor_Desc.IsChecked.Value)
            {
                Check_Hide_Prereq_For_Desc.IsEnabled = true;
                Ship.IsEnabled = true;
                Resource.IsEnabled = true;
                Custom.IsEnabled = true;
                Component.IsEnabled = true;
                Feature.IsEnabled = true;
                Diplo_Action.IsEnabled = true;
            }
            else
            {
                Check_Hide_Prereq_For_Desc.IsEnabled = false;
                Check_Hide_Prereq_For_Desc.IsChecked = false;
                Ship.IsEnabled = false;
                Ship.IsChecked = false;
                Resource.IsEnabled = false;
                Resource.IsChecked = false;
                Custom.IsEnabled = false;
                Custom.IsChecked = false;
                Component.IsEnabled = false;
                Component.IsChecked = false;
                Feature.IsEnabled = false;
                Feature.IsChecked = false;
                Diplo_Action.IsEnabled = false;
                Diplo_Action.IsChecked = false;
            }
        }

        private void Check_Hide_Prereq_For_Desc_Checked(object sender, RoutedEventArgs e)
        {
            Hide_Prereq_For_Desc.IsEnabled = true;
            Hide_Prereq_For_Desc.IsEnabled = true;
        }

        private void Check_Hide_Prereq_For_Desc_Unchecked(object sender, RoutedEventArgs e)
        {
            Hide_Prereq_For_Desc.IsEnabled = false;
            Hide_Prereq_For_Desc.IsEnabled = false;
            Hide_Prereq_For_Desc.Text = "";
        }

        private void Ship_Checked(object sender, RoutedEventArgs e)
        {
            Ship_title.IsEnabled = true;
            Ship_desc.IsEnabled = true;
        }

        private void Ship_Unchecked(object sender, RoutedEventArgs e)
        {
            Ship_title.IsEnabled = false;
            Ship_desc.IsEnabled = false;
            Ship_title.Text = "";
            Ship_desc.Text = "";
        }

        private void Custom_Checked(object sender, RoutedEventArgs e)
        {
            Custom_desc.IsEnabled = true;
            Custom_title.IsEnabled = true;
        }

        private void Custom_Unchecked(object sender, RoutedEventArgs e)
        {
            Custom_desc.IsEnabled = false;
            Custom_title.IsEnabled = false;
            Custom_desc.Text = "";
            Custom_title.Text = "";
        }

        private void Component_Checked(object sender, RoutedEventArgs e)
        {
            Component_title.IsEnabled = true;
            Component_desc.IsEnabled = true;
        }

        private void Component_Unchecked(object sender, RoutedEventArgs e)
        {
            Component_title.IsEnabled = false;
            Component_desc.IsEnabled = false;
            Component_title.Text = "";
            Component_desc.Text = "";
        }

        private void Diplo_Action_Checked(object sender, RoutedEventArgs e)
        {
            Diplo_Action_desc.IsEnabled = true;
            Diplo_Action_title.IsEnabled = true;
        }

        private void Diplo_Action_Unchecked(object sender, RoutedEventArgs e)
        {
            Diplo_Action_desc.IsEnabled = false;
            Diplo_Action_title.IsEnabled = false;
            Diplo_Action_desc.Text = "";
            Diplo_Action_title.Text = "";
        }

        private void Feature_Checked(object sender, RoutedEventArgs e)
        {
            Feature_title.IsEnabled = true;
            Feature_desc.IsEnabled = true;
        }

        private void Feature_Unchecked(object sender, RoutedEventArgs e)
        {
            Feature_title.IsEnabled = false;
            Feature_desc.IsEnabled = false;
            Feature_desc.Text = "";
            Feature_title.Text = "";
        }

        private void Resource_Unchecked(object sender, RoutedEventArgs e)
        {
            Resource_title.IsEnabled = false;
            Resource_desc.IsEnabled = false;
            Resource_title.Text = "";
            Resource_desc.Text = "";
        }

        private void Resource_Checked(object sender, RoutedEventArgs e)
        {
            Resource_title.IsEnabled = true;
            Resource_desc.IsEnabled = true;
        }

    }
}
