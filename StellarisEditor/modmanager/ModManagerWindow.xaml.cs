using StellarisEditor.mod.data;
using StellarisEditor.pdx.parser;
using StellarisEditor.pdx.scriptobject;
using StellarisEditor.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
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
using System.Collections.ObjectModel;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.Win32;
using StellarisEditor.editors;

namespace StellarisEditor.modmanager
{
    /// <summary>
    /// ModManagerWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ModManagerWindow : Window
    {

        public ModManagerWindow()
        {
            InitializeComponent();
            
            listView.ItemsSource = ModGlobalData.ModProjects;
            editPanel.DataContext = ModGlobalData.ModProjects.First();

            listView.Focus();
            CurrentMod = ModGlobalData.ModProjects.First();
        }

        private void Grid_MouseDown(object sender, MouseButtonEventArgs e)
        {
            var item = listView.SelectedItem;
            if (item is PdxMod pdxMod)
            {
                CurrentMod = pdxMod;
                editPanel.DataContext = CurrentMod;
                editPanel.Visibility = Visibility.Visible;
            }
        }
        
        public PdxMod CurrentMod = new PdxMod();

        private void ChooseImage(object sender, MouseButtonEventArgs e)
        {
            if (String.IsNullOrEmpty(CurrentMod.Directory))
            {
                MessageBox.Show("请先设置Mod保存路径");
                return;
            }

            if (CurrentMod.Directory.Equals("Example"))
            {
                MessageBox.Show("请先修改Mod保存路径");
                return;
            }

            OpenFileDialog fileDialog = new OpenFileDialog
            {
                Multiselect = false,
                InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Title = "请选择Mod封面图",
                Filter = "(*.png)|*.png"
            };

            if (fileDialog.ShowDialog() == true)
            {
                string file = fileDialog.FileName;
                CurrentMod.Picture = file;
                imageView.GetBindingExpression(Image.SourceProperty).UpdateSource();
                imageView.GetBindingExpression(Image.SourceProperty).UpdateTarget();
            }
        }

        private void UpdateSelectedTags(object sender, RoutedEventArgs e)
        {
            tagsdView.GetBindingExpression(ComboBox.TextProperty).UpdateTarget();
        }

        private void SaveModFile(object sender, RoutedEventArgs e)
        {
            if (String.IsNullOrWhiteSpace(pathView.Text))
                MessageBox.Show("路径不能为空");
            else if (String.IsNullOrWhiteSpace(versionView.Text))
                MessageBox.Show("Mod版本号不能为空");
            else if (String.IsNullOrWhiteSpace(supportedVersionView.Text))
                MessageBox.Show("群星版本号不能为空");
            else if (String.IsNullOrWhiteSpace(tagsdView.Text))
                MessageBox.Show("标签不能为空");
            else if (String.IsNullOrEmpty(CurrentMod.Picture))
                MessageBox.Show("还未选择封面图", "错误");
            else
            {
                if (!CurrentMod.IsModData)
                    WirteModFile(CurrentMod);
                else
                {
                    var mod = CurrentMod.Clone();
                    mod.IsModData = false;
                    WirteModFile(mod);
                    ModGlobalData.CreateExampleModProject();
                    listView.ItemsSource = null;
                    listView.ItemsSource = ModGlobalData.ModProjects;
                    CurrentMod = ModGlobalData.ModProjects.First();
                }

                MessageBox.Show("已保存");
            }
        }

        private void WirteModFile(PdxMod pdxMod)
        {
            var filePath = ModGlobalData.MOD_PATH_ROOT + pdxMod.Directory + ".mod";
            File.WriteAllText(filePath, pdxMod.ToString());

            var descriptorMod = pdxMod.Clone();
            descriptorMod.Directory = "";
            var descriptorPath = ModGlobalData.MOD_PATH_ROOT + pdxMod.Directory + @"\descriptor.mod";
            File.WriteAllText(descriptorPath, descriptorMod.ToString());

            ListBoxItem listBoxItem = (ListBoxItem)(listView.ItemContainerGenerator.ContainerFromIndex(listView.SelectedIndex));
            ContentPresenter contentPresenter = FindVisualChild<ContentPresenter>(listBoxItem);
            DataTemplate dataTemplate = contentPresenter.ContentTemplate;
            Image target = (Image)dataTemplate.FindName("listImageView", contentPresenter);
            target.GetBindingExpression(Image.SourceProperty).UpdateTarget();

            if (!Directory.Exists(ModGlobalData.MOD_PATH_ROOT + CurrentMod.Directory))
                Directory.CreateDirectory(ModGlobalData.MOD_PATH_ROOT + CurrentMod.Directory);
            if (!CurrentMod.Picture.Equals("thumbnail.png"))
                File.Copy(CurrentMod.Picture, ModGlobalData.MOD_PATH_ROOT + CurrentMod.Directory + "\\thumbnail.png", true);
        }

        private childItem FindVisualChild<childItem>(DependencyObject obj) where childItem : DependencyObject
        {

            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DependencyObject child = VisualTreeHelper.GetChild(obj, i);
                if (child != null && child is childItem)
                    return (childItem)child;
                else
                {
                    childItem childOfChild = FindVisualChild<childItem>(child);
                    if (childOfChild != null)
                        return childOfChild;
                }
            }
            return null;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (CurrentMod.IsModData)
            {
                MessageBox.Show("请先保存后，重新选择mod后打开编辑器");
                return;
            }

            Properties.Settings.Default.ModPath = CurrentMod.Path.Replace("/", @"\");
            Properties.Settings.Default.Save();
            ModGlobalData.Localizations = new LinkedList<PdxLocalization>();
            EditorWindow editorWindow = new EditorWindow();
            editorWindow.ShowDialog();
        }
    }
}
