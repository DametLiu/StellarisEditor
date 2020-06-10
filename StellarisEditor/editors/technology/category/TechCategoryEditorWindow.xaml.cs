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
        public ObservableCollection<PdxTechnologyCategory> TechnologyCategories = new ObservableCollection<PdxTechnologyCategory>();
        public TechCategoryEditorWindow()
        {
            InitializeComponent();
            dataGrid.ItemsSource = TechnologyCategories;

            ModGlobalData.LoadScriptedVariables(null);
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
            if (TechnologyCategories.Count != 0 && updateCount > 0)
                return;

            var current = value; updateCount = ModGlobalData.TechnologyCategories.Count;
            for (int i = 0; i < ModGlobalData.TechnologyCategories.Count; i++)
            {
                var item = ModGlobalData.TechnologyCategories.ElementAt(i);
                if (!TechnologyCategories.Contains(item))
                {
                    int a = current + (TechnologyCategories.Count / ModGlobalData.Variables.Count * (99 - current));
                    dataGrid.Dispatcher.BeginInvoke(new Action(() => {
                        TechnologyCategories.Add(item);
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
                    if (value < 99)
                    {
                        Thread.Sleep(10);
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
            Directory.CreateDirectory(Properties.Settings.Default.ModPath + PdxGlobalData.STELLARIS_PATH_TECHNOLOGY_CATEGORY);

            var gs = TechnologyCategories.GroupBy(l => l.FileName);
            foreach (var g in gs)
            {
                MessageBox.Show($"{g.Key}");
                String[] lines = new string[g.Count()];
                int i = 0;
                foreach (var l in g)
                    lines[i++] = l.ToString();
                File.WriteAllLines($"{Properties.Settings.Default.ModPath + PdxGlobalData.STELLARIS_PATH_TECHNOLOGY_CATEGORY}{g.Key}.txt", lines, new UTF8Encoding(false));
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

        private PdxTechnologyCategory CopyTechnologyCategory;
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
            if (dataGrid.SelectedItem is PdxTechnologyCategory technologyCategory)
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
            if (CopyTechnologyCategory is PdxTechnologyCategory technologyCategory)
            {
                PdxTechnologyCategory pdxTechnologyCategory = technologyCategory.Clone();
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
            if (dataGrid.SelectedItem is PdxTechnologyCategory technologyCategory)
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
            if (dataGrid.SelectedItem is PdxTechnologyCategory technologyCategory)
                TechnologyCategories.Insert(TechnologyCategories.IndexOf(technologyCategory), new PdxTechnologyCategory() { Key = $"new_technology_category{++NewIndex}" });
            else
                TechnologyCategories.Insert(0, new PdxTechnologyCategory() { Key = $"new_technology_category{++NewIndex}" });
        }

        private int NewIndex = 0;
    }
}
