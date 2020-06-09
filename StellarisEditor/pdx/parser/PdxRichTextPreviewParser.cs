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

            while (true) {
                if (index + 1 >= text.Length)
                    break;
                var current = text[index];
                if (current == '£') {
                    current = text[++index];
                    StringBuilder sb = new StringBuilder();
                    try {
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
                        if (current == ' ' || current == '§') {
                            list.AddLast(new TextIcon() { Key = sb.ToString() });
                            current = text[++index];
                        }
                        else if (current == '\\') {
                            list.AddLast(new TextIcon() { Key = sb.ToString() });
                            current = text[(index = index + 2)];
                        }
                        else if (current == '£') {
                            if (index + 1 == text.Length) {
                                list.AddLast(new TextIcon() { Key = sb.ToString() });
                                current = text[(index = index + 2)];
                            }
                            else if (text[index + 1] == '£') {
                                current = text[(index = index + 2)];
                                list.AddLast(new TextIcon() { Key = sb.ToString() });
                            }
                            else if (text[index + 1] != '£') {
                                current = text[(index = index + 1)];
                                list.AddLast(new TextIcon() { Key = sb.ToString() });
                            }
                        }
                    }
                    catch (Exception) { }


                }
                else if (current == '§') {

                    try {
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
                else {
                    var key = "|";
                    StringBuilder sb = new StringBuilder();
                    try {
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
            foreach (var item in list) {
                if (item is TextColor color) {
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
            foreach (var a in list) {
                if (a is TextColor textColor) {
                    paragraph.Inlines.Add(new Run() { Text = textColor.Text.Replace("\\n", "\n"), Foreground = new SolidColorBrush(GetKeyColor(textColor.Key)) });
                }
                else if (a is TextIcon textIcon) {
                    var image = GetKeyImage(textIcon.Key);
                    if (image != null) {
                        paragraph.Inlines.Add(new InlineUIContainer(new Image {
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
            try {
                return Properties.Resources.ResourceManager.GetObject(key) as Bitmap;
            }
            catch (Exception) {
                return Properties.Resources.placeholder;
            }
            //if (key == "energy")
            //    return Properties.Resources.energy;
            //else if (key == "minerals")
            //    return Properties.Resources.minerals;
            //else if (key == "food")
            //    return Properties.Resources.food;
            //else if (key == "consumer_goods")
            //    return Properties.Resources.consumer_goods;
            //else if (key == "unity")
            //    return Properties.Resources.unity;
            //else if (key == "influence")
            //    return Properties.Resources.influence;
            //else if (key == "alloys")
            //    return Properties.Resources.alloys;
            //else if (key == "physics_research")
            //    return Properties.Resources.physics_research;
            //else if (key == "society_research")
            //    return Properties.Resources.society_research;
            //else if (key == "engineering_research")
            //    return Properties.Resources.engineering_research;
            //else if (key == "volatile_motes")
            //    return Properties.Resources.volatile_motes;
            //else if (key == "exotic_gases")
            //    return Properties.Resources.exotic_gases;
            //else if (key == "rare_crystals")
            //    return Properties.Resources.rare_crystals;
            //else if (key == "trade_value")
            //    return Properties.Resources.trade_value;
            //else if (key == "time")
            //    return Properties.Resources.time;
            //else if (key == "sr_zro")
            //    return Properties.Resources.sr_zro;
            //else if (key == "sr_living_metal")
            //    return Properties.Resources.sr_living_metal;
            //else if (key == "sr_dark_matter")
            //    return Properties.Resources.sr_dark_matter;
            //else if (key == "nanites")
            //    return Properties.Resources.nanites;
            //else if (key == "minor_artifacts")
            //    return Properties.Resources.minor_artifacts;
            //else if (key == "ship_stats_armor")
            //    return Properties.Resources.ship_stats_armor;
            //else if (key == "ship_stats_build_cost")
            //    return Properties.Resources.ship_stats_build_cost;
            //else if (key == "ship_stats_build_time")
            //    return Properties.Resources.ship_stats_build_time;
            //else if (key == "ship_stats_damage")
            //    return Properties.Resources.ship_stats_damage;
            //else if (key == "ship_stats_evasion")
            //    return Properties.Resources.ship_stats_evasion;
            //else if (key == "ship_stats_hitpoints")
            //    return Properties.Resources.ship_stats_hit_points;
            //else if (key == "ship_stats_maintenance")
            //    return Properties.Resources.ship_stats_maintenance;
            //else if (key == "ship_stats_piracy_supression")
            //    return Properties.Resources.ship_stats_piracy_suppression;
            //else if (key == "ship_stats_power")
            //    return Properties.Resources.ship_stats_power;
            //else if (key == "ship_stats_shield")
            //    return Properties.Resources.ship_stats_shield;
            //else if (key == "ship_stats_special")
            //    return Properties.Resources.ship_stats_special;
            //else if (key == "ship_stats_speed")
            //    return Properties.Resources.ship_stats_speed;
            //else if (key == "diplo_weight")
            //    return Properties.Resources.diplo_weight;
            //else if (key == "job_administrator" || key == "job_primitive_bureaucrat")
            //    return Properties.Resources.job_administrator;
            //else if (key == "job_agri_drone" || key == "job_farmer" || key == "job_peasant" || key == "job_primitive_farmer")
            //    return Properties.Resources.job_agri_drone;
            //else if (key == "job_alloy_drone" || key == "job_fabricator" || key == "job_foundry" || key == "job_odd_factory_drone" || key == "job_odd_factory_worker" || key == "job_underground_trade_worker")
            //    return Properties.Resources.job_alloy_drone;
            //else if (key == "job_bio_trophy")
            //    return Properties.Resources.job_bio_trophy;
            //else if (key == "job_artisan_drone" || key == "job_artisan")
            //    return Properties.Resources.job_artisan_drone;
            //else if (key == "job_banker")
            //    return Properties.Resources.job_banker;
            //else if (key == "job_brain_drone")
            //    return Properties.Resources.job_brain_drone;
            //else if (key == "job_bureaucrat")
            //    return Properties.Resources.job_bureaucrat;
            //else if (key == "job_calculator" || key == "job_primitive_researcher" || key == "job_primitive_researcher_2" || key == "job_researcher")
            //    return Properties.Resources.job_calculator;
            //else if (key == "job_chemist")
            //    return Properties.Resources.job_chemist;
            //else if (key == "job_clerk")
            //    return Properties.Resources.job_clerk;
            //else if (key == "job_colonist")
            //    return Properties.Resources.job_colonist;
            //else if (key == "job_coordinator")
            //    return Properties.Resources.job_coordinator;
            //else if (key == "job_criminal")
            //    return Properties.Resources.job_criminal;
            //else if (key == "job_crystal_miner" || key == "job_crystal_mining_drone")
            //    return Properties.Resources.job_crystal_miner;
            //else if (key == "job_culture_worker")
            //    return Properties.Resources.job_culture_worker;
            //else if (key == "job_enforcer")
            //    return Properties.Resources.job_enforcer;
            //else if (key == "job_entertainer")
            //    return Properties.Resources.job_entertainer;
            //else if (key == "job_evaluator")
            //    return Properties.Resources.job_evaluator;
            //else if (key == "job_event_purge" || key == "job_purge")
            //    return Properties.Resources.job_event_purge;
            //else if (key == "job_executive")
            //    return Properties.Resources.job_executive;
            //else if (key == "job_fe_archivist")
            //    return Properties.Resources.job_fe_archivist;
            //else if (key == "job_fe_hedonist")
            //    return Properties.Resources.job_fe_hedonist;
            //else if (key == "job_fe_xeno_ward")
            //    return Properties.Resources.job_fe_xeno_ward;
            //else if (key == "job_gas_extraction_drone")
            //    return Properties.Resources.job_gas_extraction_drone;
            //else if (key == "job_gas_extractor")
            //    return Properties.Resources.job_gas_extractor;
            //else if (key == "job_healthcare")
            //    return Properties.Resources.job_healthcare;
            //else if (key == "job_herder")
            //    return Properties.Resources.job_herder;
            //else if (key == "job_high_priest" || key == "job_primitive_priest_2" || key == "job_primitive_priest" || key == "job_priest")
            //    return Properties.Resources.job_high_priest;
            //else if (key == "job_hunter" || key == "job_hunter_gatherer" || key == "job_pre_sapient" || key == "job_primitive_warrior")
            //    return Properties.Resources.job_hunter;
            //else if (key == "job_livestock")
            //    return Properties.Resources.job_livestock;
            //else if (key == "job_manager")
            //    return Properties.Resources.job_manager;
            //else if (key == "job_merchant")
            //    return Properties.Resources.job_merchant;
            //else if (key == "job_mining_drone" || key == "job_ratling_scavenger")
            //    return Properties.Resources.job_mining_drone;
            //else if (key == "job_mote_harvester" || key == "job_mote_harvesting_drone")
            //    return Properties.Resources.job_mote_harvester;
            //else if (key == "job_noble" || key == "job_primitive_noble")
            //    return Properties.Resources.job_noble;
            //else if (key == "job_organic_battery")
            //    return Properties.Resources.job_organic_battery;
            //else if (key == "job_patrol_drone")
            //    return Properties.Resources.job_patrol_drone;
            //else if (key == "job_primitive_laborer")
            //    return Properties.Resources.job_primitive_laborer;
            //else if (key == "job_primitive_warrior_2" || key == "job_warrior_drone" || key == "job_soldier")
            //    return Properties.Resources.job_primitive_warrior_2;
            //else if (key == "job_replicator")
            //    return Properties.Resources.job_replicator;
            //else if (key == "job_roboticist")
            //    return Properties.Resources.job_roboticist;
            //else if (key == "job_servant")
            //    return Properties.Resources.job_servant;
            //else if (key == "job_spawning_drone")
            //    return Properties.Resources.job_spawning_drone;
            //else if (key == "job_synapse_drone")
            //    return Properties.Resources.job_synapse_drone;
            //else if (key == "job_technician" || key == "job_technician_drone")
            //    return Properties.Resources.job_technician;
            //else if (key == "job_telepath")
            //    return Properties.Resources.job_telepath;
            //else if (key == "job_translucer" || key == "job_translucer_drone")
            //    return Properties.Resources.job_translucer;

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
