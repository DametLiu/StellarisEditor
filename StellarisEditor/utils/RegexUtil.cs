using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.utils
{
    public class RegexUtil
    {
        public static String LOCALIZATION_KEY = "^[a-zA-Z0-9_.]*";
        public static String LOCALIZATION_INDEX = @"(?<=:)\d*";
        public static String LOCALIZATION_VALUE = "(?<=\").*?(?=\")";
        public static String LOCALIZATION_WHITESPACE = @"[\s ]";
        public static String LOCALIZATION_COMMENT = "(?=#).*?(?=\n)";
    }
}
