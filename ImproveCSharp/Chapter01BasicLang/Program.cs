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
            CallOperatorOverloading();
            Console.ReadLine();
        }

        static void CallCloneable()
        {
            Employee mike = new Employee() { IDCode = "NB123", Age = 25, Department = new Department() { Name = "Dep1" } };
            Employee rose = mike.Clone() as Employee;
            Console.WriteLine(rose.Department.ToString());
            mike.Department.Name = "Dep2";
            Console.WriteLine(rose.Department.ToString());
        }

        static void CallDynamic()
        {
            /*
            object dynamic = new Dynamic();
            var Add = typeof(Dynamic).GetMethod("Add");
            int i = (int)Add.Invoke(dynamic, new object[] { 1, 2});
            */
            dynamic dynamic = new Dynamic();
            int i = dynamic.Add(1, 2);
            Console.WriteLine(i.ToString());
        }

        static void CallOperatorOverloading()
        {
            Vector v1 = new Vector() { X = 1.1, Y = 1.2 };
            Vector v2 = new Vector() { X = 2.1, Y = 2.2 };
            Vector v = v1 + v2;
            Console.WriteLine(string.Format("X:{0}, Y:{1}", v.X, v.Y));
        }
    }
}
