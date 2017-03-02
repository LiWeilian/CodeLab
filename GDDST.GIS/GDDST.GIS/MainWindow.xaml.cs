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
using System.Windows.Controls.Ribbon;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

using GDDST.GIS.PluginEngine;

namespace GDDST.GIS
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        [Import]
        private IDsApplication m_application;

        #region
        private CompositionContainer m_container;
        [ImportMany]
        private IEnumerable<IDsTool> m_tools;
        [ImportMany]
        private IEnumerable<IDsCommand> m_commands;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            InitializeComponentModel();
            InitializeUI();
        }

        private void InitializeComponentModel()
        {
            AggregateCatalog catalog = new AggregateCatalog();
            string pluginPath = AppDomain.CurrentDomain.BaseDirectory;
            catalog.Catalogs.Add(new DirectoryCatalog(pluginPath));
            m_container = new CompositionContainer(catalog);
            try
            {
                m_container.ComposeParts(this);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                //throw;
            }
        }
        private void InitializeUI()
        {
            Ribbon mainRibbon = new Ribbon();

            RibbonTab dataConnTab = new RibbonTab() { Header = "加载数据" };
            RibbonGroup dataConnGrp = new RibbonGroup() { Header = "加载数据" };
            dataConnTab.Items.Add(dataConnGrp);
            RibbonTab mapNavTab = new RibbonTab() { Header = "视图操作" };
            RibbonGroup mapNavGrp = new RibbonGroup() { Header = "视图操作" };
            mapNavTab.Items.Add(mapNavGrp);
            RibbonButton rbtn = null;
            foreach (IDsTool tool in m_tools)
            {
                tool.OnCreate(m_application);

                switch (tool.Category)
                {
                    case "加载数据":
                        rbtn = new RibbonButton();
                        rbtn.Label = tool.Caption;
                        dataConnGrp.Items.Add(rbtn);
                        break;
                    case "视图操作":
                        rbtn = new RibbonButton();
                        rbtn.Label = tool.Caption;
                        mapNavGrp.Items.Add(rbtn);
                        break;
                    default:
                        break;
                }
            }

            foreach (IDsCommand cmd in m_commands)
            {
                cmd.OnCreate(m_application);
                switch (cmd.Category)
                {
                    case "加载数据":
                        rbtn = new RibbonButton();
                        rbtn.Label = cmd.Caption;
                        dataConnGrp.Items.Add(rbtn);
                        break;
                    case "视图操作":
                        rbtn = new RibbonButton();
                        rbtn.Label = cmd.Caption;
                        mapNavGrp.Items.Add(rbtn);
                        break;
                    default:
                        break;
                }
            }

            mainRibbon.Items.Add(dataConnTab);
            mainRibbon.Items.Add(mapNavTab);

            mainGrid.Children.Add(mainRibbon);
        }
    }
}
