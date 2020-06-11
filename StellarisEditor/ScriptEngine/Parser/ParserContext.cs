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
    public class ParserContext
    {
        /// <summary>
        /// 内置堆栈
        /// </summary>
        private readonly Stack<BlockInfo> ContextStack = new Stack<BlockInfo>();

        /// <summary>
        /// 返回当前栈顶的块
        /// </summary>
        public BlockInfo CurrentBlock
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
        public BlockInfo this[Block block]
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
        private BlockInfo GetBlockInfoBy(Block area)
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
        public bool BlockIs(Block area)
        {
            return GetBlockInfoBy(area) != null;
        }

        /// <summary>
        /// 开始解析指定的块
        /// </summary>
        /// <param name="block"></param>
        public void BeginBlock(Block block)
        {
            ContextStack.Push(new BlockInfo() { Block = block });
        }

        /// <summary>
        /// 开始解析指定的块
        /// </summary>
        /// <param name="info"></param>
        public void BeginBlock(BlockInfo info)
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
