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

namespace StellarisEditor.editors.technology
{
    /// <summary>
    /// TechnologyEditorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class TechnologyEditorWindow : Window
    {
        public ObservableCollection<Technology> Technologies = new ObservableCollection<Technology>();
        public TaskCancel cancel = new TaskCancel();
        public TechnologyEditorWindow()
        {
            InitializeComponent();
            dataGrid.ItemsSource = Technologies;
            //ModGlobalData.LoadTechnologies(cancel);
            ProgressTask = new Thread(() => UpdateProgress(UpdateData));
            ProgressTask.Start();
        }

        private int updateCount = 0;
        private void UpdateData()
        {
            if (Technologies.Count != 0 || updateCount > 0)
                return;

            var current = 0; updateCount = ModGlobalData.Technologies.Count;
            dataGrid.Dispatcher.Invoke(new Action(() => {
                current = progressView.Value;
            }), DispatcherPriority.Normal);

            for (int i = 0; i < ModGlobalData.Technologies.Count; i++)
            {
                var item = ModGlobalData.Technologies.ElementAt(i);
                if (!Technologies.Contains(item))
                {
                    dataGrid.Dispatcher.BeginInvoke(new Action(() => {
                        Technologies.Add(item);
                        int a = current + (Technologies.Count / ModGlobalData.Technologies.Count * (99 - current));
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
            if (dataGrid.SelectedItem is Technology technology)
            {
                TechnologyItemEditorWindow window = new TechnologyItemEditorWindow(technology);
                window.ShowDialog();

                var selectIndex = dataGrid.SelectedIndex;
                Technologies.RemoveAt(selectIndex);
                Technologies.Insert(selectIndex, window.Technology);
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

        private Technology CopyTechnology;
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
            if (dataGrid.SelectedItem is Technology technology)
                CopyTechnology = technology;
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
            //if (CopyTechnology is PdxTechnology technology)
            //{
            //    PdxTechnology pdxTechnology = technology.Clone();
            //    pdxTechnology.Key = pdxTechnology.Key + "_copy";
            //    Technologies.Insert(Technologies.IndexOf(technology), pdxTechnology);
            //}
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
            if (dataGrid.SelectedItem is Technology technology)
                Technologies.Remove(technology);
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
            if (dataGrid.SelectedItem is Technology technology)
                Technologies.Insert(Technologies.IndexOf(technology), new Technology() { Key = $"new_technology{++NewIndex}" });
            else
                Technologies.Insert(0, new Technology() { Key = $"new_technology{++NewIndex}" });
        }

        private int NewIndex = 0;
    }
    public class StrToBoolDefultFalse : IValueConverter//string与bool相互转换，默认为false
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strToBool = (string)value;
            if (strToBool == "yes")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? boolToStr = (bool?)value;
            if (boolToStr == true)
            {
                return "yes";
            }
            else
            {
                return "no";
            }
        }
    }
    public class StrToBoolDefaultTure : IValueConverter//string与bool相互转换，默认为true
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string strToBool = (string)value;
            if (strToBool == "no")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            bool? boolToStr = (bool?)value;
            if (boolToStr == false)
            {
                return "no";
            }
            else
            {
                return "yes";
            }
        }
    }

    public class GatherToStr : IValueConverter//集合与string相互转换，通过\n进行分隔
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            ObservableCollection<string> vs = (ObservableCollection<string>)value;
            string str = null;
            if (vs == null)
            {
                return str;
            }
            else
            {
                foreach (var item in vs)
                {
                    str += (item.ToString() + "\n");
                }
                return str;
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = (string)value;
            ObservableCollection<string> vs = new ObservableCollection<string>(str.Split('\n'));
            return vs;
        }
    }
}
