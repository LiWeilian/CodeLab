using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AnonMethod
{
    class Program
    {
        static bool NameMatches(string name)
        {
            return name.StartsWith("sunny", StringComparison.OrdinalIgnoreCase);
        }

        static void Main(string[] args)
        {
            List<string> names = new List<string>();
            names.Add("Sunny Chen");
            names.Add("Kitty Wang");
            names.Add("Sunny Crystal");

            //List<string> found = names.FindAll(new Predicate<string>(NameMatches));
            List<string> found = names.FindAll(delegate(string name) { return name.StartsWith("sunny", StringComparison.OrdinalIgnoreCase); });

            if (found != null)
            {
                foreach (string f in found)
                {
                    Console.WriteLine(f);
                }
            }

            Console.ReadLine();
        }
    }
}
