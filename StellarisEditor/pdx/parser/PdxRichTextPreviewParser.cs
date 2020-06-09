using StellarisEditor.utils;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Media;
using Color = System.Windows.Media.Color;
using Image = System.Windows.Controls.Image;

namespace StellarisEditor.pdx.parser
{
    public class PdxRichTextPreviewParser
    {
        Int32 index = 0;
        char current = (char)0;
        public Paragraph ParseText(String text)
        {
            var list = new LinkedList<Object>();
            
            while (true)
            {
                if (index + 1 >= text.Length)
                    break;
                var current = text[index];
                if (current == '£')
                {
                    current = text[++index];
                    StringBuilder sb = new StringBuilder();
                    try
                    {
                        for (; ; )
                        {
                            if (current != '£' && current != ' ' && current != '§' && current != '\\')
                                sb.Append(current);
                            else
                                break;

                            if (index + 1 == text.Length)
                                list.AddLast(new TextIcon() { Key = sb.ToString() });
                            current = text[++index];
                        }
                        if (current == ' ' || current == '§')
                        {
                            list.AddLast(new TextIcon() { Key = sb.ToString() });
                            current = text[++index];
                        }   
                        else if (current == '\\')
                        {
                            list.AddLast(new TextIcon() { Key = sb.ToString() });
                            current = text[(index = index + 2)];
                        }
                        else if (current == '£')
                        {
                            if (index +1 == text.Length)
                            {
                                list.AddLast(new TextIcon() { Key = sb.ToString() });
                                current = text[(index = index + 2)];
                            }
                            else if (text[index +1] == '£')
                            {
                                current = text[(index = index + 2)];
                                list.AddLast(new TextIcon() { Key = sb.ToString() });
                            }
                            else if (text[index + 1] != '£')
                            {
                                current = text[(index = index + 1)];
                                list.AddLast(new TextIcon() { Key = sb.ToString() });
                            }
                        }
                    }
                    catch (Exception) { }

                    
                }
                else if (current == '§')
                {
                    
                    try
                    {
                        current = text[index = index + 1];
                        var key = current;
                        StringBuilder sb = new StringBuilder();
                        current = text[++index];

                        for (; ; )
                        {
                            if (current != '£' && current != '§')
                                sb.Append(current);
                            else
                                break;

                            if (index + 1 == text.Length)
                                list.AddLast(new TextColor() { Key = key.ToString(), Text = sb.ToString() });
                            current = text[++index];
                        }
                        
                        list.AddLast(new TextColor() { Key = key.ToString(), Text = sb.ToString() });
                    }
                    catch (Exception) { }

                    
                }
                else
                {
                    var key = "|";
                    StringBuilder sb = new StringBuilder();
                    try
                    {
                        for (; ; )
                        {
                            if (current != '£' && current != '§')
                                sb.Append(current);
                            else
                                break;
                            current = text[++index];
                        }
                    }
                    catch (Exception) { }

                    list.AddLast(new TextColor() { Key = key.ToString(), Text = sb.ToString() });
                }

            }


            ValidateTextColor(list);
            return GeneratePreview(list);
        }

        private void ValidateTextColor(LinkedList<object> list)
        {
            if (list.Count == 0)
                return;

            var colorKey = "!";
            foreach (var item in list)
            {
                if (item is TextColor color)
                {
                    if (color.Key == "|")
                        color.Key = colorKey;
                    else
                        colorKey = color.Key;
                }
            }
        }

        private Paragraph GeneratePreview(LinkedList<object> list)
        {
            // 富文本预览
            Paragraph paragraph = new Paragraph();
            foreach (var a in list)
            {
                if (a is TextColor textColor)
                {
                    paragraph.Inlines.Add(new Run() { Text = textColor.Text.Replace("\\n", "\n"), Foreground = new SolidColorBrush(GetKeyColor(textColor.Key)) });
                }
                else if (a is TextIcon textIcon)
                {
                    var image = GetKeyImage(textIcon.Key);
                    if (image != null)
                    {
                        paragraph.Inlines.Add(new InlineUIContainer(new Image
                        {
                            Source = BitmapUtil.BitmapToBitmapImage(image, 20, 20),
                            Width = 20,
                            Height = 20
                        }));
                    }
                }
                    
            }
            return paragraph;
        }

        private Bitmap GetKeyImage(string key)
        {
            if (key == "energy")
                return Properties.Resources.energy;
            else if (key == "minerals")
                return Properties.Resources.minerals;
            else if (key == "food")
                return Properties.Resources.food;
            else if (key == "consumer_goods")
                return Properties.Resources.consumer_goods;
            else if (key == "unity")
                return Properties.Resources.unity;
            else if (key == "influence")
                return Properties.Resources.influence;
            else if (key == "alloys")
                return Properties.Resources.alloys;
            else if (key == "physics_research")
                return Properties.Resources.physics_research;
            else if (key == "society_research")
                return Properties.Resources.society_research;
            else if (key == "engineering_research")
                return Properties.Resources.engineering_research;
            else if (key == "volatile_motes")
                return Properties.Resources.volatile_motes;
            else if (key == "exotic_gases")
                return Properties.Resources.exotic_gases;
            else if (key == "rare_crystals")
                return Properties.Resources.rare_crystals;
            else if (key == "trade_value")
                return Properties.Resources.trade_value;
            else if (key == "time")
                return Properties.Resources.time;
            else if (key == "sr_zro")
                return Properties.Resources.sr_zro;
            else if (key == "sr_living_metal")
                return Properties.Resources.sr_living_metal;
            else if (key == "sr_dark_matter")
                return Properties.Resources.sr_dark_matter;
            else if (key == "nanites")
                return Properties.Resources.nanites;
            else if (key == "minor_artifacts")
                return Properties.Resources.minor_artifacts;
            else if (key == "ship_stats_armor")
                return Properties.Resources.ship_stats_armor;
            else if (key == "ship_stats_build_cost")
                return Properties.Resources.ship_stats_build_cost;
            else if (key == "ship_stats_build_time")
                return Properties.Resources.ship_stats_build_time;
            else if (key == "ship_stats_damage")
                return Properties.Resources.ship_stats_damage;
            else if (key == "ship_stats_evasion")
                return Properties.Resources.ship_stats_evasion;
            else if (key == "ship_stats_hitpoints")
                return Properties.Resources.ship_stats_hit_points;
            else if (key == "ship_stats_maintenance")
                return Properties.Resources.ship_stats_maintenance;
            else if (key == "ship_stats_piracy_supression")
                return Properties.Resources.ship_stats_piracy_suppression;
            else if (key == "ship_stats_power")
                return Properties.Resources.ship_stats_power;
            else if (key == "ship_stats_shield")
                return Properties.Resources.ship_stats_shield;
            else if (key == "ship_stats_special")
                return Properties.Resources.ship_stats_special;
            else if (key == "ship_stats_speed")
                return Properties.Resources.ship_stats_speed;
            else if (key == "")
                return null;

            return Properties.Resources.placeholder;
        }

        private Color GetKeyColor(string key)
        {
            if (key == "M")
                return Color.FromRgb(0xa3, 0x35, 0xee);
            else if (key == "L")
                return Color.FromRgb(0xc3, 0xb0, 0x91);
            else if (key == "G")
                return Color.FromRgb(0x29, 0xe1, 0x26);
            else if (key == "R")
                return Color.FromRgb(0xfc, 0x56, 0x46);
            else if (key == "B")
                return Color.FromRgb(0x33, 0xa7, 0xff);
            else if (key == "Y") 
                return Color.FromRgb(0xF9, 0xff, 0x00);
            else if (key == "H")
                return Color.FromRgb(0xfb, 0xaa, 0x29);
            else if (key == "T")
                return Color.FromRgb(0xff, 0xff, 0xff);
            else if (key == "E")
                return Color.FromRgb(0x87, 0xff, 0xcf);
            else if (key == "S") 
                return Color.FromRgb(0xe4, 0x9c, 0x2a);
            else if (key == "W")
                return Color.FromRgb(0xff, 0xff, 0xff);
            else if (key == "P")
                return Color.FromRgb(0xe1, 0x6e, 0x6e);
            else if (key == "g")
                return Color.FromRgb(0x80, 0x80, 0x80);
            else if (key == "l")
                return Color.FromRgb(0x33, 0xa7, 0xff);
            else if (key == "!")
                return Color.FromRgb(0x00, 0x00, 0x00);
            return Color.FromRgb(0x00, 0x00, 0x00);
        }

        class TextIcon
        {
            public String Key;

            public override string ToString()
            {
                return Key;
            }
        }

        class TextColor
        {
            public String Text;
            public String Key;

            public override string ToString()
            {
                return $"{{{Key}={Text}}}";
            }
        }
    }
}
