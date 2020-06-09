using StellarisEditor.pdx.scriptobject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StellarisEditor.pdx.parser
{
    public class PdxLexer
    {
        public string context;
        public char currentHanlde;
        public int position;

        public PdxLexer(string context)
        {
            this.context = context;
            position = 0;
            currentHanlde = context.ToCharArray()[position];
        }

        public PdxVariable ParseVariable()
        {
            if (currentHanlde == '@')
            {
                Next();

                String name = ParseStringKey();
                SkipWhitespace();

                if (currentHanlde == '=')
                    Next();

                SkipWhitespace();

                String value = ParseStringValue();
                bool isR = value.Contains("@");

                try
                {
                    return new PdxVariable
                    {
                        Key = name,
                        Value = isR ? 0 : value.Contains(".") ? Double.Parse(value) : int.Parse(value),
                        Reference = isR ? new PdxVariable() { Key = value.Substring(1) } : null
                    };
                }
                catch (Exception) { SkipLine(); }
            }

            return null;
        }

        private string ParseNumberValue()
        {
            StringBuilder stringBuilder = new StringBuilder();
            while ((currentHanlde >= '0' && currentHanlde <= '9') || currentHanlde == '.' || currentHanlde == '-')
            {
                stringBuilder.Append(currentHanlde);
                Next();
            }

            return stringBuilder.Length == 0 ? "0" : stringBuilder.ToString();
        }

        /**
         * 
         * 解析带双引号字符串值
         */
        public String ParseStringValueInQuotes()
        {
            if (currentHanlde != '"')
                return null;

            // 跳过前引号
            Next();

            StringBuilder stringBuilder = new StringBuilder();
            while (currentHanlde != '"')
            {
                // 将字符拼接起来
                stringBuilder.Append(currentHanlde);
                Next();
            }

            // 跳过后引号
            Next();
            return stringBuilder.ToString();
        }

        /**
         * 
         * 解析带双引号字符串值
         */
        public String ParseStringValue()
        {
            StringBuilder stringBuilder = new StringBuilder();
            while (currentHanlde != '\n' && currentHanlde != '\r' && currentHanlde != '\t' && currentHanlde != ' ' && currentHanlde != '#')
            {
                // 将字符拼接起来
                stringBuilder.Append(currentHanlde);
                Next();
            }

            return stringBuilder.ToString();
        }

        /**
         * 
         * 解析带 花括号得字符串数组
         */
        public LinkedList<String> ParseStringArrayValue()
        {
            if (currentHanlde != '{')
                return new LinkedList<string>();

            // 跳过前花括号
            Next();

            LinkedList<String> strings = new LinkedList<String>();
            while (currentHanlde != '}')
            {
                SkipWhitespace();
                var str = ParseStringValueInQuotes();
                strings.AddLast(str);
                SkipWhitespace();
            }

            // 跳过后花括号
            Next();
            return strings;
        }

        public string ParseLocalizationKey()
        {
            StringBuilder stringBuilder = new StringBuilder();
            while (currentHanlde != ':')
            {
                stringBuilder.Append(currentHanlde);
                Next();
            }

            return stringBuilder.ToString();
        }

        public string ParseStringKey()
        {
            StringBuilder stringBuilder = new StringBuilder();
            while ((currentHanlde >= 'a' && currentHanlde <= 'z') ||
                (currentHanlde >= 'A' && currentHanlde <= 'Z') ||
                (currentHanlde >= '0' && currentHanlde <= '9') ||
                currentHanlde == '_')
            {
                stringBuilder.Append(currentHanlde);
                Next();
            }

            return stringBuilder.ToString();
        }

        public Object ParseLocalization()
        {
            SkipWhitespace();
            String key = ParseLocalizationKey();
            Next();

            Int32 index = Int32.Parse(ParseNumberValue());
            SkipWhitespace();
            String value = ParseStringValueInQuotes();
            SkipLine();

            return new { Key = key, Index = index, Value = value };
        }

        public void SkipWhitespace()
        {
            for (; ; )
            {
                if (currentHanlde == ' ' ||
                        currentHanlde == '\r' ||
                        currentHanlde == '\n' ||
                        currentHanlde == '\t' ||
                        currentHanlde == '\f' ||
                        currentHanlde == '\b')
                {
                    Next();
                    continue;
                }
                else if (currentHanlde == '#')
                {
                    SkipComment();
                    continue;
                }
                else
                {
                    break;
                }
            }
        }

        public void SkipComment()
        {
            if (currentHanlde == '#')
            {
                for (; ; )
                {
                    Next();
                    if (currentHanlde == '\n')
                    {
                        Next();
                        return;
                    }
                }
            }
        }

        public void SkipLine()
        {
            for (; currentHanlde != 0 ; )
            {
                if (currentHanlde == '\n')
                {
                    Next();
                    return;
                }

                Next();
            }
        }

        /**
         * 跳过等号
         */
        public void SkipEqualSign()
        {
            // 跳过等号前边的空白区域
            SkipWhitespace();
            
            // 跳过等号
            if(currentHanlde == '=') Next();

            // 跳过等号后边的空白区域
            SkipWhitespace();
        }

        public char Next()
        {
            position++;
            if (position < context.Length)
                currentHanlde = context.ToCharArray()[position];
            else
                currentHanlde = (char)0;
            return currentHanlde;
        }
    }
}
