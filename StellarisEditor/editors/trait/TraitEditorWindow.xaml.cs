using StellarisEditor.data;
using StellarisEditor.editors.localization;
using StellarisEditor.extension;
using StellarisEditor.mod.data;
using StellarisEditor.pdx.scriptobject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Globalization;
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
using static StellarisEditor.data.PdxGlobalData;

namespace StellarisEditor.editors.trait
{
    /// <summary>
    /// TraitEditorWindow.xaml 的交互逻辑
    /// </summary>
    /// 
    public class FakeGlobalData
    {
        public static LinkedList<PdxTrait> Traits = new LinkedList<PdxTrait>();//这是假的数据
    }
    public partial class TraitEditorWindow : Window
    {
        public ObservableCollection<PdxTrait> Traits = new ObservableCollection<PdxTrait>();
        public TaskCancel cancel = new TaskCancel();
        public TraitEditorWindow()
        {
            InitializeComponent();
            dataGrid.ItemsSource = Traits;
            //ModGlobalData.LoadTraits(cancel);
            ProgressTask = new Thread(() => UpdateProgress(UpdateData));
            ProgressTask.Start();
        }

        private int updateCount = 0;
        private void UpdateData()
        {
            if (Traits.Count != 0 || updateCount > 0)
                return;

            var current = 0; updateCount = FakeGlobalData.Traits.Count;
            dataGrid.Dispatcher.Invoke(new Action(() =>
            {
                current = progressView.Value;
            }), DispatcherPriority.Normal);

            for (int i = 0; i < FakeGlobalData.Traits.Count; i++)
            {
                var item = FakeGlobalData.Traits.ElementAt(i);
                if (!Traits.Contains(item))
                {
                    dataGrid.Dispatcher.BeginInvoke(new Action(() =>
                    {
                        Traits.Add(item);
                        int a = current + (Traits.Count / FakeGlobalData.Traits.Count * (99 - current));
                        progressView.Value = value = a;
                        updateCount--;
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

        delegate void DelegateThreadFunction();

        private void StartProgress()
        {
            progressView.Dispatcher.BeginInvoke(new Action(() =>
            {
                progressView.Value = value = 0;
                progressView.Visibility = Visibility.Visible;
                dataPanel.Visibility = Visibility.Hidden;
            }), DispatcherPriority.Normal);
        }

        private void CompleteProgress()
        {
            progressView.Dispatcher.BeginInvoke(new Action(() =>
            {
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
            if (dataGrid.SelectedItem is PdxTrait trait)
            {
                TraitItemEditorWindow window = new TraitItemEditorWindow(trait);
                window.ShowDialog();

                var selectIndex = dataGrid.SelectedIndex;
                Traits.RemoveAt(selectIndex);
                Traits.Insert(selectIndex, window.Trait);
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

        }

        private PdxTrait CopyTrait;
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
            if (dataGrid.SelectedItem is PdxTrait trait)
                CopyTrait = trait;
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
            if (dataGrid.SelectedItem is PdxTrait trait)
                Traits.Remove(trait);
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
            if (dataGrid.SelectedItem is PdxTrait trait)
                Traits.Insert(Traits.IndexOf(trait), new PdxTrait() { Key = $"new_trait{++NewIndex}" });
            else
                Traits.Insert(0, new PdxTrait() { Key = $"new_trait{++NewIndex}" });
        }

        private int NewIndex = 0;
    }
}
