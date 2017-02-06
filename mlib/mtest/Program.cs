using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using msearch;

namespace mtest
{
    class Program
    {
        static void Main(string[] args)
        {
            MSearch ms = new MSearch();
            List<FileInfo> files = ms.SearchFiles(@"E:\work_tempfiles\work_tempfiles_2017\20170206");
            foreach (FileInfo f in files)
            {
                Console.WriteLine(f.FullName);
            }
            Console.ReadLine();
        }
    }
}
