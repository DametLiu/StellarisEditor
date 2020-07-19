using StellarisEditor.data;
using StellarisEditor.mod.data;
using StellarisEditor.pdx.scriptobject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace StellarisEditor.editors.techEditor
{
    /// <summary>
    /// TechCategoryEditorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TechCategoryEditorWindow : Window
    {
        public ObservableCollection<TechnologyCategory> TechnologyCategories = new ObservableCollection<TechnologyCategory>();
        public TechCategoryEditorWindow()
        {
            InitializeComponent();
            foreach (var item in ModGlobalData.TechnologyCategories)
                TechnologyCategories.Add(item);
            dataGrid.ItemsSource = TechnologyCategories;
        }

        private void MenuItemClickSave(object sender, RoutedEventArgs e)
        {
            Save();
        }

        private void CommandExecuteSave(object sender, CanExecuteRoutedEventArgs e)
        {
            Save();
        }

        private void Save()
        {
            Directory.CreateDirectory(Properties.Settings.Default.ModPath + PdxGlobalData.STELLARIS_PATH_TECHNOLOGY_CATEGORY);

            var gs = TechnologyCategories.GroupBy(l => l.FileName);
            foreach (var g in gs)
            {
                String[] lines = new string[g.Count()];
                int i = 0;
                foreach (var l in g)
                    lines[i++] = l.ToString();
                File.WriteAllLines($"{Properties.Settings.Default.ModPath + PdxGlobalData.STELLARIS_PATH_TECHNOLOGY_CATEGORY}{g.Key}.txt", lines, new UTF8Encoding(false));
            }

            MessageBox.Show("保存完成");
        }

        private TechnologyCategory CopyTechnologyCategory;
        private void CommandExecuteCopy(object sender, CanExecuteRoutedEventArgs e)
        {
            Copy();
        }

        private void MenuItemClickCopy(object sender, RoutedEventArgs e)
        {
            Copy();
        }

        private void Copy()
        {
            if (dataGrid.SelectedItem is TechnologyCategory technologyCategory)
                CopyTechnologyCategory = technologyCategory;
        }

        private void CommandExecutePaste(object sender, CanExecuteRoutedEventArgs e)
        {
            Paste();
        }

        private void MenuItemClickPaste(object sender, RoutedEventArgs e)
        {
            Paste();
        }

        private void Paste()
        {
            if (CopyTechnologyCategory is TechnologyCategory technologyCategory)
            {
                TechnologyCategory pdxTechnologyCategory = technologyCategory.Clone();
                pdxTechnologyCategory.Key = pdxTechnologyCategory.Key + "_copy";
                TechnologyCategories.Insert(TechnologyCategories.IndexOf(technologyCategory), pdxTechnologyCategory);
            }
        }

        private void CommandExecuteExit(object sender, CanExecuteRoutedEventArgs e)
        {
            Exit();
        }

        private void MenuItemClickExit(object sender, RoutedEventArgs e)
        {
            Exit();
        }

        private void Exit()
        {
            Close();
        }

        private void CommandExecuteDelete(object sender, CanExecuteRoutedEventArgs e)
        {
            Delete();
        }

        private void MenuItemClickDelete(object sender, RoutedEventArgs e)
        {
            Delete();
        }

        private void Delete()
        {
            if (dataGrid.SelectedItem is TechnologyCategory technologyCategory)
                TechnologyCategories.Remove(technologyCategory);
        }

        private void CommandExecuteAdd(object sender, CanExecuteRoutedEventArgs e)
        {
            Add();
        }

        private void MenuItemClickAdd(object sender, RoutedEventArgs e)
        {
            Add();
        }

        private void Add()
        {
            if (dataGrid.SelectedItem is TechnologyCategory technologyCategory)
                TechnologyCategories.Insert(TechnologyCategories.IndexOf(technologyCategory), new TechnologyCategory() { Key = $"new_technology_category{++NewIndex}" });
            else
                TechnologyCategories.Insert(0, new TechnologyCategory() { Key = $"new_technology_category{++NewIndex}" });
        }

        private int NewIndex = 0;
    }
}
