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

        private ScriptParserContext Context = new ScriptParserContext();

        /// <summary>
        /// 根据指定得词法分析器创建解析器
        /// </summary>
        /// <param name="lex"></param>
        public ScriptParser(ScriptLexicalAnalyzer lex)
        {
            Lexical = lex;
        }

        /// <summary>
        /// 解析并返回定义集合
        /// </summary>
        /// <returns></returns>
        public ScriptStatementCollection Parse()
        {
            ScriptStatementCollection stems = Parse(new ScriptBlockInfo() { Block = ScriptBlock.Context });
            return stems;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        private ScriptStatementCollection Parse(ScriptBlockInfo block)
        {
            Context.BeginBlock(block);
            ScriptStatementCollection stems = new ScriptStatementCollection();

            while (true)
            {
                ScriptStatement stem = ParseStatement();
                if (stem == null)
                    break;
                stem.LinePragma = new ScriptLinePragma() { Row = Lexical.Row, Col = Lexical.Col };
                stems.AddLast(stem);
            }

            Context.EndBlock();
            return stems;

        }

        /// <summary>
        /// 解析基本语句， 变量声明、对象
        /// </summary>
        /// <returns></returns>
        private ScriptStatement ParseStatement()
        {
            ScriptStatement stem = null;
            if (Lexical.Read(Tag.Id))
            {
                ScriptLexeme id = Lexical.Curr;
                ScriptExpression rootRef = new ScriptVariableReferenceExpression() { Name = id.Lexeme };


            }
            else if (Lexical.Read(Tag.Variable)) { }
            else if (Lexical.NextIs(Tag.None) || Lexical.NextIs("}"))
            {
                return null;
            }
            else
            {
            }

            return stem;
        }
    }
}
