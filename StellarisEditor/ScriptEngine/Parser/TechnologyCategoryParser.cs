using StellarisEditor.pdx.scriptobject;
using StellarisEditor.ScriptEngine.collections;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.ScriptEngine
{
    public class TechnologyCategoryParser : Parser
    {
        /// <summary>
        /// 根据指定得词法分析器创建解析器
        /// </summary>
        /// <param name="lex"></param>
        public TechnologyCategoryParser(FileInfo file)
        {
            Lexical = new Lexical(File.ReadAllText(file.FullName));
            FileName = file.Name;
        }

        /// <summary>
        /// 解析并返回语法定义集合
        /// </summary>
        /// <returns></returns>
        public TechnologyCategoryScript Parse()
        {
            TechnologyCategoryScript technologyCategoryScript = new TechnologyCategoryScript();

            while (Lexical.Peek.Tag != Tag.None)
            {
                if (IsIdentifier(Lexical.Peek))
                    technologyCategoryScript.TechnologyCategories.Add(ParseTechnologyCategory());
                else
                    throw new ArgumentException($"TechnologyParser 无法识别该脚本语句:{FileName} {Lexical.Peek.Pragma.Row} {Lexical.Peek.Pragma.Col} {Lexical.Peek.Content}");
            }

            return technologyCategoryScript;

        }

        private TechnologyCategory ParseTechnologyCategory()
        {
            TechnologyCategory technologyCategory = new TechnologyCategory();
            technologyCategory.FileName = FileName;

            technologyCategory.Key = Lexical.Read().Content;Skip(2);
            technologyCategory.Icon = ParseValue();
            Skip(1);

            return technologyCategory;
        }
    }

    public class TechnologyCategoryScript
    {
        public TechnologyCategoryCollection TechnologyCategories { get; set; } = new TechnologyCategoryCollection();
    }

}
