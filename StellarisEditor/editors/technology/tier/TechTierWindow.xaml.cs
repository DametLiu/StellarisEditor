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
    /// TechTierWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TechTierWindow : Window
    {
        public ObservableCollection<PdxTechnologyTier> TechnologyTiers = new ObservableCollection<PdxTechnologyTier>();
        public TechTierWindow()
        {
            InitializeComponent();
            foreach (var item in ModGlobalData.TechnologyTiers)
                TechnologyTiers.Add(item);
            dataGrid.ItemsSource = TechnologyTiers;
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
            Directory.CreateDirectory(Properties.Settings.Default.ModPath + PdxGlobalData.STELLARIS_PATH_TECHNOLOGY_TIER);

            var gs = TechnologyTiers.GroupBy(l => l.FileName);
            foreach (var g in gs)
            {
                String[] lines = new string[g.Count()];
                int i = 0;
                foreach (var l in g)
                    lines[i++] = l.ToString();
                File.WriteAllLines($"{Properties.Settings.Default.ModPath + PdxGlobalData.STELLARIS_PATH_TECHNOLOGY_TIER}{g.Key}.txt", lines, new UTF8Encoding(false));
                MessageBox.Show("保存完成");
            }
        }

        private PdxTechnologyTier CopyTechnologyTier;
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
            if (dataGrid.SelectedItem is PdxTechnologyTier technologyTier)
                CopyTechnologyTier = technologyTier;
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
            if (CopyTechnologyTier is PdxTechnologyTier technologyTier)
            {
                PdxTechnologyTier pdxTechnologyTier = technologyTier.Clone();
                pdxTechnologyTier.Key = pdxTechnologyTier.Key + "_copy";
                TechnologyTiers.Insert(TechnologyTiers.IndexOf(technologyTier), pdxTechnologyTier);
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
            if (dataGrid.SelectedItem is PdxTechnologyTier technologyTier)
                TechnologyTiers.Remove(technologyTier);
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
            if (dataGrid.SelectedItem is PdxTechnologyTier technologyTier)
                TechnologyTiers.Insert(TechnologyTiers.IndexOf(technologyTier), new PdxTechnologyTier() { Key = $"new_technology_tier{++NewIndex}" });
            else
                TechnologyTiers.Insert(0, new PdxTechnologyTier() { Key = $"new_technology_tier{++NewIndex}" });
        }

        private int NewIndex = 0;
    }
}
