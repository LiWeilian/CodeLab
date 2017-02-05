using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AttributeDemo
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    class HelpAttribute: Attribute
    {
        public HelpAttribute(string desc)
        {
            this.Description = desc;
        }

        public string Description { get; private set; }
    }
}
