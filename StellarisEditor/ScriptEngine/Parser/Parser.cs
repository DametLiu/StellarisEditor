using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StellarisEditor.ScriptEngine
{
    /// <summary>
    /// 脚本解析器
    /// </summary>
    public class Parser
    {
        /// <summary>
        /// 内置词法分析器
        /// </summary>
        private Lexical Lexical { get; set; }

        private ParserContext Context = new ParserContext();

        /// <summary>
        /// 根据指定得词法分析器创建解析器
        /// </summary>
        /// <param name="lex"></param>
        public Parser(Lexical lex)
        {
            Lexical = lex;
        }

        /// <summary>
        /// 解析并返回定义集合
        /// </summary>
        /// <returns></returns>
        public NodeCollection ParseNode()
        {
            NodeCollection nodes = new NodeCollection();
            return nodes;
        }

        public NodeCollection ParseLexeme()
        {
            NodeCollection nodes = new NodeCollection();
            while (true)
            {
                var node = Lexical.Read();
                if (node.Tag != Tag.None)
                {
                    nodes.Add(node);
                }
                else
                    break;
            }
            return nodes;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        private StatementCollection Parse(BlockInfo block)
        {
            Context.BeginBlock(block);
            StatementCollection stems = new StatementCollection();

            while (true)
            {
                ScriptStatement stem = ParseStatement();
                if (stem == null)
                    break;
                stem.LinePragma = new LinePragma() { Row = Lexical.Row, Col = Lexical.Col };
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
            // 解析字段、对象
            if (Lexical.Read(Tag.Id))
            {
                Lexeme id = Lexical.Curr;
                ScriptExpression rootRef = new ScriptVariableExpression() { Name = id.Content };


            }
            // 解析变量引用定义
            else if (Lexical.Read(Tag.Variable)) {
                Lexeme variable = Lexical.Curr;
                ScriptExpression key = new ScriptVariableExpression() { Name = variable.Content };
                ScriptExpression target = ParseVariableReferenceExpression(key);


            }
            else if (Lexical.NextIs(Tag.None) || Lexical.NextIs("}"))
            {
                return null;
            }
            else
            {

            }

            return stem;
        }

        private ScriptExpression ParseVariableReferenceExpression(ScriptExpression key)
        {
            ScriptExpression result = key;
            Lexeme nsym = Lexical.Peek;
            if (nsym.Content == "=")
            {
                
            }
            return null;
        }
    }
}
