using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Runtime.Remoting.Messaging;

namespace @delegate
{
    class Program
    {
        static TimeSpan Boil()
        {
            DateTime begin = DateTime.Now;
            Console.WriteLine("水壶：开始烧水...");
            Thread.Sleep(6000);
            Console.WriteLine("水壶：水已经烧开。");
            return DateTime.Now - begin;
        }

        delegate TimeSpan BoilDelegate();

        static void BoillingFinishedCallback(IAsyncResult result)
        {
            AsyncResult asyncResult = (AsyncResult)result;
            BoilDelegate d = (BoilDelegate)asyncResult.AsyncDelegate;
            Console.WriteLine("烧水共用去{0}时间。", d.EndInvoke(result));
            Console.WriteLine("小文：将水灌入壶中。");
            Console.WriteLine("小文：继续处理家务...");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("小文：将水壶放到火炉上。");
            BoilDelegate d = new BoilDelegate(Boil);
            IAsyncResult result = d.BeginInvoke(BoillingFinishedCallback, null);
            //d.EndInvoke(result);
            Console.WriteLine("小文：开始整理家务...");
            for (int i = 0; i < 20; i++)
            {
                Console.WriteLine("小文：整理第{0}项家务...", i);
                Thread.Sleep(1000);
            }

            Console.ReadLine();
        }
    }
}
