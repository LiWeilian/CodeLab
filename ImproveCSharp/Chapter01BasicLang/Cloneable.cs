using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace Chapter01BasicLang
{
    [Serializable]
    class Employee : ICloneable
    {
        public string IDCode { get; set; }
        public int Age { get; set; }
        public Department Department { get; set; }
        public object Clone()
        {
            //return this.MemberwiseClone();
            using (Stream objectStream = new MemoryStream())
            {
                IFormatter formatter = new BinaryFormatter();
                formatter.Serialize(objectStream, this);
                objectStream.Seek(0, SeekOrigin.Begin);
                return formatter.Deserialize(objectStream) as Employee;
            }
        }
    }

    [Serializable]
    class Department
    {
        public string Name { get; set; }
        public override string ToString()
        {
            return this.Name;
        }
    }
}
