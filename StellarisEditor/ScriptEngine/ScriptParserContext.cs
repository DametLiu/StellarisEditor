using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.ScriptEngine
{
    /// <summary>
    /// 解析器上下文
    /// </summary>
    public class ScriptParserContext
    {
        /// <summary>
        /// 内置堆栈
        /// </summary>
        private readonly Stack<ScriptBlockInfo> ContextStack = new Stack<ScriptBlockInfo>();

        /// <summary>
        /// 返回当前栈顶的块
        /// </summary>
        public ScriptBlockInfo CurrentBlock
        {
            get
            {
                return ContextStack.Peek();
            }
        }

        /// <summary>
        /// 通过块类型访问栈中的块，如果没有返回null
        /// </summary>
        /// <param name="block"></param>
        /// <returns></returns>
        public ScriptBlockInfo this[ScriptBlock block]
        {
            get
            {
                return GetBlockInfoBy(block);
            }
        }

        /// <summary>
        /// 返回栈中指定块类型的块，如果没有返回null
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        private ScriptBlockInfo GetBlockInfoBy(ScriptBlock area)
        {
            foreach (var a in ContextStack.ToArray())
                if (a.Block == area)
                    return a;
            return null;
        }

        /// <summary>
        /// 返回当前的块是否是指定的类型
        /// </summary>
        /// <param name="area"></param>
        /// <returns></returns>
        public bool BlockIs(ScriptBlock area)
        {
            return GetBlockInfoBy(area) != null;
        }

        /// <summary>
        /// 开始解析指定的块
        /// </summary>
        /// <param name="block"></param>
        public void BeginBlock(ScriptBlock block)
        {
            ContextStack.Push(new ScriptBlockInfo() { Block = block });
        }

        /// <summary>
        /// 开始解析指定的块
        /// </summary>
        /// <param name="info"></param>
        public void BeginBlock(ScriptBlockInfo info)
        {
            ContextStack.Push(info);
        }

        /// <summary>
        /// 结束解析块
        /// </summary>
        public void EndBlock()
        {
            ContextStack.Pop();
        }
    }
}
