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
                // 跳过注释
                else if (c == '#')
                {
                    c = stream.Read();
                    while (c != '\n' && c != '\0')
                        c = stream.Read();
                }
                // 如果读到文件末尾则直接返回结束词汇
                else if (c == '\0')
                    return ScriptLexeme.END;
                else
                    break;
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
                    sb.Append(c);
                return new ScriptLexeme() { Tag = Tag.String, Lexeme = sb.ToString() };
            }
            #endregion


            return null;
        }
    }
}
