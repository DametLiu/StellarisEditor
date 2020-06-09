using StellarisEditor.pdx.scriptobject;
using StellarisEditor.utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;

namespace StellarisEditor.pdx.parser
{
    public class LocalizationParser
    {
        public static Object ParseLocalization(string line)
        {
            line = line.Trim();
            if (String.IsNullOrWhiteSpace(line) || line.StartsWith("#") || !line.Contains("\""))
                return null;

            var key = Regex.Match(line, RegexUtil.LOCALIZATION_KEY).Value;
            var value = Regex.Match(line, RegexUtil.LOCALIZATION_VALUE).Value;

            return new { Key = key, Value = value };
        }
    }
}
