using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter01BasicLang
{
    class Vector
    {
        public double X { get; set; }
        public double Y { get; set; }
        public static Vector operator+(Vector v1, Vector v2)
        {
            return new Vector() { X = v1.X + v2.X, Y = v1.Y + v2.Y };
        }
    }
}
