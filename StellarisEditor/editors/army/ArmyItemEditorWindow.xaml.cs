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

namespace StellarisEditor.editors.army
{
    /// <summary>
    /// ArmyItemEditorWindow.xaml 的交互逻辑
    /// </summary>
    public partial class ArmyItemEditorWindow : Window
    {
        public PdxArmy Army { get; set; }

        public ArmyItemEditorWindow(PdxArmy Army)
        {
            this.Army = Army;
            InitializeComponent();
            DataContext = Army;
        }
        public ArmyItemEditorWindow()
        {
            InitializeComponent();
        }
    }
}
