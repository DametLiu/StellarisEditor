using StellarisEditor.utils;
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
    public class NormalParser :IParser
    {
        /// <summary>
        /// 内置词法分析器
        /// </summary>
        private Lexical Lexical { get; set; }
        
        /// <summary>
        /// 根据指定得词法分析器创建解析器
        /// </summary>
        /// <param name="lex"></param>
        public NormalParser(Lexical lex)
        {
            Lexical = lex;
        }

        /// <summary>
        /// 解析并返回语法定义集合
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        public StatementCollection Parse()
        {
            
            StatementCollection stems = new StatementCollection();

            while (true)
            {
                Statement stem = null;
                try
                {
                    stem = ParseStatement();
                }
                catch (Exception e) { LogUtil.ErrorLog(e); }
                if (stem == null)
                    break;
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
            if (Lexical.Read(Tag.None))
                return null;

            // 如果是@开头的变量定义
            if (Lexical.Read(Tag.Variable))
            {
                Lexeme variable = Lexical.Curr;
                
                // 尝试读取等号
                if (Lexical.Read(Tag.Equal))
                {
                    // 如果是数值赋值
                    if (Lexical.Read(Tag.Number))
                    {
                        VariableStatement v = new VariableStatement()
                        {
                            Key = new VariableExpression() { Content = variable.Content, Pragma = variable.Pragma },
                            Equal = new EqualExpreesion() { Content = "="},
                            Value = new NumberExpression() { Content = Lexical.Curr.Content }
                        };
                        LogUtil.Log(v.ToString());
                        return v;
                    }
                    // 如果是变量引用
                    else if (Lexical.Read(Tag.Variable))
                    {
                        VariableStatement v = new VariableStatement()
                        {
                            Key = new VariableExpression() { Content = variable.Content, Pragma = variable.Pragma },
                            Equal = new EqualExpreesion() { Content = "=" },
                            Value = new VariableExpression() { Content = Lexical.Curr.Content }
                        };
                        LogUtil.Log(v.ToString());
                        return v;
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
                                context.CurrentBlock.Statements.AddLast(new AssignmentStatement() { Key = new IdentifierExpression() { Content = l.Content }, Equal = new EqualExpreesion() { Content = m.Content }, Value = new VariableExpression() { Content = r.Content }, Content = $"{l.Content} {m.Content} {r.Content}", Pragma = l.Pragma });
                            }
                            else if (r.Tag == Tag.Number)
                            {
                                context.CurrentBlock.Statements.AddLast(new AssignmentStatement() { Key = new IdentifierExpression() { Content = l.Content }, Equal = new EqualExpreesion() { Content = m.Content }, Value = new NumberExpression() { Content = r.Content }, Content = $"{l.Content} {m.Content} {r.Content}", Pragma = l.Pragma });
                            }
                            else if (r.Tag == Tag.String)
                            {
                                context.CurrentBlock.Statements.AddLast(new AssignmentStatement() { Key = new IdentifierExpression() { Content = l.Content }, Equal = new EqualExpreesion() { Content = m.Content }, Value = new StringExpression() { Content = r.Content }, Content = $"{l.Content} {m.Content} {r.Content}", Pragma = l.Pragma });
                            }
                            else if (r.Tag == Tag.Id)
                            {
                                context.CurrentBlock.Statements.AddLast(new AssignmentStatement() { Key = new IdentifierExpression() { Content = l.Content }, Equal = new EqualExpreesion() { Content = m.Content }, Value = new IdentifierExpression() { Content = r.Content }, Content = $"{l.Content} {m.Content} {r.Content}", Pragma = l.Pragma });
                            }
                            else if (r.Tag == Tag.Boolean)
                            {
                                context.CurrentBlock.Statements.AddLast(new AssignmentStatement() { Key = new IdentifierExpression() { Content = l.Content }, Equal = new EqualExpreesion() { Content = m.Content }, Value = new BooleanExpression() { Content = r.Content }, Content = $"{l.Content} {m.Content} {r.Content}", Pragma = l.Pragma });
                            }
                            else if (r.Tag == Tag.Brace_Left)
                            {
                                context.BeginBlock(new ObjectStatement() { Statements = new StatementCollection(), Content = $"{l.Content} {m.Content} {r.Content}" });
                            }
                            else
                            {
                                context.CurrentBlock.Statements.AddLast(new UnknowStatement() { Content = Lexical.Curr.Content, Pragma = Lexical.Curr.Pragma });
                            }
                        }
                        // 组装数组
                        else if (m.Tag == Tag.Id || m.Tag == Tag.Number || m.Tag == Tag.String || m.Tag == Tag.Brace_Right)
                        {
                            ArrayStatement array = new ArrayStatement { Elements = new ExpressionCollection() };
                            array.Elements.Add(new IdentifierExpression() { Content = l.Content });
                            while (m.Tag != Tag.Brace_Right)
                            {
                                if (m.Tag == Tag.Id)
                                    array.Elements.Add(new IdentifierExpression() { Content = m.Content });
                                else if (m.Tag == Tag.String)
                                    array.Elements.Add(new StringExpression() { Content = m.Content });
                                else if (m.Tag == Tag.Number)
                                    array.Elements.Add(new NumberExpression() { Content = m.Content });
                                else
                                    array.Elements.Add(new UnknowExpression() { Content = m.Content });

                                m = Lexical.Read();
                            }
                            context.CurrentBlock.Statements.AddLast(array);
                            context.EndBlock();
                        }
                    }
                    // 字符串数组
                    else if (l.Tag == Tag.String)
                    {
                        ArrayStatement array = new ArrayStatement { Elements = new ExpressionCollection() };
                        while (l.Tag != Tag.Brace_Right)
                        {
                            if (l.Tag == Tag.Id)
                                array.Elements.Add(new IdentifierExpression() { Content = l.Content });
                            else if (l.Tag == Tag.String)
                                array.Elements.Add(new StringExpression() { Content = l.Content });
                            else if (l.Tag == Tag.Number)
                                array.Elements.Add(new NumberExpression() { Content = l.Content });
                            else
                                array.Elements.Add(new UnknowExpression() { Content = l.Content });

                            l = Lexical.Read();
                        }
                        context.CurrentBlock.Statements.AddLast(array);
                        context.EndBlock();
                    }
                    // 数值数组
                    else if (l.Tag == Tag.Number)
                    {
                        m = Lexical.Read();
                        if (m.Tag == Tag.Equal)
                        {
                            r = Lexical.Read();
                            if (r.Tag == Tag.Brace_Left)
                            {
                                context.BeginBlock(new ObjectStatement() { Statements = new StatementCollection(), Content = $"{l.Content} {m.Content} {r.Content}" });
                            }
                        }
                        else
                        {
                            ArrayStatement array = new ArrayStatement { Elements = new ExpressionCollection() };
                            while (l.Tag != Tag.Brace_Right)
                            {
                                if (l.Tag == Tag.Id)
                                    array.Elements.Add(new IdentifierExpression() { Content = l.Content });
                                else if (l.Tag == Tag.String)
                                    array.Elements.Add(new StringExpression() { Content = l.Content });
                                else if (l.Tag == Tag.Number)
                                    array.Elements.Add(new NumberExpression() { Content = l.Content });
                                else
                                    array.Elements.Add(new UnknowExpression() { Content = l.Content });

                                l = Lexical.Read();
                            }
                            context.CurrentBlock.Statements.AddLast(array);
                            context.EndBlock();
                        }
                    }
                    else if (l.Tag == Tag.Brace_Right)
                    {
                        if (!context.IsEmpty)
                        {
                            ObjectStatement objectStatement = context.CurrentBlock;
                            context.EndBlock();

                            if (context.IsEmpty)
                                return objectStatement;
                        }
                    }

                    if (context.IsEmpty || Lexical.Curr.Tag == Tag.None)
                        break;
                }
            }

            return null;
        }
    }
}
