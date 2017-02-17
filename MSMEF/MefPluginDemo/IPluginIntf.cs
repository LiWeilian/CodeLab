using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MefPluginDemo
{
    public interface IPluginIntf
    {
        string Text { get; }
        void Do();
    }
}
