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
    public class TechnologyTierParser
    {
        public static LinkedList<TechnologyTier> Parse(FileInfo file)
        {
            LinkedList<TechnologyTier> tiers = new LinkedList<TechnologyTier>();
            String text = File.ReadAllText(file.FullName);
            if (String.IsNullOrWhiteSpace(text))
                return tiers;

            PdxLexer lexer = new PdxLexer(text);
            while (lexer.currentHanlde != (char)0)
                tiers.Add(lexer.ParseTechnologyTier());
            return tiers;
        }
    }
}
