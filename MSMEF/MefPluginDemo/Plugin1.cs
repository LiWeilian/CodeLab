using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Composition;


namespace MefPluginDemo
{
    [Export(typeof(IPluginIntf))]
    public class Plugin1 : IPluginIntf
    {
        string IPluginIntf.Text
        {
            get
            {
                return "Plugin1";
            }
        }

        void IPluginIntf.Do()
        {
            MessageBox.Show(string.Format("I am {0}.", (this as IPluginIntf).Text));
        }
    }
}
