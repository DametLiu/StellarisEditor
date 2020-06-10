using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.ScriptEngine
{
    /// <summary>
    /// <para>脚本词法分析器</para>
    /// <para>本类用来扫描脚本代码，生成代码单元</para>
    /// </summary>
    public class ScriptLexicalAnalyzer
    {
        private readonly CharStream stream;

        /// <summary>
        /// 当前行数
        /// </summary>
        public int Row { get { return stream.Row; } }
        /// <summary>
        /// 当前列数
        /// </summary>
        public int Col { get { return stream.Col; } }

        /// <summary>
        /// 根据指定的脚本文本构造一个词法分析器
        /// </summary>
        /// <param name="scriptText"></param>
        public ScriptLexicalAnalyzer(string scriptText)
        {
            stream = new CharStream(scriptText);
        }

        /// <summary>
        /// 读取一个符号，并判断是否为指定符号类型若不是 则输出异常
        /// </summary>
        /// <param name="lexem"></param>
        /// <returns></returns>
        public ScriptLexeme ReadIs(Tag tag)
        {

            if (_peek.Tag == tag)
            {
                return this.Read();
            }

            string mes = "语法错误";
            switch (tag)
            {
                case Tag.ID:
                    mes = "缺少标识符";
                    break;
            }
            
            return new ScriptLexeme();
        }

        /// <summary>
        /// 对代码进行扫描，并返回一个有效的词汇
        /// </summary>
        public ScriptLexeme Scan()
        {
            char c = stream.Read();

            #region 空白字符和注释
            while (true)
            {
                // 如果是空白字符则跳过字符
                if (c == '\r' || c == '\t' || c == ' ' || c == '\n')
                    c = stream.Read();
                // 如果读到文件末尾则直接返回结束词汇
                else if (c == '\0')
                    return ScriptLexeme.END;
                else
                    break;
            }
            #endregion

            #region 注释
            if (c == '#')
            {
                c = stream.Read();
                StringBuilder sc = new StringBuilder();
                while (c != '\n' && c != '\0')
                {
                    sc.Append(sc);
                    c = stream.Read();
                }
                return new ScriptLexeme() { Tag = Tag.Comment, Lexeme = sc.ToString() };
            }
            #endregion

            #region 运算符
            switch (c)
            {
                case '<':
                    if (stream.NextIs('='))
                        return new ScriptLexeme() { Tag = Tag.Operator, Lexeme = "<=" };
                    return new ScriptLexeme() { Tag = Tag.Operator, Lexeme = "<" };
                case '>':
                    if (stream.NextIs('='))
                        return new ScriptLexeme() { Tag = Tag.Operator, Lexeme = ">=" };
                    return new ScriptLexeme() { Tag = Tag.Operator, Lexeme = ">" };
                case '=':
                    return new ScriptLexeme() { Tag = Tag.Operator, Lexeme = "=" };
            }
            #endregion

            #region 字符串
            if (c == '\"')
            {
                StringBuilder sb = new StringBuilder();

                c = stream.Read();
                while (c != '\"' && c != '\0')
                {
                    sb.Append(c);
                    c = stream.Read();
                }
                return new ScriptLexeme() { Tag = Tag.String, Lexeme = sb.ToString() };
            }
            #endregion

            #region 数字
            if (char.IsDigit(c))
            {
                bool hasDot = false;
                StringBuilder nb = new StringBuilder();
                nb.Append(c);

                while (true)
                {
                    c = stream.Peek();
                    if (char.IsDigit(c))
                    {
                        nb.Append(c);
                        stream.Read();
                    }
                    else if (c == '.' && !hasDot)
                    {
                        hasDot = true;
                        nb.Append(c);
                        stream.Read();
                    }
                    else
                    {
                        break;
                    }

                }
                return new ScriptLexeme() { Tag = Tag.Number, Lexeme = nb.ToString() };
            }
            #endregion

            #region 标识符 由字母和下划线组成
            if (char.IsLetter(c) || c == '_')
            {
                StringBuilder si = new StringBuilder();
                si.Append(c);
                c = stream.Peek();
                while (true)
                {
                    if (char.IsLetterOrDigit(c) || c == '_')
                    {
                        si.Append(c);
                        c = stream.Read();
                    }
                    else
                    {
                        break;
                    }
                    c = stream.Peek();
                }
                return new ScriptLexeme { Lexeme = si.ToString(), Tag = Tag.Id };
            }
            #endregion

            return new ScriptLexeme() { Tag = Tag.Unknow, Lexeme = "" + c };
        }
    }
}