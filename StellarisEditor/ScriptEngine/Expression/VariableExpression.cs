﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.ScriptEngine
{
    /// <summary>
    /// 脚本变量定义
    /// </summary>
    public class VariableExpression : Expression
    {
        /// <summary>
        /// 变量名称
        /// </summary>
        public string Key { get; set; }

        public override string ToString()
        {
            return $"@{Key}";
        }
    }
}
