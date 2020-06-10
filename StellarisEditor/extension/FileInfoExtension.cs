using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.extension
{
    public static class FileInfoExtension
    {
        public static String SimpleName(this FileInfo source)
        {
            return source.Name.Substring(0, source.Name.LastIndexOf("."));
        }
    }
}
