using StellarisEditor.pdx.scriptobject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.pdx.parser
{
    public class ScriptedVariablesParser
    {
        public LinkedList<PdxVariable> ParseScriptedVariable(FileInfo file)
        {
            LinkedList<PdxVariable> result = new LinkedList<PdxVariable>();

            string context = File.ReadAllText(file.FullName);
            if ("".Equals(context) || context == String.Empty)
                return result;

            PdxLexer lexer = new PdxLexer(context);


            for (; ; )
            {
                lexer.SkipWhitespace(); // 跳过注释和空白字符

                char ch = lexer.currentHanlde;
                if (ch == '@')
                {   // 读取变量声明
                    var varialbe = lexer.ParseVariable();
                    if (varialbe != null)
                    {
                        varialbe.FileName = file.Name.Substring(0, file.Name.LastIndexOf("."));
                        result.AddLast(varialbe);
                    }
                }
                else if (ch == (char)0)
                {
                    break;
                }
                else
                {
                    lexer.Next();
                }
            }

            return result;
        }
    }
}
