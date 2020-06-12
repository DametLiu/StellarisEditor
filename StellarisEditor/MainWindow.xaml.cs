using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using StellarisEditor.data;
using StellarisEditor.mod.data;
using StellarisEditor.modmanager;
using StellarisEditor.ScriptEngine;
using StellarisEditor.utils;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static StellarisEditor.utils.FileUtil;

namespace StellarisEditor
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private Dispatcher dispatcher;


        public MainWindow()
        {
#if DEBUG
            String c = File.ReadAllText(@"C:\Users\dametliu\Documents\Paradox Interactive\Stellaris\mod\AAA\common\technology\00_apocalypse_tech.txt");
            Script s = new Script(new ParserConfig(), c);
            s.Parse();
            StringBuilder sb = new StringBuilder();
            sb.Append(Regex.Match("", @"@?\w*").Value).Append("\n");
            foreach (var n in s.Nodes)
            {
                sb.Append(n.ToString()).Append("\n");
            }
            File.WriteAllText(@"C:\Users\dametliu\Documents\Paradox Interactive\Stellaris\mod\AAA\common\technology\test.txt", sb.ToString());
            Close();
            return;
#endif
            Process.GetCurrentProcess().PriorityClass = ProcessPriorityClass.RealTime;
            InitializeComponent();
            dispatcher = Dispatcher.CurrentDispatcher;

            dispatcher.BeginInvoke(new Action(() => {
                var stellarisPath = Properties.Settings.Default.StellarisPath;
                if (!CheckStellarisFolderCorrect(stellarisPath))
                {
                    RegistryKey key = Registry.LocalMachine.OpenSubKey(@"SOFTWARE").OpenSubKey(@"Microsoft").OpenSubKey(@"Windows").OpenSubKey(@"CurrentVersion").OpenSubKey(@"Uninstall").OpenSubKey("Steam App 281990");
                    if (key != null)
                    {
                        object value = key.GetValue("InstallLocation");
                        if (value != null)
                            stellarisPath = value.ToString();
                    }

                    while (!CheckStellarisFolderCorrect(stellarisPath))
                    {
                        var dialog = new CommonOpenFileDialog
                        {
                            IsFolderPicker = true,
                            Multiselect = false,
                            Title = "选择群星安装目录",
                            ShowHiddenItems = false,
                            ShowPlacesList = true
                        };
                        CommonFileDialogResult result = dialog.ShowDialog();
                        if (result == CommonFileDialogResult.Ok && CheckStellarisFolderCorrect(dialog.FileName))
                        {
                            stellarisPath = dialog.FileName;
                            break;
                        }
                    }

                    Properties.Settings.Default.StellarisPath = stellarisPath;
                    Properties.Settings.Default.Save();

                }
                new Thread(delegate ()
                {
                    LoadModProjects();
                    PdxGlobalData.LoadTechnologyTiers(null);
                    PdxGlobalData.LoadTechnologyCategories(null);
                    //LoadScriptedVariablesData();
                    //LoadLocalizations();

                    SetProgressText("正在假装加载数据...");
                    while (true)
                    {
                        var value = 0.0;
                        dispatcher.Invoke(new Action(() =>
                        {
                            value = progressBar.Value;
                        }), DispatcherPriority.Normal);
                        if (value >= 100)
                            break;

                        Thread.Sleep(new Random().Next(1, 2));
                        SetProgressValue(0.1);
                    }

                    OpenModManagerWindow();
                    CloseWindow();

                })
                {
                    Priority = ThreadPriority.Highest
                }.Start();

                
            } ), DispatcherPriority.Normal);

            new Thread(delegate () {
                while (true)
                {
                    try
                    {
                        ChangeBackgroundImage(Properties.Resources.StellarisEditorLaucher);
                        ChangeBackgroundImage(Properties.Resources.StellarisEditorLaucher8);
                        ChangeBackgroundImage(Properties.Resources.StellarisEditorLaucher7);
                        ChangeBackgroundImage(Properties.Resources.StellarisEditorLaucher6);
                        ChangeBackgroundImage(Properties.Resources.StellarisEditorLaucher5);
                        ChangeBackgroundImage(Properties.Resources.StellarisEditorLaucher4);
                        ChangeBackgroundImage(Properties.Resources.StellarisEditorLaucher3);
                        ChangeBackgroundImage(Properties.Resources.StellarisEditorLaucher2);
                    }
                    catch (Exception)
                    {
                        break;
                    }
                }

            }).Start();
        }

        private void ChangeBackgroundImage(Bitmap bitmap)
        {
            var value = true;
            dispatcher.Invoke(new Action(() => { value = IsVisible; }), DispatcherPriority.Normal);

            if (!value)
                return;

            dispatcher.BeginInvoke(new Action(() => { Background = new ImageBrush(BitmapUtil.BitmapToBitmapImage(bitmap, 800, 450)); }), DispatcherPriority.Normal);
            Thread.Sleep(5000);
        }

        private static bool CheckStellarisFolderCorrect(string stellarisPath)
        {
            return !String.IsNullOrEmpty(stellarisPath) && Directory.Exists(stellarisPath) && File.Exists(stellarisPath + @"\stellaris.exe");
        }

        private void LoadLocalizations()
        {
            SetProgressText("正在加载本地化数据...");
            PdxGlobalData.LoadLocalizations(new PdxGlobalData.TaskCancel());
            SetProgressValue(1);
            Thread.Sleep(new Random().Next(500, 1000));
        }

        private void LoadModProjects()
        {
            SetProgressText("正在加载Mod项目列表...");
            ModGlobalData.LoadModProjects();
            SetProgressValue(1);
            Thread.Sleep(new Random().Next(500,1000));
        }

        private void OpenModManagerWindow()
        {
            dispatcher.BeginInvoke(new Action(() => {
                ModManagerWindow modManagerWindow = new ModManagerWindow();
                modManagerWindow.Show();
            }), DispatcherPriority.Normal);
        }

        private void LoadScriptedVariablesData()
        {
            SetProgressText("正在加载变量数据...");
            PdxGlobalData.LoadScriptedVariables(null);
            SetProgressValue(1);
            Thread.Sleep(new Random().Next(500, 1000));
        }

        public class UpdateFindFileState : IFileFindNotify
        {
            public MainWindow window;
            public UpdateFindFileState(MainWindow window)
            {
                this.window = window;
            }
            public void Notify(string filePath)
            {
                window.SetProgressTextValue("正在扫描文件： " + filePath, 0.0001);
            }
        }

        public void ShowMessageBox(String text, String title)
        {
            dispatcher.BeginInvoke(new Action(() => {
                MessageBox.Show(text, title);
            }), DispatcherPriority.Background);
        }

        public void CloseWindow()
        {
            dispatcher.BeginInvoke(new Action(() =>
            {
                Close();
            }), DispatcherPriority.Normal);
        }

        public void SetProgressText(String text)
        {
            dispatcher.BeginInvoke(new Action(() => {
                progressText.Text = text;
            }), DispatcherPriority.Normal);
        }

        public void SetProgressValue(Double value)
        {
            dispatcher.BeginInvoke(new Action(() => {
                progressBar.Value = progressBar.Value + value;
            }), DispatcherPriority.Normal);
        }

        public void SetProgressTextValue(String text, Double value)
        {
            dispatcher.BeginInvoke(new Action(() => {
                progressText.Text = text;
                progressBar.Value = progressBar.Value + value;
            }), DispatcherPriority.Normal);
            Thread.Sleep(1);
        }
    }
}
