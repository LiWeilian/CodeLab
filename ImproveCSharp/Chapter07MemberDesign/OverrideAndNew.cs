using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter07MemberDesign
{
    public class Shape
    {
        public virtual void MethodVirtual()
        {
            Console.WriteLine("Shape MethodVirtual");
        }

        public void Method()
        {
            Console.WriteLine("Shape Method");
        }
    }

    public class Circle: Shape
    {
        public override void MethodVirtual()
        {
            Console.WriteLine("Circle override MethodVirtual");
        }
    }

    public class Triangle: Shape
    {
        public new void MethodVirtual()
        {
            Console.WriteLine("Triangle new MethodVirtual");
        }

        public new void Method()
        {
            Console.WriteLine("Triangle new Method");
        }
    }
}
