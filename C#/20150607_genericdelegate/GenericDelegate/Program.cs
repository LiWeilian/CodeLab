using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GenericDelegate
{
    class Program
    {
        class IntEventArgs : System.EventArgs
        {
            public int IntValue { get; set; }
            public IntEventArgs() { }
            public IntEventArgs(int value)
            {
                IntValue = value;
            }
        }

        class StringEventArgs : System.EventArgs
        {
            public string StringValue { get; set; }
            public StringEventArgs() { }
            public StringEventArgs(string value)
            {
                StringValue = value;
            }
        }

        static void PrintInt(object sender, IntEventArgs e)
        {
            Console.WriteLine(e.IntValue);
        }

        static void PrintString(object sender, StringEventArgs e)
        {
            Console.WriteLine(e.StringValue);
        }

        static void Main(string[] args)
        {
            EventHandler<IntEventArgs> iHandler = new EventHandler<IntEventArgs>(PrintInt);
            EventHandler<StringEventArgs> sHandler = new EventHandler<StringEventArgs>(PrintString);

            iHandler(null, new IntEventArgs(101));
            sHandler(null, new StringEventArgs("Hi, how is your mother?"));

            Console.ReadLine();
        }
    }
}
