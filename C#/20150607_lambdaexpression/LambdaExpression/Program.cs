using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LambdaExpression
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> names = new List<string>();
            names.Add("Sunny Chen");
            names.Add("Kitty Wang");
            names.Add("Sunny Crystal");

            //List<string> found = names.FindAll(delegate(string name) { return name.StartsWith("sunny", StringComparison.OrdinalIgnoreCase); });
            //List<string> found = names.FindAll((string name) => { return name.StartsWith("sunny", StringComparison.OrdinalIgnoreCase); });
            List<string> found = names.FindAll(name => name.StartsWith("sunny", StringComparison.OrdinalIgnoreCase));
            
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
