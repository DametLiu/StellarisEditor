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

            dataGrid.ItemsSource = TechnologyTiers;

            ModGlobalData.LoadTechnologyTiers(null);
            ProgressTask = new Thread(() => UpdateProgress(UpdateData));
            ProgressTask.Start();
        }
        private Thread SaveTask;
        private Thread ProgressTask;
        private int value = 0;
        delegate void DelegateThreadFunction();

        private int updateCount = 0;
        private void UpdateData()
        {
            if (TechnologyTiers.Count != 0 || updateCount > 0)
                return;
            updateCount = ModGlobalData.TechnologyTiers.Count;

            var current = value;
            for (int i = 0; i < ModGlobalData.TechnologyTiers.Count; i++)
            {
                var item = ModGlobalData.TechnologyTiers.ElementAt(i);
                if (!TechnologyTiers.Contains(item))
                {
                    int a = current + (TechnologyTiers.Count / ModGlobalData.TechnologyTiers.Count * (99 - current));
                    dataGrid.Dispatcher.BeginInvoke(new Action(() => {
                        TechnologyTiers.Add(item);
                        progressView.Value = value = a;
                        updateCount--;
                    }), DispatcherPriority.Normal);
                }
            }
        }

        private void UpdateProgress(DelegateThreadFunction method)
        {
            Thread.Sleep(500);
            StartProgress();

            while (true)
            {
                ThreadPool.GetMaxThreads(out int maxWorkerThreads, out int maxPortThreads);
                ThreadPool.GetAvailableThreads(out int workerThreads, out int portThreads);
                if (maxWorkerThreads - workerThreads != 0)
                {
                    Thread.Sleep(1000);
                    PlusProgress();
                }
                else
                {

                    if (!IsVisible)
                        return;

                    method?.Invoke();
                    method = null;
                    if (value < 99)
                    {
                        Thread.Sleep(1);
                        PlusProgress();
                    }
                    else
                    {
                        CompleteProgress();
                        return;
                    }
                }
            }
        }

        private void StartSaveTask()
        {
            if (SaveTask != null && ProgressTask != null && SaveTask.ThreadState != ThreadState.Stopped && ProgressTask.ThreadState != ThreadState.Stopped)
            {
                MessageBox.Show("请等待上一次保存任务完成");
                return;
            }

            SaveTask = new Thread(Save);
            SaveTask.Start();
            ProgressTask = new Thread(() => UpdateProgress(null));
            ProgressTask.Start();
        }

        private void MenuItemClickSave(object sender, RoutedEventArgs e)
        {
            StartSaveTask();
        }

        private void CommandExecuteSave(object sender, CanExecuteRoutedEventArgs e)
        {
            StartSaveTask();
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
            }
        }

        private void StartProgress()
        {
            progressView.Dispatcher.BeginInvoke(new Action(() => {
                progressView.Value = value = 0;
                progressView.Visibility = Visibility.Visible;
                dataPanel.Visibility = Visibility.Hidden;
            }), DispatcherPriority.Normal);
        }

        private void CompleteProgress()
        {
            progressView.Dispatcher.BeginInvoke(new Action(() => {
                progressView.Value = value = 0;
                progressView.Visibility = Visibility.Collapsed;
                dataPanel.Visibility = Visibility.Visible;
            }), DispatcherPriority.Normal);
        }

        private void PlusProgress()
        {
            progressView.Dispatcher.BeginInvoke(new Action(() =>
            {
                progressView.Value = ++value;
            }), DispatcherPriority.Normal);
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
