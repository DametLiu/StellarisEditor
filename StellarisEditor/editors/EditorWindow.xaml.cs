using StellarisEditor.editors.job;
using StellarisEditor.editors.localization;
using StellarisEditor.editors.scriptedvariables;
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
        }

        private void OpenLocalizationEditor(object sender, RoutedEventArgs e)
        {
            LocalizationEditorWindow window = new LocalizationEditorWindow();
            window.ShowDialog();
        }

        private void OpenBuildingEditor(object sender, RoutedEventArgs e)
        {
            var win = new StellarisEditor.editors.building.BuildingEditorWindow();
            win.ShowDialog();
        }

        private void OpenJobEditor(object sender, RoutedEventArgs e)
        {

        }

        private void OpenVariableEditor(object sender, RoutedEventArgs e)
        {
            ScriptedVariableEditorWindow window = new ScriptedVariableEditorWindow();
            window.ShowDialog();
        }
    }
}
