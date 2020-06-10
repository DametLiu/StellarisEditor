using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.ScriptEngine
{
    /// <summary>
    /// 块类型
    /// </summary>
    public enum ScriptBlock
    {
        /// <summary>
        /// 上下文
        /// </summary>
        Context,
        /// <summary>
        /// 循环
        /// </summary>
        While,
        /// <summary>
        /// 条件语句
        /// </summary>
        Condition,
        /// <summary>
        /// 分支语句
        /// </summary>
        Switch,
    }
}
