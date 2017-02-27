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

        static void CallInnerException()
        {
            InnerException ie = new InnerException();
            try
            {
                ie.Method();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(string.Format("Inner Exception: {0}", e.InnerException.Message));
            }
        }
        static void Main(string[] args)
        {
            //CallInsteadOfErrorCode();
            CallInnerException();

            Console.ReadLine();
        }
    }
}
