using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.ScriptEngine
{
    /// <summary>
    /// 解析配置
    /// </summary>
    public class ParserConfig
    {
        public Token[] Tokens { get; set; } = new Token[] {
            new Token( "WHITESPACE", @"\s+", true ),
            new Token( "COMMENT", @"#.*[\n\r]", true ),
            new Token( "EQUAL", "=" ),
            new Token( "LESS", "<" ),
            new Token( "MORE", ">" ),
            new Token( "EQUAL_LESS", "<=" ),
            new Token( "EQUAL_MORE", ">=" ),
            new Token( "BRACE_BEGIN", "{" ),
            new Token( "BRACE_END", "}" ),
            new Token( "NUMBER", @"[-+]?[0-9]*.?[0-9]+" ),
            new Token( "STRING", @""".*""" ),
            new Token( "IDENTIFIER", @"@?[a-zA-Z0-0_]*" )
            
        };
    }

    public class Token
    {
        public string Name { get; set; }
        public string Regex { get; set; }
        public bool Ignore { get; set; }

        public Token(string name, string regex, bool ignore = false)
        {
            Name = name;
            Regex = regex;
            Ignore = ignore;
        }
    }
}
