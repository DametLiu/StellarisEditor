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

    }
}
