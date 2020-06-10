using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.ScriptEngine
{
    /// <summary>
    /// 字符流
    /// </summary>
    public class CharStream
    {
        /// <summary>
        /// 源字符串
        /// </summary>
        private StringReader SourceString;

        /// <summary>
        /// 字符串结束符
        /// </summary>
        private readonly char end = '\0';

        /// <summary>
        /// 当前行数
        /// </summary>
        public int Row { get; set; } = 1;
        /// <summary>
        /// 当前列数
        /// </summary>
        public int Col { get; set; } = 1;

        /// <summary>
        /// 使用指定的字符串构造一个字符流
        /// </summary>
        /// <param name="source">源字符串</param>
        public CharStream(string source)
        {
            SourceString = new StringReader(source);
        }

        /// <summary>
        /// 预览下一个字符，但并不读取. 如果读到流的末尾则返回-1
        /// </summary>
        /// <returns></returns>
        public char Peek()
        {
            int i = SourceString.Peek();
            return i > -1 ? (char)i : '\0';
        }

        /// <summary>
        /// 读取一个字符，如果读到流的末尾则返回-1
        /// </summary>
        /// <returns></returns>
        public char Read()
        {
            int i = SourceString.Read();
            char c = i > -1 ? (char)i : '\0';
            Col++;
            if (c == '\n')
            {
                Row++;
                Col = 0;
            }
            return c;
        }

        /// <summary>
        /// 如果下一个字符是指定字符，读取并返回真，否则不读取并返回假
        /// </summary>
        /// <param name="c"></param>
        /// <returns></returns>
        public bool NextIs(char c)
        {
            char n = Peek();
            if (n == c)
            {
                Read();
                return true;
            }
            return false;
        }
    }
}
