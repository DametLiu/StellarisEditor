using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StellarisEditor.utils
{
    public class FileUtil
    {
        public interface IFileFindNotify
        {
            void Notify(String filePath);
        }

        public static FileInfo FindDirectoryInAllDrives(String fileName, IFileFindNotify fileFindNotify)
        {
            DriveInfo[] driveInfos = DriveInfo.GetDrives();
            foreach (DriveInfo driveInfo in driveInfos)
            {
                DirectoryInfo rootDirectory = driveInfo.RootDirectory;
                var d = FindFileInDirectory(rootDirectory, fileName, fileFindNotify);
                if (d != null)
                    return d;
            }

            return null;
        }

        public static FileInfo FindFileInDirectory(DirectoryInfo directory, String fileName, IFileFindNotify fileFindNotify)
        {
            FileInfo[] fileInfos = directory.GetFiles();
            foreach (FileInfo fileInfo in fileInfos)
            {
                try
                {
                    if (fileFindNotify != null)
                        fileFindNotify.Notify(fileInfo.FullName);
                    if (fileInfo.Name.Equals(fileName))
                        return fileInfo;
                }
                catch (Exception)
                {
                    continue;
                }
            }

            DirectoryInfo[] directoryInfos = directory.GetDirectories();
            foreach (DirectoryInfo directoryInfo in directoryInfos)
            {
                try
                {
                    var result = FindFileInDirectory(directoryInfo, fileName, fileFindNotify);
                    if (result != null)
                        return result;
                }
                catch (Exception)
                {
                    continue;
                }
            }

            return null;
        }
    }
}
