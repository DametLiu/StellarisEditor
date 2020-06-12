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
        public StatementCollection Parse()
        {
            
            StatementCollection stems = new StatementCollection();

            while (true)
            {
                Statement stem = ParseStatement();
                if (stem == null)
                    break;
                stem.Pragma = new LinePragma() { Row = Lexical.Row, Col = Lexical.Col };
                stems.AddLast(stem);
            }
            return stems;

        }

        /// <summary>
        /// 解析基本语句， 变量声明、对象
        /// </summary>
        /// <returns></returns>
        private Statement ParseStatement()
        {
            // 如果是@开头的变量定义
            if (Lexical.Read(Tag.Variable))
            {
                Lexeme variable = Lexical.Curr;
                VariableExpression variableExpression = new VariableExpression() { Key = variable.Content, Pragma = variable.Pragma};
                
                // 尝试读取等号
                if (Lexical.Read(Tag.Equal))
                {
                    // 如果是数值赋值
                    if (Lexical.Read(Tag.Number))
                    {
                        return new VariableStatement()
                        {
                            VariableExpression = variableExpression,
                            AssignExpression = new AssignExpression() { Target = variableExpression, Express = new NumberExpression() { Key = Lexical.Curr.Content } }
                        };
                    }
                    // 如果是变量引用
                    else if (Lexical.Read(Tag.Variable))
                    {
                        return new VariableStatement()
                        {
                            VariableExpression = variableExpression,
                            AssignExpression = new AssignExpression() { Target = variableExpression, Express = new VariableExpression() { Key = Lexical.Curr.Content } }
                        };
                    }
                }
                return new UnknowStatement() { Content = variable.Content, Pragma = variable.Pragma };
            }
            // 如果是对象定义
            else
            {
                ParserContext context = new ParserContext();

                Lexeme l = null;
                Lexeme m = null;
                Lexeme r = null;
                while (true)
                {
                    
                    l = Lexical.Read();
                    if (l.Tag == Tag.Id)
                    {
                        m = Lexical.Read();
                        if (m.Tag == Tag.Equal)
                        {
                            r = Lexical.Read();
                            if (r.Tag == Tag.Variable)
                            {
                                context.CurrentBlock.Statements.AddLast(new AssignmentStatement() { Key = new IdentifierExpression() { Key = l.Content }, Equal = new EqualExpreesion() { Key = m.Content }, Value = new VariableExpression() { Key = r.Content }, Content = $"{l.Content} {m.Content} {r.Content}", Pragma = l.Pragma });
                            }
                            else if (r.Tag == Tag.Number)
                            {
                                context.CurrentBlock.Statements.AddLast(new AssignmentStatement() { Key = new IdentifierExpression() { Key = l.Content }, Equal = new EqualExpreesion() { Key = m.Content }, Value = new NumberExpression() { Key = r.Content }, Content = $"{l.Content} {m.Content} {r.Content}", Pragma = l.Pragma });
                            }
                            else if (r.Tag == Tag.String)
                            {
                                context.CurrentBlock.Statements.AddLast(new AssignmentStatement() { Key = new IdentifierExpression() { Key = l.Content }, Equal = new EqualExpreesion() { Key = m.Content }, Value = new StringExpression() { Key = r.Content }, Content = $"{l.Content} {m.Content} {r.Content}", Pragma = l.Pragma });
                            }
                            else if (r.Tag == Tag.Id)
                            {
                                context.CurrentBlock.Statements.AddLast(new AssignmentStatement() { Key = new IdentifierExpression() { Key = l.Content }, Equal = new EqualExpreesion() { Key = m.Content }, Value = new IdentifierExpression() { Key = r.Content }, Content = $"{l.Content} {m.Content} {r.Content}", Pragma = l.Pragma });
                            }
                            else if (r.Tag == Tag.Boolean)
                            {
                                context.CurrentBlock.Statements.AddLast(new AssignmentStatement() { Key = new IdentifierExpression() { Key = l.Content }, Equal = new EqualExpreesion() { Key = m.Content }, Value = new BooleanExpression() { Key = r.Content }, Content = $"{l.Content} {m.Content} {r.Content}", Pragma = l.Pragma });
                            }
                            else if (r.Tag == Tag.Brace_Left)
                            {
                                context.BeginBlock(new ObjectStatement());
                            }
                        }
                        

                        
                    }
                    else if (l.Tag == Tag.Brace_Right)
                    {
                        context.EndBlock();
                    }



                    if (context.IsEmpty || Lexical.Curr.Tag == Tag.None)
                        break;
                }
            }

            return new UnknowStatement() { Content = Lexical.Curr.Content, Pragma = Lexical.Curr.Pragma };
        }
    }
}
