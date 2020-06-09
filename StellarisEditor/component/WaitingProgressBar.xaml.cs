using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace StellarisEditor.component
{
    /// <summary>
    /// WaitingProgressBar.xaml 的交互逻辑
    /// </summary>
    public partial class WaitingProgressBar : UserControl
    {
        public static readonly DependencyProperty ValueProperty = DependencyProperty.Register("Value", typeof(Int32), typeof(WaitingProgressBar), new PropertyMetadata(-1, new PropertyChangedCallback(OnValueChange)));

        private static void OnValueChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is WaitingProgressBar waitingProgressBar)
            {
                waitingProgressBar.InvokeValueChange();
            }
        }

        private void InvokeValueChange()
        {
            var value = Value;
            value = Math.Max(0, value);
            value = Math.Min(99, value);

            progressView.Text = value < 10 ? $"0{value}" : $"{value}";
        }

        public Int32 Value
        {
            get
            {
                return (Int32) GetValue(ValueProperty);
            }

            set
            {
                SetValue(ValueProperty, value);
            }
        }

        public WaitingProgressBar()
        {
            InitializeComponent();
        }
        
    }
}
