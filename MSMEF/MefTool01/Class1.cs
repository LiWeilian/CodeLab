using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Composition;

using MefPluginDemo;

namespace MefTool01
{
    [Export(typeof(IMefTool))]
    public class Class1 : IMefTool
    {
        public string Text
        {
            get
            {
                return "Tool01";
            }
        }

        public void Do()
        {
            MessageBox.Show(string.Format("{0}.Do", this.Text));
        }

        public void OnClick()
        {
            MessageBox.Show(string.Format("{0}.OnClick", this.Text));
        }

        public void OnMapMouseDown(int button, int shift, int x, int y, double mapX, double mapY)
        {
            throw new NotImplementedException();
        }

        public void OnMapMouseMove(int button, int shift, int x, int y, double mapX, double mapY)
        {
            throw new NotImplementedException();
        }

        public void OnMapMouseUp(int button, int shift, int x, int y, double mapX, double mapY)
        {
            throw new NotImplementedException();
        }
    }
}
