using StellarisEditor.pdx.parser;
using StellarisEditor.pdx.scriptobject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
            Resource_title.Text = null;
            Resource_desc.Text = null;
        }

        private void Resource_Checked(object sender, RoutedEventArgs e)
        {
            Resource_title.IsEnabled = true;
            Resource_desc.IsEnabled = true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Technology.Modifier.RemoveAt(Technology.Modifier.Count - 1);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            Technology.Modifier.Add(new pdx.scriptobject.Expression() { Key = "", Operator = "", Value = "" });
        }

        private void Prereqfor_Desc_Checked(object sender, RoutedEventArgs e)
        {
            Check_Hide_Prereq_For_Desc.IsEnabled = true;
            Ship.IsEnabled = true;
            Resource.IsEnabled = true;
            Custom.IsEnabled = true;
            Component.IsEnabled = true;
            Feature.IsEnabled = true;
            Diplo_Action.IsEnabled = true;
        }

        private void Prereqfor_Desc_Unchecked(object sender, RoutedEventArgs e)
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

        private void Start_Technology_Checked(object sender, RoutedEventArgs e)
        {
            Cost.IsEnabled = false;
            Tier.IsEnabled = false;
            Ai_Weight.IsEnabled = false;
            Weight_Modifier.IsEnabled = false;
            Modifier.IsEnabled = false;
            Tier.Text = "0";
        }

        private void Start_Technology_Unchecked(object sender, RoutedEventArgs e)
        {
            Cost.IsEnabled = true;
            Tier.IsEnabled = true;
            Ai_Weight.IsEnabled = true;
            Weight_Modifier.IsEnabled = true;
            Modifier.IsEnabled = true;
            Tier.Text = null;
        }

        private void Check_Gateway_Checked(object sender, RoutedEventArgs e)
        {
            Gateway.IsEnabled = true;
        }

        private void Check_Gateway_Unchecked(object sender, RoutedEventArgs e)
        {
            Gateway.IsEnabled = false;
        }

        private void Circulation_Checked(object sender, RoutedEventArgs e)
        {
            Cost_Pre_Level.IsEnabled = true;
            CycleIndex.IsEnabled = true;
        }

        private void Circulation_Unchecked(object sender, RoutedEventArgs e)
        {
            Cost_Pre_Level.IsEnabled = false;
            Cost_Pre_Level.Text = "0";
            CycleIndex.IsEnabled = false;
            CycleIndex.Text = "0";
        }
        //删除按钮
        //private void Remove_Click(object sender, RoutedEventArgs e)
        //{
        //    if (!string.IsNullOrWhiteSpace(_target.Uid))
        //    {
        //        foreach (TreeViewItem item in this.Modifier.Items)
        //        {
        //            if (item.Uid == _target.Uid)
        //            {
        //                this.Modifier.Items.Remove(_target);
        //                break;
        //            }
        //            TraverseNodeInfo(item, _target);
        //        }
        //    }
        //}
        ////需要删除的节点信息
        //TreeViewItem _target;
        ////鼠标右键点击事件
        //private void Modifier_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        //{
        //    TreeViewItem targetItem = GetNearestContainer(e.OriginalSource as UIElement);
        //    {
        //        if (targetItem != null)
        //        {
        //            ContextMenu contextMenu = this.FindResource("deletNode") as ContextMenu;
        //            contextMenu.PlacementTarget = sender as TreeViewItem;
        //            contextMenu.IsOpen = true;
        //            FindParent(targetItem);
        //        }
        //    }
        //}
        //private TreeViewItem GetNearestContainer(UIElement element)
        //{
        //    TreeViewItem container = element as TreeViewItem;
        //    while ((container == null) && (element != null))
        //    {
        //        element = VisualTreeHelper.GetParent(element) as UIElement;
        //        container = element as TreeViewItem;
        //    }
        //    return container;
        //}
        //private void FindParent(TreeViewItem treeView)
        //{
        //    _target = new TreeViewItem();
        //    TreeViewItem parentTree = treeView.Parent as TreeViewItem;
        //    if (parentTree != null && parentTree.Items.Count > 1)
        //    {
        //        _target = treeView;
        //    }
        //    else if (parentTree != null && parentTree.Items.Count == 1)
        //    {
        //        _target = parentTree;
        //        FindParent(parentTree);
        //    }
        //    else
        //    {
        //        _target = treeView;
        //    }
        //}
        //private void TraverseNodeInfo(TreeViewItem viewItem,TreeViewItem tree)
        //{
        //    foreach (TreeViewItem item in viewItem.Items)
        //    {
        //        if (item.Uid == tree.Uid)
        //        {
        //            viewItem.Items.Remove(tree);
        //            return;
        //        }
        //        TraverseNodeInfo(item, _target);
        //    }
        //}
    }
}
