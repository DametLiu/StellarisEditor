using StellarisEditor.pdx.parser;
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
using StellarisEditor.editors.dataConversion;

namespace StellarisEditor.editors.Agenda
{
    /// <summary>
    /// AgendaEditorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class AgendaEditorWindow : Window
    {
        public PdxAgenda Agenda { get; set; }
        public AgendaEditorWindow(PdxAgenda Agenda)
        {
            this.Agenda = Agenda;
            InitializeComponent();
            DataContext = Agenda;
        }

        public AgendaEditorWindow()
        {
            InitializeComponent();
        }

        private void AgendaModifierDelete_Click(object sender, RoutedEventArgs e)
        {
            Agenda.Modifier.RemoveAt(Agenda.Modifier.Count - 1);
            if (Agenda.Modifier.Count==0)
            {
                AgendaModifierDelete.IsEnabled = false;
            }
        }

        private void AgendaModifierAdd_Click(object sender, RoutedEventArgs e)
        {
            Agenda.Modifier.Add(new pdx.scriptobject.Expression() { Key = "", Operator = "", Value = "" });
            AgendaModifierDelete.IsEnabled = true;
        }
        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            TreeViewTransition.TrimExpressionTransform(Agenda.Modifier);
            Modifier.IsEnabled = true;
        }
    }
}
