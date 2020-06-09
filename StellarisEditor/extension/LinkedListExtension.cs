using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.extension
{
    public static class LinkedListExtension
    {
        public static TSource Find<TSource>(this LinkedList<TSource> source, Func<TSource, bool> predicate)
        {
            foreach (var item in source)
                if (predicate(item))
                    return item;
            return default(TSource);
        }

        public static LinkedListNode<TSource> Add<TSource>(this LinkedList<TSource> sources, TSource obj)
        {
            return sources.AddLast(obj);
        }
    }
}
