using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter07MemberDesign
{
    class Program
    {
        static void CallOverrideAndNew()
        {
            Shape s = new Circle();
            s.MethodVirtual();
            s.Method();

            Circle c = new Circle();
            (c as Shape).MethodVirtual();
            c.Method();

            Shape s2 = new Triangle();
            s2.MethodVirtual();
            s2.Method();
            (s2 as Triangle).MethodVirtual();
            (s2 as Triangle).Method();

            Triangle t = new Triangle();
            t.MethodVirtual();
            t.Method();
            (t as Shape).MethodVirtual();
            (t as Shape).Method();

        }
        static void Main(string[] args)
        {
            CallOverrideAndNew();

            Console.ReadLine();
        }
    }
}
