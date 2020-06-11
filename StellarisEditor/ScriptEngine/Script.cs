using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.ScriptEngine
{
    public class Script
    {
        /// <summary>
        /// 解析器配置
        /// </summary>
        ParserConfig Config { get; set; }
        /// <summary>
        /// 内置解析器
        /// </summary>
        Parser Parser { get; set; }
        /// <summary>
        /// 脚本节点集合
        /// </summary>
        public NodeCollection Nodes { get; set; }
        /// <summary>
        /// 脚本正文
        /// </summary>
        String Text { get; set; }

        public Script(ParserConfig config, String text)
        {
            Parser = new Parser(new Lexical(text));
            Text = text;
        }

        public void Parse()
        {
            Nodes = Parser.ParseLexeme();
        }
    }
}
