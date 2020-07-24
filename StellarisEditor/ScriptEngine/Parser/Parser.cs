using StellarisEditor.pdx.scriptobject;
using StellarisEditor.utils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.ScriptEngine
{
    public abstract class Parser<T>
    {
        /// <summary>
        /// 内置词法分析器
        /// </summary>
        protected Lexical Lexical { get; set; }
        protected String FileName { get; set; }

        protected bool IsOperator(Lexeme lexeme) => lexeme.Tag == Tag.Equal || lexeme.Tag == Tag.Less || lexeme.Tag == Tag.LessEqual || lexeme.Tag == Tag.Greater || lexeme.Tag == Tag.GreaterEqual;

        protected bool IsIdentifier(Lexeme lexeme) => lexeme.Tag == Tag.Id;

        protected bool IsVariable(Lexeme lexeme) => lexeme.Tag == Tag.Variable;

        protected bool IsValidMiddle(Lexeme lexeme) => IsOperator(lexeme);

        protected bool IsValidLeft(Lexeme lexeme) => lexeme.Tag == Tag.Id || lexeme.Tag == Tag.Variable;

        protected bool IsValidRight(Lexeme lexeme) => lexeme.Tag == Tag.Number || lexeme.Tag == Tag.Variable || lexeme.Tag == Tag.String || lexeme.Tag == Tag.Id;

        protected bool IsNumber(Lexeme lexeme) => lexeme.Tag == Tag.Number;

        public abstract T Parse();

        protected Variable ParseVariable()
        {
            Lexeme k = Lexical.Read();
            Lexeme e = Lexical.Read();
            Lexeme v = Lexical.Read();

            Variable variable = new Variable();
            variable.Key = k.Content;

            if (IsVariable(v))
                variable.Reference = new Variable() { Key = v.Content };

            if (IsNumber(v))
                variable.Value = v.Content;

            variable.FileName = FileName;
            LogUtil.Log($"{k.Content} {e.Content} {v.Content}");
            return variable;
        }

        protected String ParseValue() => Skip(2).Read().Content;

        protected Lexical Skip(int step)
        {
            for (int i = 0; i < step; i++)
                Lexical.Read();

            return Lexical;
        }

        protected ObservableCollection<String> ParseArray()
        {
            ObservableCollection<String> vs = new ObservableCollection<string>();
            Skip(3);

            while (Lexical.Peek.Tag != Tag.Brace_Right)
                vs.Add(Lexical.Read().Content);

            Skip(1);
            return vs;
        }

        protected ObservableCollection<Expression> ParseTriggers()
        {
            ObservableCollection<Expression> triggers = new ObservableCollection<Expression>();
            Skip(3);

            while (CheckBlock())
            {
                triggers.Add(ParseTrigger(null));
            }
            Skip(1);

            return triggers;
        }

        protected Expression ParseTrigger(Expression parent)
        {
            Expression result = parent ?? new Expression();

            while (CheckBlock())
            {
                Lexeme p1 = Lexical.Read();
                Lexeme p2 = Lexical.Read();
                Lexeme p3 = Lexical.Read();

                if (p3.Tag == Tag.Brace_Left)
                {
                    var c = new Expression() { Key = p1.Content, Operator = p2.Content, Value = p3.Content };
                    result.Children.Add(ParseTrigger(c)); Skip(1);
                }
                else
                    result.Children.Add(new Expression() { Key = p1.Content, Operator = p2.Content, Value = p3.Content });
            }
            return result;
        }

        protected bool CheckBlock()
        {
            return Lexical.Peek.Tag != Tag.Brace_Right && Lexical.Peek.Tag != Tag.None;
        }
    }
}
