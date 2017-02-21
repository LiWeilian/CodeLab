using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Composition;

using MefPluginDemo;

namespace MefCommand01
{
    /// <summary>
    /// 
    /// </summary>
    [Export(typeof(IMefCommand))]
    [Export(typeof(IPluginIntf))]
    public class Cmd01 : IMefCommand
    {
        /// <summary>
        /// 
        /// </summary>
        public string Text
        {
            get
            {
                return "Cmd01";
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void Do()
        {
            MessageBox.Show(string.Format("{0}.Do", this.Text));
        }

        /// <summary>
        /// 
        /// </summary>
        public void OnClick()
        {
            MessageBox.Show(string.Format("{0}.OnClick", this.Text));
        }
    }
}
