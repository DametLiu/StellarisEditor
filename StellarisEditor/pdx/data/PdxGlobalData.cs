using StellarisEditor.extension;
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

        public static LinkedList<PdxVariable> Variables = new LinkedList<PdxVariable>();
        public static LinkedList<PdxLocalization> Localizations = new LinkedList<PdxLocalization>();

        public static void LoadScriptedVariables(TaskCancel cancel)
        {
            LinkedList<VariableFileState> lines = new LinkedList<VariableFileState>();            
            LoadScriptedVariable(lines, Properties.Settings.Default.StellarisPath + STELLARIS_PATH_SCRIPTED_VARIABLES, Variables, cancel);

            ExcuteVariableTask(lines);
        }

        public static void LoadScriptedVariable(LinkedList<VariableFileState> lines, String path, LinkedList<PdxVariable> variables, TaskCancel cancel)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(path);
            FileInfo[] fileInfos = directoryInfo.GetFiles();
            foreach (FileInfo file in fileInfos)
                Array.ForEach(File.ReadAllLines(file.FullName, new UTF8Encoding(false)), (l) => { lines.AddLast(new VariableFileState() { file = file, line = l, variables = variables, cancel = cancel }); });
        }

        public static void ExcuteVariableTask(LinkedList<VariableFileState> lines)
        {
            foreach (var line in lines)
                ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback(ParseVariableLine), line);
        }

        private static void ParseVariableLine(object state)
        {
            VariableFileState variableFileState = state as VariableFileState;
            if (IsTaskCanceled(variableFileState))
                return;
            
            PdxVariable variable = ScriptedVariablesParser.parseVariable(variableFileState.line);
            
            if (variable != null)
            {
                variable.FileName = variableFileState.file.Name.Substring(0, variableFileState.file.Name.LastIndexOf("."));
                lock (variableFileState.variables)
                {
                    if (!variableFileState.variables.Contains(variable))
                        variableFileState.variables.Add(variable);
                }
            }
        }

        private static bool IsTaskCanceled(TaskState variableFileState)
        {
            return variableFileState.cancel is TaskCancel cancel && cancel.Cancel;
        }

        public class VariableFileState : TaskState
        {
            public FileInfo file;
            public String line;
            
            public LinkedList<PdxVariable> variables;
        }

        public class TaskState
        {
            public TaskCancel cancel;
        }

        public static void LoadLocalizations(TaskCancel cancel)
        {
            LinkedList<LocalizationFileState> lines = new LinkedList<LocalizationFileState>();
            LoadLocalization(lines, Properties.Settings.Default.StellarisPath, STELLARIS_PATH_LOCALIZATION_ENGLISH, Localizations, cancel);
            LoadLocalization(lines, Properties.Settings.Default.StellarisPath, STELLARIS_PATH_LOCALIZATION_SIMPLE_CHINESE, Localizations, cancel);

            //ExcuteLocalizationTask(lines);
        }

        public static void ExcuteLocalizationTask(LinkedList<LocalizationFileState> lines)
        {
            foreach (var line in lines)
            {
                if (line.line.StartsWith("l_english") || line.line.StartsWith("l_simp_chinese"))
                    continue;

                ThreadPool.UnsafeQueueUserWorkItem(new WaitCallback(ParseLocalizationLine), line);
            }
        }

        public static void LoadLocalization(LinkedList<LocalizationFileState> lines, string p, string s, LinkedList<PdxLocalization> Localizations, TaskCancel cancel)
        {
            DirectoryInfo directoryInfo = new DirectoryInfo(p + s);
            if (directoryInfo.Exists)
                foreach (FileInfo file in directoryInfo.GetFiles())
                    Array.ForEach(File.ReadAllLines(file.FullName, new UTF8Encoding(true)), (l) => { lines.AddLast(new LocalizationFileState() { file = file, path = p, sub = s, line = l, localizations = Localizations, cancel = cancel }); });
        }


        public class LocalizationFileState : TaskState
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
            LocalizationFileState localizationFileState = state as LocalizationFileState;
            if (IsTaskCanceled(localizationFileState))
                return;

            var pdxLocalization = LocalizationParser.ParseLocalization(localizationFileState.line);
            if (pdxLocalization != null)
            {
                var key = (String)pdxLocalization.GetType().GetProperty("Key").GetValue(pdxLocalization);
                var value = (String)pdxLocalization.GetType().GetProperty("Value").GetValue(pdxLocalization);

                lock (localizationFileState.localizations)
                {
                    var localization = localizationFileState.localizations.Find(l => l.Key.Equals(key));
                    if (localization != null)
                    {
                        if (localizationFileState.sub == STELLARIS_PATH_LOCALIZATION_ENGLISH)
                            localization.ValueEnglish = value;
                        else if (localizationFileState.sub == STELLARIS_PATH_LOCALIZATION_SIMPLE_CHINESE)
                            localization.ValueSimpChinese = value;
                    }
                    else
                    {
                        var fileName = localizationFileState.file.Name.Substring(0, localizationFileState.file.Name.LastIndexOf("."));
                        fileName = fileName.Replace("_l_simp_chinese", "");
                        fileName = fileName.Replace("_l_english", "");
                        fileName = fileName.Replace("l_simp_chinese", "");
                        fileName = fileName.Replace("l_english", "");

                        PdxLocalization newLocalization = new PdxLocalization
                        {
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
