using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.ScriptEngine
{
    /// <summary>
    /// 脚本解析器
    /// </summary>
    public class ScriptParser
    {
        /// <summary>
        /// 内置词法分析器
        /// </summary>
        private ScriptLexicalAnalyzer Lexical { get; set; }

        /// <summary>
        /// 根据指定得词法分析器创建解析器
        /// </summary>
        /// <param name="lex"></param>
        public ScriptParser(ScriptLexicalAnalyzer lex)
        {
            Lexical = lex;
        }
    }
}
