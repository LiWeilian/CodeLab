using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter05Exception
{
    class Program
    {
        static void CallInsteadOfErrorCode()
        {
            InsteadOfErrorCode iec = new InsteadOfErrorCode();
            iec.Method();
        }
        static void Main(string[] args)
        {
            CallInsteadOfErrorCode();

            Console.ReadLine();
        }
    }
}
