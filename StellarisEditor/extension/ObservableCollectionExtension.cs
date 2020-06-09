using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.extension
{
    public static class ObservableCollectionExtension
    {
        public static TSource Find<TSource>(this ObservableCollection<TSource> source, Func<TSource, bool> predicate)
        {
            foreach (var item in source)
                if (predicate(item))
                    return item;
            return default(TSource);
        }
    }
}
