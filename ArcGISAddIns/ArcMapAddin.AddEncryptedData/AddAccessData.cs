using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace ArcMapAddin.AddEncryptedData
{
    public class AddAccessData : ESRI.ArcGIS.Desktop.AddIns.Button
    {
        public AddAccessData()
        {
        }

        protected override void OnClick()
        {
            //
            //  TODO: Sample code showing how to access button host
            //
            ArcMap.Application.CurrentTool = null;
            MessageBox.Show("Add Access Data");
        }
        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }
    }

}
