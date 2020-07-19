using StellarisEditor.pdx.scriptobject;
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
    public class ScriptedVariablesParser
    {
        public static Variable parseVariable(String line)
        {
            line = Regex.Replace(line, @"[\s ]", "", RegexOptions.Singleline);
            if (String.IsNullOrWhiteSpace(line) || line.StartsWith("#") || !line.Contains("@") || !line.Contains("="))
                return null;

            line = line.Substring(1);
            var key = Regex.Match(line, ".*?(?==)").Value;
            var value = Regex.Match(line, "(?<==).*").Value;

            Variable variable = new Variable() { Key = key };
            try
            {
                
                if (value.Contains("@"))
                    variable.Reference = new Variable() { Key = value.Substring(1) };
                else
                    variable.Value = value;
            }
            catch (Exception) {}

            return variable;
        }
    }
}
