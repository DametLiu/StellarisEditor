using StellarisEditor.data;
using StellarisEditor.mod.data;
using StellarisEditor.pdx.scriptobject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;
using static StellarisEditor.data.PdxGlobalData;

namespace StellarisEditor.editors.localization
{
    /// <summary>
    /// LocalizationEditorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LocalizationEditorWindow : Window
    {
        public ObservableCollection<PdxLocalization> Localizations = new ObservableCollection<PdxLocalization>();
        public TaskCancel cancel = new TaskCancel();
        public LocalizationEditorWindow()
        {
            InitializeComponent();
            dataGrid.ItemsSource = Localizations;

            ModGlobalData.LoadLocalizations(cancel);
            ProgressTask = new Thread(() => UpdateProgress(UpdateData));
            ProgressTask.Start();
        }

        private void UpdateData()
        {
            if (Localizations.Count != 0)
                return;

            var current = 0;
            dataGrid.Dispatcher.Invoke(new Action(() => {
                current = progressView.Value;
            }), DispatcherPriority.Normal);

            for (int i = 0; i < ModGlobalData.Localizations.Count; i++)
            {
                var item = ModGlobalData.Localizations.ElementAt(i);
                if (!Localizations.Contains(item))
                {
                    dataGrid.Dispatcher.BeginInvoke(new Action(() => {
                        Localizations.Add(item);
                        int a = current + (Localizations.Count / ModGlobalData.Localizations.Count * (99 - current));
                        progressView.Value = value = a;
                    }), DispatcherPriority.Normal);
                }
            }
        }

        private Int32 value = 0;
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
                        Thread.Sleep(20);
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

        delegate void DelegateThreadFunction();

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

        protected override void OnClosing(CancelEventArgs e)
        {
            if (SaveTask != null && SaveTask.IsAlive)
            {
                e.Cancel = true;
                MessageBox.Show("请等待保存完成在关闭窗口");
            }
        }

        protected override void OnClosed(EventArgs e)
        {
            cancel.Cancel = true;
        }

        private void PlusProgress()
        {
            progressView.Dispatcher.BeginInvoke(new Action(() =>
            {
                progressView.Value = ++value;
            }), DispatcherPriority.Normal);
        }

        private Thread SaveTask;
        private Thread ProgressTask;


        private void MouseDoubleClick_EditItem(object sender, MouseButtonEventArgs e)
        {
            if (dataGrid.SelectedItem is PdxLocalization localization)
            {
                LocalizationItemEditeWindow window = new LocalizationItemEditeWindow(localization);
                window.ShowDialog();

                var selectIndex = dataGrid.SelectedIndex;
                Localizations.RemoveAt(selectIndex);
                Localizations.Insert(selectIndex, window.Localization);
            }
        }

        private void CommandExecuteSave(object sender, CanExecuteRoutedEventArgs e)
        {
            StartSaveTask();
        }

        private void StartSaveTask()
        {
            if (SaveTask != null && ProgressTask != null && SaveTask.ThreadState != ThreadState.Stopped && ProgressTask.ThreadState != ThreadState.Stopped)
            {
                MessageBox.Show("请等待上一次保存任务完成");
                return;
            }

            SaveTask = new Thread(() => Save());
            SaveTask.Start();
            ProgressTask = new Thread(() => UpdateProgress(() => CompleteSave()));
            ProgressTask.Start();
        }

        private void CompleteSave()
        {
            
        }

        private void MenuItemClickSave(object sender, RoutedEventArgs e)
        {
            StartSaveTask();
        }

        private void Save()
        {
            String en = new StringBuilder().Append(Properties.Settings.Default.ModPath).Append(PdxGlobalData.STELLARIS_PATH_LOCALIZATION_ENGLISH).ToString();
            String zh = new StringBuilder().Append(Properties.Settings.Default.ModPath).Append(PdxGlobalData.STELLARIS_PATH_LOCALIZATION_SIMPLE_CHINESE).ToString();
            Directory.CreateDirectory(en);
            Directory.CreateDirectory(zh);

            var gs = Localizations.GroupBy(l => l.FileName);
            foreach (var g in gs)
            {
                String[] sa_en = new string[g.Count() + 1];
                sa_en[0] = "l_english:";
                int i = 1;
                foreach (var l in g)
                    sa_en[i++] = l.ToStringValueEnglish();
                File.WriteAllLines(en + $"{(String.IsNullOrWhiteSpace(g.Key) ? "l_english" : g.Key)}{(g.Key.Contains("l_english") ? "" : "_l_english")}.yml", sa_en, new UTF8Encoding(true));

                String[] sa_ch = new string[sa_en.Length];
                sa_ch[0] = "l_simp_chinese:";
                i = 1;
                foreach (var l in g)
                    sa_ch[i++] = l.ToStringValueSimpChinese();
                File.WriteAllLines(zh + $"{(String.IsNullOrWhiteSpace(g.Key) ? "l_simp_chinese" : g.Key)}{(g.Key.Contains("l_simp_chinese") ? "" : "_l_simp_chinese")}.yml", sa_ch, new UTF8Encoding(true));
            }
        }

        private PdxLocalization CopyLocalization;
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
            if (dataGrid.SelectedItem is PdxLocalization localization)
                CopyLocalization = localization;
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
            if (CopyLocalization is PdxLocalization localization)
            {
                PdxLocalization pdxLocalization = localization.Clone();
                pdxLocalization.Key = pdxLocalization.Key + "_copy";
                Localizations.Insert(Localizations.IndexOf(localization), pdxLocalization);
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
            if (dataGrid.SelectedItem is PdxLocalization localization)
                Localizations.Remove(localization);
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
            if (dataGrid.SelectedItem is PdxLocalization localization)
                Localizations.Insert(Localizations.IndexOf(localization), new PdxLocalization() { Key = $"new_localization{++NewIndex}" });
            else
                Localizations.Insert(0, new PdxLocalization() { Key = $"new_localization{++NewIndex}" });
        }

        private int NewIndex = 0;
    }
}
