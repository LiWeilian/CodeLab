using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MefPluginDemo
{
    /// <summary>
    /// 
    /// </summary>
    public interface IMefCommand : IPluginIntf
    {
        /// <summary>
        /// 
        /// </summary>
        void OnClick();
    }
}
