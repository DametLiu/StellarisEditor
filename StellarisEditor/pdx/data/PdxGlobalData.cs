﻿using StellarisEditor.extension;
using StellarisEditor.pdx.parser;
using StellarisEditor.pdx.scriptobject;
using StellarisEditor.ScriptEngine;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;


namespace StellarisEditor.data
{
    public class PdxGlobalData
    {
        public static String STELLARIS_PATH_SCRIPTED_VARIABLES = @"\common\scripted_variables\";
        public static String STELLARIS_PATH_LOCALIZATION = @"\localisation\";
        public static String STELLARIS_PATH_LOCALIZATION_BRAZ_POR = @"\localisation\braz_por\";
        public static String STELLARIS_PATH_LOCALIZATION_ENGLISH = @"\localisation\english\";
        public static String STELLARIS_PATH_LOCALIZATION_FRENCH = @"\localisation\french\";
        public static String STELLARIS_PATH_LOCALIZATION_GERMAN = @"\localisation\german\";
        public static String STELLARIS_PATH_LOCALIZATION_POLISH = @"\localisation\polish\";
        public static String STELLARIS_PATH_LOCALIZATION_RUSSIAN = @"\localisation\russian\";
        public static String STELLARIS_PATH_LOCALIZATION_SIMPLE_CHINESE = @"\localisation\simp_chinese\";
        public static String STELLARIS_PATH_LOCALIZATION_SPANISH = @"\localisation\spanish\";
        public static String STELLARIS_PATH_TECHNOLOGY_TIER = @"\common\technology\tier\";
        public static String STELLARIS_PATH_TECHNOLOGY_CATEGORY = @"\common\technology\category\";
        public static String STELLARIS_PATH_TECHNOLOGY = @"\common\technology\";

        public static LinkedList<Variable> Variables = new LinkedList<Variable>();
        public static LinkedList<PdxLocalization> Localizations = new LinkedList<PdxLocalization>();
        public static LinkedList<TechnologyTier> TechnologyTiers = new LinkedList<TechnologyTier>();
        public static LinkedList<TechnologyCategory> TechnologyCategories = new LinkedList<TechnologyCategory>();
        public static LinkedList<Technology> Technologies = new LinkedList<Technology>();

        public static void LoadDatas()
        {
            LoadTechnologyCategories(Properties.Settings.Default.StellarisPath, TechnologyCategories);
            LoadTechnologyTiers(Properties.Settings.Default.StellarisPath, TechnologyTiers);
            LoadTechnologies(Properties.Settings.Default.StellarisPath, Technologies, Variables);
        }

        public static void LoadTechnologies(string root, LinkedList<Technology> technologies, LinkedList<Variable> variables)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(root + STELLARIS_PATH_TECHNOLOGY);
            if (!directoryInfo.Exists)
                return;

            FileInfo[] fileInfos = directoryInfo.GetFiles();
            foreach (FileInfo file in fileInfos)
            {
                TechnologyScript technologyScript = new TechnologyParser(file).Parse();

                foreach (var item in technologyScript.Technologies)
                    technologies.Add(item);
                foreach (var item in technologyScript.Variables)
                    variables.Add(item);
                
            }
        }

        public static void LoadTechnologyCategories(string root, LinkedList<TechnologyCategory> categories)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(root + STELLARIS_PATH_TECHNOLOGY_CATEGORY);
            if (!directoryInfo.Exists)
                return;

            FileInfo[] fileInfos = directoryInfo.GetFiles();
            foreach (FileInfo file in fileInfos)
            {
                TechnologyCategoryScript technologyCategoryScript = new TechnologyCategoryParser(file).Parse();

                foreach (var item in technologyCategoryScript.TechnologyCategories)
                    categories.Add(item);
            }
        }

        public static void LoadTechnologyTiers(string root, LinkedList<TechnologyTier> tiers)
        {
            //DirectoryInfo directoryInfo = new DirectoryInfo(root + STELLARIS_PATH_TECHNOLOGY_TIER);
            //if (!directoryInfo.Exists)
            //    return;

            //FileInfo[] fileInfos = directoryInfo.GetFiles();
            //foreach (FileInfo file in fileInfos)
            //{
            //    TechnologyScript statements = new TechnologyParser(file).Parse();
                
            //}
        }
       
        public static void LoadScriptedVariables(TaskCancel cancel)
        {
            LinkedList<VariableState> lines = new LinkedList<VariableState>();
            LoadScriptedVariable(lines, Properties.Settings.Default.StellarisPath + STELLARIS_PATH_SCRIPTED_VARIABLES, Variables, cancel);

            ExcuteVariableTask(lines);
        }

        public static void LoadScriptedVariable(LinkedList<VariableState> lines, String path, LinkedList<Variable> variables, TaskCancel cancel)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            FileInfo[] fileInfos = directoryInfo.GetFiles();
            foreach (FileInfo file in fileInfos)
                Array.ForEach(File.ReadAllLines(file.FullName, new UTF8Encoding(false)), (l) => { lines.AddLast(new VariableState() { file = file, line = l, variables = variables, cancel = cancel }); });
        }

        public static void ExcuteVariableTask(LinkedList<VariableState> lines)
        {
            foreach (var line in lines)
                ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback(ParseVariableLine), line);
        }

        private static void ParseVariableLine(object state)
        {
            VariableState variableFileState = state as VariableState;
            if (IsTaskCanceled(variableFileState))
                return;

            Variable variable = ScriptedVariablesParser.parseVariable(variableFileState.line);

            if (variable != null) {
                variable.FileName = variableFileState.file.Name.Substring(0, variableFileState.file.Name.LastIndexOf("."));
                lock (variableFileState.variables) {
                    if (!variableFileState.variables.Contains(variable))
                        variableFileState.variables.Add(variable);
                }
            }
        }

        private static bool IsTaskCanceled(TaskState variableFileState)
        {
            return variableFileState.cancel is TaskCancel cancel && cancel.Cancel;
        }

        public class VariableState : TaskState
        {
            public FileInfo file;
            public String line;

            public LinkedList<Variable> variables;
        }

        public class TaskState
        {
            public TaskCancel cancel;
        }

        public static void LoadLocalizations(TaskCancel cancel)
        {
            LinkedList<LocalizationState> lines = new LinkedList<LocalizationState>();
            LoadLocalization(lines, Properties.Settings.Default.StellarisPath, STELLARIS_PATH_LOCALIZATION_ENGLISH, Localizations, cancel);
            LoadLocalization(lines, Properties.Settings.Default.StellarisPath, STELLARIS_PATH_LOCALIZATION_SIMPLE_CHINESE, Localizations, cancel);

            ExcuteLocalizationTask(lines);
        }

        public static void ExcuteLocalizationTask(LinkedList<LocalizationState> lines)
        {
            foreach (var line in lines) {
                if (line.line.StartsWith("l_english") || line.line.StartsWith("l_simp_chinese"))
                    continue;

                ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback(ParseLocalizationLine), line);
            }
        }

        public static void LoadLocalization(LinkedList<LocalizationState> lines, string p, string s, LinkedList<PdxLocalization> Localizations, TaskCancel cancel)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(p + s);
            if (directoryInfo.Exists) {
                foreach (FileInfo file in directoryInfo.GetFiles())
                    Array.ForEach(File.ReadAllLines(file.FullName, new UTF8Encoding(true)), (l) => { lines.AddLast(new LocalizationState() { file = file, path = p, sub = s, line = l, localizations = Localizations, cancel = cancel }); });
            }
        }

        public class LocalizationState : TaskState
        {
            public FileInfo file;
            public String sub;
            public String path;
            public String line;
            public LinkedList<PdxLocalization> localizations;
        }
        public class TaskCancel
        {
            public bool Cancel;
        }
        public static void ParseLocalizationLine(object state)
        {
            LocalizationState localizationFileState = state as LocalizationState;
            if (IsTaskCanceled(localizationFileState))
                return;

            var pdxLocalization = LocalizationParser.ParseLocalization(localizationFileState.line);
            if (pdxLocalization != null) {
                var key = (String)pdxLocalization.GetType().GetProperty("Key").GetValue(pdxLocalization);
                var value = (String)pdxLocalization.GetType().GetProperty("Value").GetValue(pdxLocalization);

                lock (localizationFileState.localizations) {
                    var localization = localizationFileState.localizations.Find(l => l.Key.Equals(key));
                    if (localization != null) {
                        if (localizationFileState.sub == STELLARIS_PATH_LOCALIZATION_ENGLISH)
                            localization.ValueEnglish = value;
                        else if (localizationFileState.sub == STELLARIS_PATH_LOCALIZATION_SIMPLE_CHINESE)
                            localization.ValueSimpChinese = value;
                    }
                    else {
                        var fileName = localizationFileState.file.Name.Substring(0, localizationFileState.file.Name.LastIndexOf("."));
                        fileName = fileName.Replace("_l_simp_chinese", "");
                        fileName = fileName.Replace("_l_english", "");
                        fileName = fileName.Replace("l_simp_chinese", "");
                        fileName = fileName.Replace("l_english", "");

                        PdxLocalization newLocalization = new PdxLocalization {
                            Key = key,
                            FileName = fileName
                        };

                        if (localizationFileState.sub == STELLARIS_PATH_LOCALIZATION_ENGLISH)
                            newLocalization.ValueEnglish = value;
                        else if (localizationFileState.sub == STELLARIS_PATH_LOCALIZATION_SIMPLE_CHINESE)
                            newLocalization.ValueSimpChinese = value;

                        localizationFileState.localizations.AddLast(newLocalization);
                    }
                }
            }
        }
    }
}
