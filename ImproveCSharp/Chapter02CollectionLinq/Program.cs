﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Chapter02CollectionLinq
{
    class Program
    {
        static void ResizeArray()
        {
            int[] iArr = { 1, 2, 3, 4, 5, 6 };
            Stopwatch watch = new Stopwatch();
            watch.Start();
            iArr = (int[])iArr.Resize(10);
            watch.Stop();
            Console.WriteLine(string.Format("Resize Array: {0}", watch.Elapsed));
        }

        static void ResizeList()
        {
            List<int> list = new List<int>(new int[] { 1, 2, 3, 4, 5, 6 });
            Stopwatch watch = new Stopwatch();
            watch.Start();
            list.Add(0);
            list.Add(0);
            list.Add(0);
            list.Add(0);
            watch.Stop();
            Console.WriteLine(string.Format("Resize List: {0}", watch.Elapsed));
        }

        static void CallInitialization()
        {
            Initialization init = new Initialization();
            init.InitializeLinqResult();
        }

        static void CallMultiThreadCollection()
        {
            MultiThreadCollection.Op3();            
        }

        static void CallNoListAsBaseClass()
        {
            NoListAsBaseClass.Method1();
            NoListAsBaseClass.Method2();
        }

        static void CallUseLambdaInQuery()
        {
            UseLambdaInQuery.Method1();
        }

        static void Main(string[] args)
        {
            CallUseLambdaInQuery();
            Console.ReadLine();
        }
    }
}
