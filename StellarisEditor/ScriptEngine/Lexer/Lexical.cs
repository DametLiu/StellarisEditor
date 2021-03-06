﻿using StellarisEditor.utils;
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
    public class Lexical
    {
        private readonly CharStream stream;

        /// <summary>
        /// 预览词汇
        /// </summary>
        public Lexeme Peek { get; private set; }
        /// <summary>
        /// 当前词汇
        /// </summary>
        public Lexeme Curr { get; private set; }

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
        public Lexical(string scriptText)
        {
            stream = new CharStream(scriptText);
            Peek = Scan();
        }

        /// <summary>
        /// 读取一个词汇，并更新预览词汇
        /// </summary>
        /// <returns></returns>
        public Lexeme Read()
        {
            Curr = Peek;
            Peek = Scan();
            return Curr;
        }

        /// <summary>
        /// 返回是否能读取指定词汇，如果可以则读取词汇并返回true，否则不读取返回false
        /// </summary>
        /// <param name="lexem"></param>
        /// <returns></returns>
        public bool Read(string lexem)
        {
            if (Peek.Content == lexem)
            {
                Read();
                return true;
            }
            return false;
        }

        /// <summary>
        /// 返回是否能读取指定标签的词汇，如果可以则读取词汇并返回true，否则不读取返回false
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public bool Read(Tag tag)
        {
            if (Peek.Tag == tag)
            {
                Read();
                return true;
            }
            return false;

        }

        /// <summary>
        /// 判断下一个词汇是否是指定的词汇
        /// </summary>
        /// <param name="lexem"></param>
        /// <returns></returns>
        public bool NextIs(string lexem)
        {
            if (Peek.Content == lexem)
                return true;
            return false;
        }

        /// <summary>
        /// 判断下一个词汇是否具有指定的标签
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public bool NextIs(Tag tag)
        {
            if (Peek.Tag == tag)
                return true;
            return false;
        }

        /// <summary>
        /// 读取并返回指定的词汇，否则返回null
        /// </summary>
        /// <param name="lexem"></param>
        /// <returns></returns>
        public Lexeme ReadIs(string lexem)
        {
            if (Peek.Content == lexem)
                return Read();
            return null;
        }

        /// <summary>
        /// 读取并返回指定标签的词汇，否则返回null
        /// </summary>
        /// <param name="tag"></param>
        /// <returns></returns>
        public Lexeme ReadIs(Tag tag)
        {
            if (Peek.Tag == tag)
                return Read();
            return null;
        }

        /// <summary>
        /// 对代码进行扫描，并返回一个有效的词汇
        /// </summary>
        public Lexeme Scan()
        {
            char c = stream.Read();

            #region 空白字符
            while (true)
            {
                // 如果是空白字符则跳过字符
                if (c == '\r' || c == '\t' || c == ' ' || c == '\n')
                    c = stream.Read();
                // 跳过注释
                else if (c == '#')
                {
                    c = stream.Read();
                    while (c != '\n' && c != '\0')
                        c = stream.Read();
                }
                // 如果读到文件末尾则直接返回结束词汇
                else if (c == '\0')
                    return new Lexeme() { Tag = Tag.None, Content = "end", Pragma = new LinePragma() { Row = Row, Col = Col } };
                else
                    break;
            }
            #endregion

            #region 块
            if (c == '{')
            {
                return new Lexeme() { Tag = Tag.Brace_Left, Content = c.ToString(), Pragma = new LinePragma() { Row = Row, Col = Col } };
            }
            if (c == '}')
            {
                return new Lexeme() { Tag = Tag.Brace_Right, Content = c.ToString(), Pragma = new LinePragma() { Row = Row, Col = Col } };
            }
            #endregion

            #region 注释
            //if (c == '#')
            //{
            //    StringBuilder sc = new StringBuilder();
            //    sc.Append(c);
            //    while (c != '\n' && c != '\r' && c != '\0')
            //    {
            //        sc.Append(c);
            //        c = stream.Read();
            //    }
            //    return new Lexeme() { Tag = Tag.Comment, Content = sc.ToString(), Pragma = new LinePragma() { Row = Row, Col = Col } };
            //}
            #endregion

            #region 变量
            if (c == '@')
            {
                StringBuilder sv = new StringBuilder();
                while (c != ' ' && c != '\n' && c != '\r' && c != '\t' && c != '=' && c != '\0')
                {
                    sv.Append(c);
                    c = stream.Read();
                }
                return new Lexeme() { Tag = Tag.Variable, Content = sv.ToString(), Pragma = new LinePragma() { Row = Row, Col = Col } };
            }
            #endregion

            #region 运算符
            switch (c)
            {
                case '<':
                    if (stream.NextIs('='))
                        return new Lexeme() { Tag = Tag.LessEqual, Content = "<=" };
                    return new Lexeme() { Tag = Tag.Less, Content = "<", Pragma = new LinePragma() { Row = Row, Col = Col } };
                case '>':
                    if (stream.NextIs('='))
                        return new Lexeme() { Tag = Tag.GreaterEqual, Content = ">=" };
                    return new Lexeme() { Tag = Tag.Greater, Content = ">", Pragma = new LinePragma() { Row = Row, Col = Col } };
                case '=':
                    return new Lexeme() { Tag = Tag.Equal, Content = "=", Pragma = new LinePragma() { Row = Row, Col = Col } };
            }
            #endregion

            #region 字符串
            if (c == '\"')
            {
                StringBuilder sb = new StringBuilder();
                sb.Append('"');
                c = stream.Read();
                while (c != '\"' && c != '\0')
                {
                    sb.Append(c);
                    c = stream.Read();
                }
                sb.Append('"');
                return new Lexeme() { Tag = Tag.String, Content = sb.ToString(), Pragma = new LinePragma() { Row = Row, Col = Col } };
            }
            #endregion

            #region 数字
            if (((c == '-' || c == '+') && char.IsDigit(stream.Peek())) || char.IsDigit(c))
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
                return new Lexeme() { Tag = Tag.Number, Content = nb.ToString(), Pragma = new LinePragma() { Row = Row, Col = Col } };
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

                if ("yes" == si.ToString().ToLower())
                    return new Lexeme { Content = si.ToString(), Tag = Tag.Boolean, Pragma = new LinePragma() { Row = Row, Col = Col } };
                else if ("no" == si.ToString().ToLower())
                    return new Lexeme { Content = si.ToString(), Tag = Tag.Boolean, Pragma = new LinePragma() { Row = Row, Col = Col } };
                return new Lexeme { Content = si.ToString(), Tag = Tag.Id, Pragma = new LinePragma() { Row = Row, Col = Col } };
            }
            #endregion

            return new Lexeme() { Tag = Tag.Unknow, Content = c.ToString(), Pragma = new LinePragma() { Row = Row, Col = Col } };
        }
    }
}