using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

using MediaInfoNET;
using msearch;

namespace mtest
{
    class Program
    {
        static void Main(string[] args)
        {
            /*
            MSearch ms = new MSearch();
            List<FileInfo> files = ms.SearchFiles(@"E:\work_tempfiles\work_tempfiles_2017\20170206");
            foreach (FileInfo f in files)
            {
                Console.WriteLine(f.FullName);
            }
            Console.ReadLine();
            */

            MediaFile mFile = new MediaFile(@"C:\work_media\是否支持AC3格式测试样品.mkv");
            Console.WriteLine();
            Console.WriteLine("General ---------------------------------");
            Console.WriteLine();
            Console.WriteLine("File Name : {0}", mFile.Name);
            Console.WriteLine("Format : {0}", mFile.General.Format);
            Console.WriteLine("Duration : {0}", mFile.General.DurationString);
            Console.WriteLine("Bitrate : {0}", mFile.General.Bitrate);

            Console.ReadLine();
        }
    }
}
