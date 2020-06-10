using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.ScriptEngine
{

    public enum Tag
    {
        Number,
        String,
        /// <summary>
        /// 已结束
        /// </summary>
        None,
    }

    public class ScriptLexeme
    {
        public static string END = "end";

        public Tag Tag { get; set; }
        public string Lexeme { get; set; }
    }
}
