using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.ScriptEngine
{
    /// <summary>
    /// 词汇标签，用来区分词汇的具体类别
    /// </summary>
    public enum Tag
    {
        /// <summary>
        /// 变量
        /// </summary>
        Variable,
        /// <summary>
        /// 标识符
        /// </summary>
        Id,
        /// <summary>
        /// 数值
        /// </summary>
        Number,
        /// <summary>
        /// 字符串
        /// </summary>
        String,
        /// <summary>
        /// 运算符
        /// </summary>
        Operator,
        /// <summary>
        /// 已结束
        /// </summary>
        None,
        /// <summary>
        /// 未知的
        /// </summary>
        Unknow,
        /// <summary>
        /// 注释
        /// </summary>
        Comment,
        /// <summary>
        /// 块结构开始
        /// </summary>
        Brace_Left,
        /// <summary>
        /// 块结构结束
        /// </summary>
        Brace_Right,
        /// <summary>
        /// 等号
        /// </summary>
        Equal,
        /// <summary>
        /// 布尔值
        /// </summary>
        Boolean,
    }

    /// <summary>
    /// <para>词汇</para>
    /// <para>词汇实体包括词汇的标签和词汇字符串</para>
    /// </summary>
    public class Lexeme : Node
    {
        /// <summary>
        /// 表示文本结束
        /// </summary>
        public static readonly Lexeme END = new Lexeme() { Tag = Tag.None, Content = "end" };

        /// <summary>
        /// 词汇标签，用来区分词汇的类型
        /// </summary>
        public Tag Tag { get; set; }

        public override string ToString()
        {
            return $"{{Row={Pragma.Row}, Col={Pragma.Col}, Tag={Tag.ToString()}, Content={Content}}}";
        }
    }
}
