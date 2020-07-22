using StellarisEditor.editors.job;
using StellarisEditor.editors.localization;
using StellarisEditor.editors.scriptedvariables;
using StellarisEditor.mod.data;
using StellarisEditor.pdx.scriptobject;
using System;
using System.Collections.Generic;
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

namespace StellarisEditor.editors
{
    /// <summary>
    /// EditorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class EditorWindow : Window
    {
        public EditorWindow()
        {
            InitializeComponent();

            ModGlobalData.LoadDatas();
        }

        private void OpenLocalizationEditor(object sender, RoutedEventArgs e)
        {
            var window = new LocalizationEditorWindow();
            window.ShowDialog();
        }

        private void OpenBuildingEditor(object sender, RoutedEventArgs e)
        {
            var win = new building.BuildingEditorWindow();
            win.ShowDialog();
        }

        private void OpenJobEditor(object sender, RoutedEventArgs e)
        {
            var win = new JobEditorWindow();
            win.ShowDialog();
        }

        private void OpenVariableEditor(object sender, RoutedEventArgs e)
        {
            var window = new ScriptedVariableEditorWindow();
            window.ShowDialog();
        }

        private void OpenTechTierEditor(object sender, RoutedEventArgs e)
        {
            var window = new techEditor.TechTierWindow();
            window.ShowDialog();
        }

        private void OpenTechCategoryEditor(object sender, RoutedEventArgs e)
        {
            var window = new techEditor.TechCategoryEditorWindow();
            window.ShowDialog();
        }
        private void OpenTechEditor(object sender, RoutedEventArgs e)
        {
            var window = new technology.TechnologyEditorWindow();
            window.ShowDialog();
        }
        private void OpenAgendaEditor(object sender, RoutedEventArgs e)
        {
            var window = new Agenda.AgendaEditorlist();
            window.ShowDialog();
        }
    }
}
