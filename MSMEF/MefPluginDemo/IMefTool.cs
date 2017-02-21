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
    public interface IMefTool : IPluginIntf
    {
        void OnClick();
        void OnMapMouseDown(int button, int shift, int x, int y, double mapX, double mapY);
        void OnMapMouseUp(int button, int shift, int x, int y, double mapX, double mapY);
        void OnMapMouseMove(int button, int shift, int x, int y, double mapX, double mapY);
    }
}
