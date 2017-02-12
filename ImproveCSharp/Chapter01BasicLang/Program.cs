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
            CallDynamic();
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
    }
}
