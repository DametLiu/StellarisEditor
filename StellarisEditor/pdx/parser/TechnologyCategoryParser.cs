using StellarisEditor.extension;
using StellarisEditor.pdx.scriptobject;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.pdx.parser
{
    public class TechnologyCategoryParser
    {
        public static LinkedList<PdxTechnologyCategory> Parse(FileInfo file)
        {
            LinkedList<PdxTechnologyCategory> tiers = new LinkedList<PdxTechnologyCategory>();
            String text = File.ReadAllText(file.FullName);
            if (String.IsNullOrWhiteSpace(text))
                return tiers;

            PdxLexer lexer = new PdxLexer(text);
            while (lexer.currentHanlde != (char)0)
                tiers.Add(lexer.ParseTechnologyCategory());
            return tiers;
        }
    }
}
