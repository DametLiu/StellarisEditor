using StellarisEditor.pdx.parser;
using StellarisEditor.pdx.scriptobject;
using StellarisEditor.utils;
using System;
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
        public LocalizationItemEditeWindow(PdxLocalization Localization)
        {
            this.Localization = Localization;
            InitializeComponent();
            DataContext = Localization;
        }

        public LocalizationItemEditeWindow()
        {
            InitializeComponent();
        }

        private void ColorClick(object sender, RoutedEventArgs e)
        {
            if (LastTextBox is TextBox textbox)
            {
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

        private Object LastTextBox;
        private void TextBoxLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            LastTextBox = e.Source;
        }

        private void IconClick(object sender, RoutedEventArgs e)
        {
            if (LastTextBox is TextBox textbox)
            {
                int selectionStart = textbox.SelectionStart;
                if (selectionStart != textbox.Text.Length && textbox.Text[selectionStart] == '£')
                    textbox.Text = textbox.Text.Insert(selectionStart, " ");

                if (e.Source == alloys)
                    textbox.Text = textbox.Text.Insert(selectionStart, "£alloys£");
                else if (e.Source == consumer_goods)
                    textbox.Text = textbox.Text.Insert(selectionStart, "£consumer_goods£");
                else if (e.Source == energy)
                    textbox.Text = textbox.Text.Insert(selectionStart, "£energy£");
                else if (e.Source == engineering_research)
                    textbox.Text = textbox.Text.Insert(selectionStart, "£engineering_research£");
                else if (e.Source == exotic_gases)
                    textbox.Text = textbox.Text.Insert(selectionStart, "£exotic_gases£");
                else if (e.Source == food)
                    textbox.Text = textbox.Text.Insert(selectionStart, "£food£");
                else if (e.Source == minerals)
                    textbox.Text = textbox.Text.Insert(selectionStart, "£minerals£");
                else if (e.Source == influence)
                    textbox.Text = textbox.Text.Insert(selectionStart, "£influence£");
                else if (e.Source == minor_artifacts)
                    textbox.Text = textbox.Text.Insert(selectionStart, "£minor_artifacts£");
                else if (e.Source == nanites)
                    textbox.Text = textbox.Text.Insert(selectionStart, "£nanites£");
                else if (e.Source == physics_research)
                    textbox.Text = textbox.Text.Insert(selectionStart, "£physics_research£");
                else if (e.Source == rare_crystals)
                    textbox.Text = textbox.Text.Insert(selectionStart, "£rare_crystals£");
                else if (e.Source == society_research)
                    textbox.Text = textbox.Text.Insert(selectionStart, "£society_research£");
                else if (e.Source == sr_dark_matter)
                    textbox.Text = textbox.Text.Insert(selectionStart, "£sr_dark_matter£");
                else if (e.Source == sr_living_metal)
                    textbox.Text = textbox.Text.Insert(selectionStart, "£sr_living_metal£");
                else if (e.Source == sr_zro)
                    textbox.Text = textbox.Text.Insert(selectionStart, "£sr_zro£");
                else if (e.Source == trade_value)
                    textbox.Text = textbox.Text.Insert(selectionStart, "£trade_value£");
                else if (e.Source == unity)
                    textbox.Text = textbox.Text.Insert(selectionStart, "£unity£");
                else if (e.Source == volatile_motes)
                    textbox.Text = textbox.Text.Insert(selectionStart, "£volatile_motes£");
                else if (e.Source == time)
                    textbox.Text = textbox.Text.Insert(selectionStart, "£time£");
                else if (e.Source == ship_stats_armor)
                    textbox.Text = textbox.Text.Insert(selectionStart, "£ship_stats_armor£");
                else if (e.Source == ship_stats_build_cost)
                    textbox.Text = textbox.Text.Insert(selectionStart, "£ship_stats_build_cost£");
                else if (e.Source == ship_stats_build_time)
                    textbox.Text = textbox.Text.Insert(selectionStart, "£ship_stats_build_time£");
                else if (e.Source == ship_stats_damage)
                    textbox.Text = textbox.Text.Insert(selectionStart, "£ship_stats_damage£");
                else if (e.Source == ship_stats_evasion)
                    textbox.Text = textbox.Text.Insert(selectionStart, "£ship_stats_evasion£");
                else if (e.Source == ship_stats_hitpoints)
                    textbox.Text = textbox.Text.Insert(selectionStart, "£ship_stats_hitpoints£");
                else if (e.Source == ship_stats_maintenance)
                    textbox.Text = textbox.Text.Insert(selectionStart, "£ship_stats_maintenance£");
                else if (e.Source == ship_stats_piracy_supression)
                    textbox.Text = textbox.Text.Insert(selectionStart, "£ship_stats_piracy_supression£");
                else if (e.Source == ship_stats_power)
                    textbox.Text = textbox.Text.Insert(selectionStart, "£ship_stats_power£");
                else if (e.Source == ship_stats_shield)
                    textbox.Text = textbox.Text.Insert(selectionStart, "£ship_stats_shield£");
                else if (e.Source == ship_stats_special)
                    textbox.Text = textbox.Text.Insert(selectionStart, "£ship_stats_special£");
                else if (e.Source == ship_stats_speed)
                    textbox.Text = textbox.Text.Insert(selectionStart, "£ship_stats_speed£");
                
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

            if (view.Text.Length == 0)
            {
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
