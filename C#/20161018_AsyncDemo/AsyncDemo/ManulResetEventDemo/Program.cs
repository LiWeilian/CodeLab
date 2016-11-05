using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ManulResetEventDemo
{
    class MyThread
    {

        Thread t = null;

        ManualResetEvent manualEvent = new ManualResetEvent(true);//为true,一开始就可以执行

        private void Run()

        {

            while (true)

            {

                this.manualEvent.WaitOne();

                Console.WriteLine("这里是  {0}", Thread.CurrentThread.ManagedThreadId);

                Thread.Sleep(5000);

            }

        }

        public void Start()

        {

            this.manualEvent.Set();

        }

        public void Stop()

        {

            this.manualEvent.Reset();

        }

        public MyThread()

        {

            t = new Thread(this.Run);

            t.Start();

        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            MyThread myt = new MyThread();

            while (true)

            {

                Console.WriteLine("输入 stop后台线程挂起 start 开始执行！");

                string str = Console.ReadLine();

                if (str.ToLower().Trim() == "stop")

                {

                    myt.Stop();

                }

                if (str.ToLower().Trim() == "start")

                {

                    myt.Start();

                }

            }
        }
    }
}
