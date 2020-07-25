using StellarisEditor.pdx.scriptobject;
using StellarisEditor.ScriptEngine.collections;
using StellarisEditor.utils;
using System;
using System.Collections.ObjectModel;
using System.IO;

namespace StellarisEditor.ScriptEngine
{
    /// <summary>
    /// 脚本解析器
    /// </summary>
    public class TechnologyParser : Parser<TechnologyScript>
    {
        
        
        /// <summary>
        /// 根据指定得词法分析器创建解析器
        /// </summary>
        /// <param name="lex"></param>
        public TechnologyParser(FileInfo file)
        {
            Lexical = new Lexical(File.ReadAllText(file.FullName));
            FileName = file.Name;
        }

        /// <summary>
        /// 解析并返回语法定义集合
        /// </summary>
        /// <returns></returns>
        public override TechnologyScript Parse()
        {
            TechnologyScript technologyScript = new TechnologyScript();

            while (Lexical.Peek.Tag != Tag.None)
            {
                if (IsVariable(Lexical.Peek))
                    technologyScript.Variables.Add(ParseVariable());
                else if (IsIdentifier(Lexical.Peek))
                    technologyScript.Technologies.Add(ParseTechnology());
                else
                    throw new ArgumentException($"TechnologyParser 无法识别该脚本语句:{FileName} {Lexical.Peek.Pragma.Row} {Lexical.Peek.Pragma.Col} {Lexical.Peek.Content}");
            }

            return technologyScript;

        }

        private Technology ParseTechnology()
        {
            Technology technology = new Technology();
            technology.FileName = FileName;

            // 科技名称
            technology.Key = Lexical.Read().Content;
            Lexical.Read();
            Lexical.Read();

            while (Lexical.Peek.Tag != Tag.Brace_Right)
            {
                Lexeme peek = Lexical.Peek;
                if (peek.Content == "cost")
                    technology.Cost = ParseValue();
                else if (peek.Content == "area")
                    technology.Area = ParseValue();
                else if (peek.Content == "tier")
                    technology.Tier = ParseValue();
                else if (peek.Content == "category")
                    technology.Category = ParseArray();
                else if (peek.Content == "levels")
                    technology.Levels = ParseValue();
                else if (peek.Content == "cost_per_level")
                    technology.CostPerLevel = ParseValue();
                else if (peek.Content == "prerequisites")
                    technology.Prerequisites = ParseArray();
                else if (peek.Content == "weight")
                    technology.Weight = ParseValue();
                else if (peek.Content == "gateway")
                    technology.Gateway = ParseValue();
                else if (peek.Content == "ai_update_type")
                    technology.AiUpdateType = ParseValue();
                else if (peek.Content == "start_tech")
                    technology.StartTech = ParseValue();
                else if (peek.Content == "feature_flags")
                    technology.FeatureFlags = ParseArray();
                else if (peek.Content == "is_rare")
                    technology.IsRare = ParseValue();
                else if (peek.Content == "is_dangerous")
                    technology.IsDangerous = ParseValue();
                else if (peek.Content == "is_reverse_engineerable")
                    technology.IsReverseEngineerable = ParseValue();
                else if (peek.Content == "weight_modifier")
                    technology.WeightModifier = ParseWeightModifier();
                else if (peek.Content == "ai_weight")
                    technology.AiWeight = ParseWeightModifier();
                else if (peek.Content == "modifier")
                    technology.Modifier = ParseTriggers();
                else if (peek.Content == "potential")
                    technology.Potential = ParseTriggers();
                else if (peek.Content == "prereqfor_desc")
                    technology.PrereqforDesc = ParsePrereqforDesc();
                else if (peek.Content == "weight_groups")
                    technology.WeightGroups = ParseArray();
                else if (peek.Content == "mod_weight_if_group_picked")
                    technology.ModWeightIfGroupPicked = ParseModWeightIfGroupPicked();
                else if (peek.Content == "icon")
                    technology.Icon = ParseValue();
                else if (peek.Tag == Tag.None)
                    break;
                else
                    throw new ArgumentException($"TechnologyParser 无法识别该脚本语句:{FileName} {Lexical.Peek.Pragma.Row} {Lexical.Peek.Pragma.Col} {Lexical.Peek.Content}");

            }

            Skip(1);
            return technology;
        }

        private ObservableCollection<WeightGroupPicked> ParseModWeightIfGroupPicked()
        {
            ObservableCollection<WeightGroupPicked> weightGroupPickeds = new ObservableCollection<WeightGroupPicked>();
            Skip(3);

            while(CheckBlock())
            {
                weightGroupPickeds.Add(new WeightGroupPicked() { Group = Lexical.Read().Content, Operator = Lexical.Read().Content, Value = Lexical.Read().Content });
            }

            Skip(1);
            return weightGroupPickeds;
        }

        private PrereqforDesc ParsePrereqforDesc()
        {
            PrereqforDesc prereqforDesc = new PrereqforDesc();
            Skip(3);

            while (CheckBlock())
            {
                if (Lexical.Peek.Content == "hide_prereq_for_desc")
                    prereqforDesc.HidePrereqForDesc = ParseValue();
                else if (Lexical.Peek.Content == "ship" ||
                    Lexical.Peek.Content == "component" ||
                    Lexical.Peek.Content == "diplo_action" ||
                    Lexical.Peek.Content == "feature" ||
                    Lexical.Peek.Content == "resource" ||
                    Lexical.Peek.Content == "custom")
                    prereqforDesc.Cotegories.Add(ParsePrereqforDescCetegory());
            }
            Skip(1);

            return prereqforDesc;
        }

        private PrereqforDescCetegory ParsePrereqforDescCetegory()
        {
            PrereqforDescCetegory prereqforDescCetegory = new PrereqforDescCetegory();
            prereqforDescCetegory.Key = Lexical.Read().Content;
            Skip(2);

            for (int i = 0; i < 2; i++)
            {
                if (Lexical.Peek.Content == "title")
                    prereqforDescCetegory.Title = ParseValue();
                else if (Lexical.Peek.Content == "desc")
                    prereqforDescCetegory.Desc = ParseValue();
            }

            Skip(1);
            return prereqforDescCetegory;
        }

        private ObservableCollection<Modifier> ParseStaticModifier()
        {
            ObservableCollection<Modifier> modifiers = new ObservableCollection<Modifier>();
            Skip(3);

            while (CheckBlock())
            {
                Lexeme p1 = Lexical.Read();
                Lexeme p2 = Lexical.Read();
                Lexeme p3 = Lexical.Read();

                modifiers.Add(new Modifier() { Key = p1.Content, Operator = p2.Content, Value = p3.Content });
            }

            Skip(1);

            return modifiers;
        }

        private WeightModifier ParseWeightModifier()
        {
            WeightModifier weightModifier = new WeightModifier();
            Skip(3);
            
            while (CheckBlock())
            {
                if (Lexical.Peek.Content == "factor")
                    weightModifier.Factor = ParseValue();
                else if (Lexical.Peek.Content == "weight")
                    weightModifier.Weight = ParseValue();
                else if (Lexical.Peek.Content == "modifier")
                    weightModifier.Modifiers.Add(ParseModifier());
            }
            Skip(1);
            return weightModifier;
        }

        private WeightModifier.Modifier ParseModifier()
        {
            WeightModifier.Modifier modifier = new WeightModifier.Modifier();
            Skip(3);
            
            while (CheckBlock())
            {
                if (Lexical.Peek.Content == "factor")
                    modifier.Factor = ParseValue();
                else if (Lexical.Peek.Content == "weight")
                    modifier.Weight = ParseValue();
                else
                    modifier.Triggers.Add(ParseTrigger(null));
            }

            Skip(1);
            return modifier;
        }
    }
    
    public class TechnologyScript
    {
        public VariableCollection Variables { get; set; } = new VariableCollection();
        public TechnologyColletion Technologies { get; set; } = new TechnologyColletion();
    }
}
