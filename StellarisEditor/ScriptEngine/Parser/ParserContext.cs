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
        private readonly Stack<ObjectStatement> ContextStack = new Stack<ObjectStatement>();

        /// <summary>
        /// 返回当前栈顶的块
        /// </summary>
        public ObjectStatement CurrentBlock
        {
            get
            {
                return ContextStack.Peek();
            }
        }

        public bool IsEmpty => ContextStack.Count == 0;

        /// <summary>
        /// 开始解析指定的块
        /// </summary>
        /// <param name="block"></param>
        public void BeginBlock(ObjectStatement statement)
        {
            ContextStack.Push(statement);
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
