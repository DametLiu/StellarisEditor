﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.ScriptEngine
{
    /// <summary>
    /// 等号表达式
    /// </summary>
    public class EqualExpreesion : Expression
    {
        public override string ToString()
        {
            return Content;
        }
    }
}
