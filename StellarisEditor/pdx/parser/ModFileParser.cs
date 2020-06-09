using StellarisEditor.pdx.scriptobject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StellarisEditor.pdx.parser
{
    /**
     * mod 文件解析器
     */
    public class ModFileParser
    {
        public PdxMod ParseModFile(string filePath)
        {
            PdxMod result = new PdxMod();

            // 如果文件是空得，直接返回空对象
            string context = File.ReadAllText(filePath);
            if ("".Equals(context) || context == String.Empty)
                return null;

            PdxLexer lexer = new PdxLexer(context);

            for (; ; )
            {
                lexer.SkipWhitespace(); // 跳过注释和空白字符

                char ch = lexer.currentHanlde;
                if (ch == 'n' || ch == 'p' || ch == 'v' || ch == 's' || ch == 't' || ch == 'r')
                {
                    var key = lexer.ParseStringKey();
                    switch (key)
                    {
                        case "name":
                            lexer.SkipEqualSign();
                            result.Name = lexer.ParseStringValueInQuotes();
                            break;
                        case "picture":
                            lexer.SkipEqualSign();
                            result.Picture = lexer.ParseStringValueInQuotes();
                            break;
                        case "version":
                            lexer.SkipEqualSign();
                            result.Version = lexer.ParseStringValueInQuotes();
                            break;
                        case "supported_version":
                            lexer.SkipEqualSign();
                            result.SupportedVersion = lexer.ParseStringValueInQuotes();
                            break;
                        case "path":
                            lexer.SkipEqualSign();
                            result.Path = lexer.ParseStringValueInQuotes();
                            break;
                        case "tags":
                            lexer.SkipEqualSign();
                            LinkedList<String> tags = lexer.ParseStringArrayValue();
                            foreach (PdxModTag t in result.Tags)
                                foreach (String tag in tags)
                                    if (t.Loclization_English.Equals(tag))
                                        t.IsChecked = true;
                            break;
                        case "remote_file_id":
                            lexer.SkipEqualSign();
                            result.RemoteFileId = lexer.ParseStringValueInQuotes();
                            break;
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
