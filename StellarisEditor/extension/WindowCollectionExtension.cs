using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace StellarisEditor.extension
{
    public static class WindowCollectionExtension
    {
        public static bool Any(this WindowCollection source, Func<Window, bool> predicate)
        {
            foreach (Window item in source)
                if (predicate(item))
                    return true;
            return false;
        }

        public static bool All(this WindowCollection source, Func<Window, bool> predicate)
        {
            foreach (Window item in source)
                if (!predicate(item))
                    return false;
            return true;
        }
    }
}
