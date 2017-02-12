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
            Employee mike = new Employee() { IDCode = "NB123", Age = 25, Department = new Department() { Name = "Dep1" } };
            Employee rose = mike.Clone() as Employee;
            Console.WriteLine(rose.Department.ToString());
            mike.Department.Name = "Dep2";
            Console.WriteLine(rose.Department.ToString());
            Console.ReadLine();
        }
    }
}
