using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace msearch
{
    public class MSearch
    {
        public List<FileInfo> SearchFiles(string libPath)
        {
            List<FileInfo> files = new List<FileInfo>();
            if (!Directory.Exists(libPath))
            {
                return null;
            }

            DirectoryInfo dirInfo = new DirectoryInfo(libPath);
            FileInfo[] fileInfos = dirInfo.GetFiles();
            foreach (FileInfo f in fileInfos)
            {
                files.Add(f);
            }

            DirectoryInfo[] dirInfos = dirInfo.GetDirectories();
            foreach (DirectoryInfo d in dirInfos)
            {
                List<FileInfo> fs = SearchFiles(d.FullName);

                foreach (FileInfo f in fs)
                {
                    files.Add(f);
                }
            }

            return files;
        }
    }
}
