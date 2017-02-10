using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter01BasicLang
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = "192.168.1.1";
            IP ip = (IP)s;
            Console.WriteLine(ip.ToString());
            IP3 ip3 = (IP3)ip;
            Console.WriteLine(string.Format("{0}: {1}", ip3.Name, ip3.ToString()));
            Console.ReadLine();
        }
    }
}
