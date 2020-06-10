﻿using System;
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
        /// 数值
        /// </summary>
        Number,
        /// <summary>
        /// 字符串
        /// </summary>
        String,
        /// <summary>
        /// 已结束
        /// </summary>
        None,
    }

    /// <summary>
    /// <para>词汇</para>
    /// <para>词汇实体包括词汇的标签和词汇字符串</para>
    /// </summary>
    public class ScriptLexeme
    {
        /// <summary>
        /// 表示文本结束
        /// </summary>
        public static readonly ScriptLexeme END = new ScriptLexeme() { Tag = Tag.None, Lexeme = "end" };

        public Tag Tag { get; set; }
        public string Lexeme { get; set; }
    }
}
