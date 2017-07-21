using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;
using ESRI.ArcGIS.DataSourcesGDB;
using ESRI.ArcGIS.Geodatabase;
using ESRI.ArcGIS.Geometry;

namespace PointToLine
{
    public partial class Form1 : Form
    {
        private IWorkspace m_ws = null;
        public Form1()
        {
            ESRI.ArcGIS.RuntimeManager.Bind(ESRI.ArcGIS.ProductCode.EngineOrDesktop);
            InitializeComponent();
        }

        private IWorkspace OpenWorkspace()
        {
            IWorkspaceFactory wsf = new AccessWorkspaceFactoryClass();
            return wsf.OpenFromFile(".\\WGS84.mdb", 0);
        }
        private IFeatureClass OpenLayer(IWorkspace ws)
        {
            if (ws != null)
            {
                return (ws as IFeatureWorkspace).OpenFeatureClass("bound");
            } else
            {
                return null;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            m_ws = OpenWorkspace();
            IFeatureClass layer = OpenLayer(m_ws);
            if (layer != null)
            {
                if (!File.Exists(".\\coords.txt"))
                {
                    MessageBox.Show("");
                }

                (m_ws as IWorkspaceEdit).StartEditing(true);
                (m_ws as IWorkspaceEdit).StartEditOperation();

                IFeature feature = layer.CreateFeature();

                IPolyline polyline = new PolylineClass();

                FileStream fs = new FileStream(".\\coords.txt", FileMode.Open, FileAccess.Read);
                StreamReader sr = new StreamReader(fs);
                
                string coordstr = sr.ReadLine();
                while (coordstr != null)
                {
                    string[] coords = coordstr.Split(',');
                    if (coords.Length == 2)
                    {
                        double x, y;
                        if (double.TryParse(coords[1], out x) && (double.TryParse(coords[0], out y)))
                        {
                            IPoint point = new PointClass();
                            point.PutCoords(x, y);

                            (polyline as IPointCollection).AddPoint(point);
                        }
                    }
                    coordstr = sr.ReadLine();
                }
                sr.Close();
                fs.Close();

                feature.Shape = polyline;
                feature.Store();

                (m_ws as IWorkspaceEdit).StopEditOperation();
                (m_ws as IWorkspaceEdit).StopEditing(true);
            }
        }
    }
}
