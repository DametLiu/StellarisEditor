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
        /// 内置解析器
        /// </summary>
        Parser Parser { get; set; }
        /// <summary>
        /// 脚本节点集合
        /// </summary>
        public StatementCollection Nodes { get; set; }
        /// <summary>
        /// 脚本正文
        /// </summary>
        String Text { get; set; }

        public Script(String text)
        {
            Parser = new Parser(new Lexical(text));
            Text = text;
        }

        public void Parse()
        {
            Nodes = Parser.Parse();
        }
    }
}
