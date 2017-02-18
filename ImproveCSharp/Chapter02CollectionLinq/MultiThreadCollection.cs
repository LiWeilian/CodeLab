using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using System.Collections;

namespace Chapter02CollectionLinq
{
    class MultiThreadCollection
    {
        public static void Op()
        {
            AutoResetEvent autoSet = new AutoResetEvent(false);

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
                Console.WriteLine("删除成功");
            });
            t2.Start();
        }

        public static void Op2()
        {
            AutoResetEvent autoSet = new AutoResetEvent(false);

            ArrayList personList = new ArrayList()
            {
                new Person() { Name = "Rose", Age = 19},
                new Person() { Name = "Steve", Age = 45},
                new Person() { Name = "Jessica", Age = 20}
            };

            Thread t1 = new Thread(() =>
            {
                autoSet.WaitOne();
                lock(personList.SyncRoot)
                {
                    foreach (Person p in personList)
                    {
                        Console.WriteLine(p.Name);
                        Thread.Sleep(1000);
                    }
                }
            });
            t1.Start();

            Thread t2 = new Thread(() =>
            {
                autoSet.Set();
                Thread.Sleep(1000);
                lock(personList.SyncRoot)
                {
                    personList.RemoveAt(2);
                    Console.WriteLine("删除成功");
                }
            });
            t2.Start();
        }

        public static void Op3()
        {
            AutoResetEvent autoSet = new AutoResetEvent(false);

            object syncObj = new object();

            List<Person> personList = new List<Person>()
            {
                new Person() { Name = "Rose", Age = 19},
                new Person() { Name = "Steve", Age = 45},
                new Person() { Name = "Jessica", Age = 20}
            };

            Thread t1 = new Thread(() =>
            {
                autoSet.WaitOne();
                lock(syncObj)
                {
                    foreach (Person p in personList)
                    {
                        Console.WriteLine(p.Name);
                        Thread.Sleep(1000);
                    }
                }
            });
            t1.Start();

            Thread t2 = new Thread(() =>
            {
                autoSet.Set();
                Thread.Sleep(1000);
                lock(syncObj)
                {
                    personList.RemoveAt(2);
                    Console.WriteLine("删除成功");
                }
            });
            t2.Start();
        }
    }
}
