using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chapter02CollectionLinq
{
    public static class ClassForExtensions
    {
        public static Array Resize(this Array array, int newSize)
        {
            Type t = array.GetType().GetElementType();
            Array newArray = Array.CreateInstance(t, newSize);
            Array.Copy(array, 0, newArray, 0, Math.Min(array.Length, newSize));
            return newArray;
        }
    }
}
