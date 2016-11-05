using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.IO;

namespace StreamDemo
{
    class MyThread
    {
        private Thread t = null;
        ManualResetEvent mre = new ManualResetEvent(false);
        private void Run()
        {
            
        }
        public MyThread()
        {

        }
    }
    class Program
    {
        class AsyncState
        {
            public FileStream FS { get; set; }
            public byte[] Buffer { get; set; }
            public ManualResetEvent EvtHandle { get; set; }
        }
        static int bufferSize = 128;
        static void Main(string[] args)
        {
            string filePath = @".\demo.txt";
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                /*
                byte[] buffer = new byte[bufferSize];

                AsyncState asyncState = new AsyncState();
                asyncState.FS = fs;
                asyncState.Buffer = buffer;
                asyncState.EvtHandle = new ManualResetEvent(false);

                IAsyncResult asyncResult = fs.BeginRead(buffer, 0, bufferSize, 
                    new AsyncCallback(AsyncReadCallback), asyncState);
                asyncState.EvtHandle.WaitOne();
                */
                byte[] buffer2 = new byte[bufferSize];

                AsyncState asyncState2 = new AsyncState();
                asyncState2.FS = fs;
                asyncState2.Buffer = buffer2;
                asyncState2.EvtHandle = new ManualResetEvent(false);

                IAsyncResult asyncResult2 = fs.BeginRead(buffer2, 0, bufferSize,
                    new AsyncCallback(AsyncReadCallback), asyncState2);
                asyncState2.EvtHandle.WaitOne();

                Console.WriteLine("READ COMPLETED!");
                Console.ReadLine();
            }
            /*
            byte[] bytes = new byte[4096];
            fs.Read(bytes, 0, 4096);
            string s = System.Text.Encoding.UTF8.GetString(bytes);
            Console.WriteLine(s);
            */
            /*
            StreamReader sr = new StreamReader(fs);
            while (sr.Peek() >= 0)
            {
                string s = sr.ReadLine();
                Console.WriteLine(s);
            }
            */
            Console.ReadLine();
        }

        public static void AsyncReadCallback(IAsyncResult asyncResult)
        {
            AsyncState asyncState = (AsyncState)asyncResult.AsyncState;
            int readCount = asyncState.FS.EndRead(asyncResult);
            if (readCount > 0)
            {
                byte[] buffer;
                if (readCount == bufferSize)
                {
                    buffer = asyncState.Buffer;
                } else
                {
                    buffer = new byte[readCount];
                    Array.Copy(asyncState.Buffer, 0, buffer, 0, readCount);
                }
                string readContent = System.Text.Encoding.UTF8.GetString(buffer);
                Console.WriteLine(readContent);
                Thread.Sleep(1000);
            }
            if (readCount < bufferSize)
            {
                asyncState.EvtHandle.Set();
            } else
            {
                Array.Clear(asyncState.Buffer, 0, bufferSize);
                asyncState.FS.BeginRead(asyncState.Buffer, 0, bufferSize, 
                    new AsyncCallback(AsyncReadCallback), asyncState);
            }
        }
    }
}
