using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Chapter02CollectionLinq
{
    class MultiThreadCollection
    {
        static AutoResetEvent autoSet = new AutoResetEvent(false);
        public static void Op()
        {
            List<Person> personList = new List<Person>()
            {
                new Person() { Name = "Rose", Age = 19},
                new Person() { Name = "Steve", Age = 45},
                new Person() { Name = "Jessica", Age = 20}
            };

            Thread t1 = new Thread(() =>
            {
                autoSet.WaitOne();
                foreach (Person p in personList)
                {
                    Console.WriteLine(p.Name);
                    Thread.Sleep(1000);
                }
            });
            t1.Start();

            Thread t2 = new Thread(() =>
            {
                autoSet.Set();
                Thread.Sleep(1000);
                personList.RemoveAt(2);
            });
            t2.Start();
        }
    }
}
