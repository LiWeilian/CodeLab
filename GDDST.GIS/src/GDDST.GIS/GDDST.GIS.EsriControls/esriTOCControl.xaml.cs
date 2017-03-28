using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

using ESRI.ArcGIS.Controls;
using ESRI.ArcGIS.Carto;

using GDDST.GIS.PluginEngine;

namespace GDDST.GIS.EsriControls
{
    /// <summary>
    /// esriTOCControl.xaml 的交互逻辑
    /// </summary>
    public partial class esriTOCControl : UserControl
    {
        private IDsApplication m_app = null;
        public esriTOCControl(IDsApplication hook)
        {
            InitializeComponent();
            m_app = hook;

            InitializeTOCSettings();
            InitializeTOCControlEvents();
        }

        private void InitializeTOCSettings()
        {
            this.tocCtrl.EnableLayerDragDrop = true;
        }

        private void InitializeTOCControlEvents()
        {
            tocCtrl.OnBeginLabelEdit += TocCtrl_OnBeginLabelEdit;
            tocCtrl.OnEndLabelEdit += TocCtrl_OnEndLabelEdit;

            tocCtrl.OnDoubleClick += TocCtrl_OnDoubleClick;
            tocCtrl.OnMouseDown += TocCtrl_OnMouseDown;
            tocCtrl.OnMouseMove += TocCtrl_OnMouseMove;
            tocCtrl.OnMouseUp += TocCtrl_OnMouseUp;
        }

        private void TocCtrl_OnMouseUp(object sender, ITOCControlEvents_OnMouseUpEvent e)
        {
            throw new NotImplementedException();
        }

        private void TocCtrl_OnMouseMove(object sender, ITOCControlEvents_OnMouseMoveEvent e)
        {
            throw new NotImplementedException();
        }

        private void TocCtrl_OnMouseDown(object sender, ITOCControlEvents_OnMouseDownEvent e)
        {
            AxTOCControl tocCtrl = (AxTOCControl)sender;
            esriTOCControlItem itemType = esriTOCControlItem.esriTOCControlItemNone;
            IBasicMap basicMap = null;
            ILayer layer = null;
            object unk = null;
            object data = null;
            tocCtrl.HitTest(e.x, e.y, ref itemType, ref basicMap, ref layer, ref unk, ref data);

            switch (e.button)
            {
                case 1:
                    break;
                case 2:
                    break;
                default:
                    break;
            }
        }

        private void TocCtrl_OnDoubleClick(object sender, ITOCControlEvents_OnDoubleClickEvent e)
        {
            throw new NotImplementedException();
        }

        private void TocCtrl_OnEndLabelEdit(object sender, ITOCControlEvents_OnEndLabelEditEvent e)
        {
            e.canEdit = false;
        }

        private void TocCtrl_OnBeginLabelEdit(object sender, ITOCControlEvents_OnBeginLabelEditEvent e)
        {
            e.canEdit = false;
        }
    }
}
