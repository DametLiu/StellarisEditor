﻿using StellarisEditor.data;
using StellarisEditor.pdx.parser;
using StellarisEditor.pdx.scriptobject;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using static StellarisEditor.data.PdxGlobalData;

namespace StellarisEditor.mod.data
{
    public class ModGlobalData
    {
        public static String MOD_PATH_ROOT = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\Paradox Interactive\Stellaris\mod\";

        public static ObservableCollection<PdxMod> ModProjects = new ObservableCollection<PdxMod>();
        public static LinkedList<PdxLocalization> Localizations = new LinkedList<PdxLocalization>();
        public static LinkedList<Variable> Variables = new LinkedList<Variable>();
        public static LinkedList<TechnologyTier> TechnologyTiers = new LinkedList<TechnologyTier>();
        public static LinkedList<TechnologyCategory> TechnologyCategories = new LinkedList<TechnologyCategory>();
        public static LinkedList<Technology> Technologies = new LinkedList<Technology>();

        public static void LoadDatas()
        {
            LoadTechnologyCategories(Properties.Settings.Default.ModPath, TechnologyCategories);
            LoadTechnologyTiers(Properties.Settings.Default.ModPath, TechnologyTiers);
            LoadTechnologies(Properties.Settings.Default.ModPath, Technologies, Variables);
        }

        public static void LoadScriptedVariables(TaskCancel cancel)
        {
            Variables.Clear();
            LinkedList<VariableState> lines = new LinkedList<VariableState>();

            string dir_path = Properties.Settings.Default.ModPath + STELLARIS_PATH_SCRIPTED_VARIABLES;
            if (Directory.Exists(dir_path)) {
                LoadScriptedVariable(lines, dir_path, Variables, cancel);
                ExcuteVariableTask(lines);
            }
        }

        public static void LoadModProjects()
        {
            CreateExampleModProject();

            // 获取所有得 mod 项目文件夹
            DirectoryInfo[] directoryInfos = new DirectoryInfo(ModGlobalData.MOD_PATH_ROOT).GetDirectories();
            foreach (DirectoryInfo directoryInfo in directoryInfos) {
                if (File.Exists(directoryInfo.Parent.FullName + @"\" + directoryInfo.Name + ".mod")) {
                    // 获取mod配置文件
                    FileInfo fileInfo = new FileInfo(directoryInfo.Parent.FullName + @"\" + directoryInfo.Name + ".mod");
                    ModFileParser modFileParser = new ModFileParser();
                    PdxMod pdxMod = modFileParser.ParseModFile(fileInfo.FullName);
                    ModProjects.Add(pdxMod);
                }

            }
        }

        public static void LoadLocalizations(TaskCancel cancel)
        {
            Localizations.Clear();
            LinkedList<LocalizationState> lines = new LinkedList<LocalizationState>();
            LoadLocalization(lines, Properties.Settings.Default.ModPath, STELLARIS_PATH_LOCALIZATION_ENGLISH, Localizations, cancel);
            LoadLocalization(lines, Properties.Settings.Default.ModPath, STELLARIS_PATH_LOCALIZATION_SIMPLE_CHINESE, Localizations, cancel);
            ExcuteLocalizationTask(lines);
        }

        public static void CreateExampleModProject()
        {
            ModProjects.Insert(0, new PdxMod() {
                Name = "新建Mod",
                Version = "1.0.0",
                SupportedVersion = "2.6.*",
                Path = "C:/Example",
                RemoteFileId = "",
                IsModData = true

            });
            ModProjects.First().Tags.First().IsChecked = true;
        }
    }
}
