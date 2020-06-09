using StellarisEditor.pdx.parser;
using StellarisEditor.pdx.scriptobject;
using StellarisEditor.utils;
using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
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
using Color = System.Windows.Media.Color;
using Image = System.Windows.Controls.Image;

namespace StellarisEditor.editors.localization
{
    /// <summary>
    /// LocalizationItemEditeWindow.xaml 的交互逻辑
    /// </summary>
    public partial class LocalizationItemEditeWindow : Window
    {
        public PdxLocalization Localization { get; set; }
        private Object LastTextBox;
        private const string IconPath = "Resources";

        public LocalizationItemEditeWindow(PdxLocalization Localization)
        {
            this.Localization = Localization;
            InitializeComponent();
            LoadAllIcon();
            DataContext = Localization;
        }

        public LocalizationItemEditeWindow()
        {
            InitializeComponent();
        }

        private void LoadAllIcon()
        {
            string[] files = Directory.GetFiles(IconPath);
            foreach (var fpath in files) {
                var fname = System.IO.Path.GetFileNameWithoutExtension(fpath);
                var ext = System.IO.Path.GetExtension(fpath);
                if (fname.Contains("StellarisEditorLaucher") || ext != ".png") {
                    continue;
                }
                else {
                    var button = new Button() {
                        Width = 24,
                        Height = 24,
                        Name = fname,
                        ToolTip = fname
                    };
                    button.Click += IconClick;
                    var imageBrush = new ImageBrush {
                        ImageSource = new BitmapImage(new Uri(fpath, UriKind.RelativeOrAbsolute))
                    };
                    button.Background = imageBrush;

                    iconPanel.Children.Add(button);
                }
            }
        }

        private void ColorClick(object sender, RoutedEventArgs e)
        {
            if (LastTextBox is TextBox textbox) {
                int selectionStart = textbox.SelectionStart;
                if (e.Source == CM)
                    textbox.Text = textbox.Text.Insert(selectionStart, "§M");
                else if (e.Source == CL)
                    textbox.Text = textbox.Text.Insert(selectionStart, "§L");
                else if (e.Source == CG)
                    textbox.Text = textbox.Text.Insert(selectionStart, "§G");
                else if (e.Source == CR)
                    textbox.Text = textbox.Text.Insert(selectionStart, "§R");
                else if (e.Source == CB)
                    textbox.Text = textbox.Text.Insert(selectionStart, "§B");
                else if (e.Source == CY)
                    textbox.Text = textbox.Text.Insert(selectionStart, "§Y");
                else if (e.Source == CH)
                    textbox.Text = textbox.Text.Insert(selectionStart, "§H");
                else if (e.Source == CT)
                    textbox.Text = textbox.Text.Insert(selectionStart, "§T");
                else if (e.Source == CE)
                    textbox.Text = textbox.Text.Insert(selectionStart, "§E");
                else if (e.Source == CS)
                    textbox.Text = textbox.Text.Insert(selectionStart, "§S");
                else if (e.Source == CW)
                    textbox.Text = textbox.Text.Insert(selectionStart, "§W");
                else if (e.Source == CP)
                    textbox.Text = textbox.Text.Insert(selectionStart, "§P");
                else if (e.Source == Cg)
                    textbox.Text = textbox.Text.Insert(selectionStart, "§g");
                else if (e.Source == Cl)
                    textbox.Text = textbox.Text.Insert(selectionStart, "§l");
                else if (e.Source == CD)
                    textbox.Text = textbox.Text.Insert(selectionStart, "§!");
                textbox.Focus();
                textbox.Select(selectionStart, 0);
            }
        }

        private void TextBoxLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            LastTextBox = e.Source;
        }

        private void IconClick(object sender, RoutedEventArgs e)
        {
            if (LastTextBox is TextBox textbox) {
                int selectionStart = textbox.SelectionStart;
                if (selectionStart != textbox.Text.Length && textbox.Text[selectionStart] == '£')
                    textbox.Text = textbox.Text.Insert(selectionStart, " ");

                var button = e.Source as Button;
                textbox.Text = textbox.Text.Insert(selectionStart, $"£{button.Name}£");

                textbox.Focus();
                textbox.Select(selectionStart, 0);
            }
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            DialogResult = true;
        }

        private void ChineseView_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox view = e.Source as TextBox;

            if (view.Text.Length == 0) {
                if (view == chineseView)
                    chineseViewR.Document.Blocks.Clear();
                else if (view == englishView)
                    englishViewR.Document.Blocks.Clear();
                return;
            }

            Paragraph paragraph = new PdxRichTextPreviewParser().ParseText(view.Text);
            if (view == chineseView)
                ResetPreview(paragraph, chineseViewR);
            else if (view == englishView)
                ResetPreview(paragraph, englishViewR);
        }

        private void ResetPreview(Paragraph paragraph, RichTextBox richTextBox)
        {
            richTextBox.Document.Blocks.Clear();
            richTextBox.Document.Blocks.Add(paragraph);
        }
    }
}
