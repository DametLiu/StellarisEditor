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

        public ScriptLexicalAnalyzer(string scriptText)
        {
            stream = new CharStream(scriptText);
        }
    }
}
