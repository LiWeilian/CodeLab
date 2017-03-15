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
using System.IO;
using System.Drawing;
using System.Windows.Interop;
using System.Windows.Controls.Ribbon;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

using GDDST.GIS.PluginEngine;
using GDDST.GIS.dsSystem;
using GDDST.GIS.ui;

namespace GDDST.GIS
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        [Import]
        private IDsApplication m_application;
        [Import]
        private IDsUIStyle m_UIStyle;
        

        #region GIS初始化组件
        [ImportMany]
        private IEnumerable<IDsGISInitialize> m_gisInitList;
        #endregion

        #region 插件容器
        private CompositionContainer m_container;
        [ImportMany]
        private IEnumerable<IDsTool> m_tools;
        [ImportMany]
        private IEnumerable<IDsCommand> m_commands;
        [ImportMany]
        private IEnumerable<IDsPanel> m_pluginPanels;
        [ImportMany]
        private IEnumerable<IDsGISControls> m_gisCtrls;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            InitializeSystemSettinigs();
            //使用插件前先调用此方法
            InitializeComponentModel();

            InitializeGISComponents();

            InitializeUI();
        }
                
        private void Window_Closed(object sender, EventArgs e)
        {
            ShutdownGISComponents();
        }

        private void InitializeSystemSettinigs()
        {
            App.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
        }

        private void InitializeGISComponents()
        {
            if (m_gisInitList != null)
            {
                foreach (IDsGISInitialize gisInit in m_gisInitList)
                {
                    gisInit.GISInitialize();
                }
            }
        }

        private void ShutdownGISComponents()
        {
            if (m_gisInitList != null)
            {
                foreach (IDsGISInitialize gisInit in m_gisInitList)
                {
                    gisInit.GISShutdown();
                }
            }
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

            #region
            if (m_tools != null)
            {
                foreach (IDsTool tool in m_tools)
                {
                    m_application.Plugins.Add(tool);
                }
            }
            if (m_commands != null)
            {
                foreach (IDsCommand cmd in m_commands)
                {
                    m_application.Plugins.Add(cmd);
                }
            }

            if (m_pluginPanels != null)
            {
                foreach (IDsPanel pnl in m_pluginPanels)
                {
                    m_application.Plugins.Add(pnl);
                }
            }
            #endregion
        }

        private BitmapSource CreateBitmapImageSource(string imageFileName)
        {
            IntPtr ptr = IntPtr.Zero;
            try
            {
                Bitmap bmp = new Bitmap(string.Format("{0}\\..\\images\\{1}", AppDomain.CurrentDomain.BaseDirectory, imageFileName));
                ptr = bmp.GetHbitmap();
                return Imaging.CreateBitmapSourceFromHBitmap(ptr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            }
            catch (Exception)
            {
                return null;
            }
            finally
            {
                if (ptr != IntPtr.Zero)
                {
                    dsSystem.Win32.DeleteObject(ptr);
                }
            }
            
        }
        private BitmapSource CreateBitmapImageSource(Bitmap bitmap)
        {
            if (bitmap == null)
            {
                return null;
            } else
            {
                IntPtr ptr = IntPtr.Zero;
                try
                {
                    ptr = bitmap.GetHbitmap();
                    return Imaging.CreateBitmapSourceFromHBitmap(ptr, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
                }
                catch (Exception)
                {
                    return null;
                }
                finally
                {
                    if (ptr != IntPtr.Zero)
                    {
                        dsSystem.Win32.DeleteObject(ptr);
                    }
                }
            }
        }
        private RibbonTab CreateTestUITab()
        {
            RibbonTab testUITab = new RibbonTab() { Header = "测试Ribbon控件" };
            RibbonGroup testUIGrp = new RibbonGroup() { Header = "测试Ribbon控件1" };
            testUITab.Items.Add(testUIGrp);

            TextBox txtbox = new TextBox();
            txtbox.Width = 100;
            testUIGrp.Items.Add(txtbox);

            ComboBox combobox = new ComboBox();
            combobox.Width = txtbox.Width;
            combobox.Items.Add("Item0");
            combobox.Items.Add("Item1");
            combobox.Items.Add("Item2");
            testUIGrp.Items.Add(combobox);


            RibbonGroup testUIGrp2 = new RibbonGroup() { Header = "测试Ribbon控件2" };
            testUITab.Items.Add(testUIGrp2);

            RibbonCheckBox chkbox = new RibbonCheckBox();
            chkbox.Label = "选项1";
            testUIGrp2.Items.Add(chkbox);
            chkbox = new RibbonCheckBox();
            chkbox.Label = "选项2";
            testUIGrp2.Items.Add(chkbox);
            chkbox = new RibbonCheckBox();
            chkbox.Label = "选项3";
            testUIGrp2.Items.Add(chkbox);


            return testUITab;
        }

        private RibbonTab CreateUserControlTab()
        {
            RibbonTab userCtrlTab = new RibbonTab() { Header = "测试用户自定义控件" };
            RibbonGroup userCtrlGrp = new RibbonGroup() { Header = "测试用户自定义控件1" };
            userCtrlTab.Items.Add(userCtrlGrp);

            QueryPanel qp = new QueryPanel();
            userCtrlGrp.Items.Add(qp);

            return userCtrlTab;
        }

        private AppSkin CreateMainStyle(string name)
        {
            string styleFileName = string.Format("{0}..\\skins\\{1}.xaml", AppDomain.CurrentDomain.BaseDirectory, name);
            if (File.Exists(styleFileName))
            {
                try
                {
                    ResourceDictionary rd = new ResourceDictionary();
                    rd.Source = new Uri(styleFileName, UriKind.Absolute);//"H:\\GitHub\\CodeLab\\GDDST.GIS\\output\\skins\\Black.xaml"
                    AppSkin ms = new AppSkin() { Name = name, ResrcDict = rd};
                    return ms;
                }
                catch (Exception)
                {
                    return null;
                }
            }
            return null;
        }

        private List<AppSkin> GetAppSkins()
        {
            List<AppSkin> appSkins = new List<AppSkin>();

            string[] filenames = Directory.GetFiles(string.Format("{0}..\\skins\\", AppDomain.CurrentDomain.BaseDirectory), "*.xaml");
            foreach (string filename in filenames)
            {
                string name = System.IO.Path.GetFileNameWithoutExtension(filename);
                try
                {
                    ResourceDictionary rd = new ResourceDictionary();
                    rd.Source = new Uri(filename, UriKind.Absolute);
                    AppSkin appSkin = new AppSkin() { Name = name, ResrcDict = rd };
                    appSkins.Add(appSkin);
                }
                catch (Exception)
                {
                }
            }

            return appSkins;
        }

        private RibbonTab CreateTestStyleTab()
        {
            RibbonTab testStyleTab = new RibbonTab() { Header = "测试样式设置" };
            RibbonGroup testStyleGrp = new RibbonGroup() { Header = "测试样式设置" };
            testStyleTab.Items.Add(testStyleGrp);

            ComboBox cbStyles = new ComboBox();
            //cbStyles.IsEditable = true;
            cbStyles.Width = 100;
            cbStyles.DisplayMemberPath = "Name";
            cbStyles.SelectedValuePath = "ResrcDict";

            cbStyles.ItemsSource = GetAppSkins();

            //cbStyles.Items.Add("Black");
            //cbStyles.Items.Add("Blue");
            //cbStyles.Items.Add("Gray");
            cbStyles.SelectionChanged += delegate (object sender, SelectionChangedEventArgs e)
            {
                App.Current.Resources = (cbStyles.SelectedItem as AppSkin).ResrcDict;
            };
            testStyleGrp.Items.Add(cbStyles);


            return testStyleTab;
        }

        private RibbonTab CreatePluginPanelTab()
        {
            
            RibbonTab pluginPanelTab = new RibbonTab() { Header = "地图查询" };
            /*
            foreach (IDsPanel pluginPanel in m_pluginPanels)
            {
                RibbonGroup pluginPanelGrp = new RibbonGroup() { Header = "测试面板插件" };
                pluginPanelTab.Items.Add(pluginPanelGrp);

                pluginPanel.OnCreate(m_application);
                pluginPanel.OnActivate();

                pluginPanelGrp.Items.Add(pluginPanel.PluginPanel);
            }
            */
            return pluginPanelTab;
        }

        private IDsPlugin GetPluginByNameSapce(string nameSpace)
        {
            if (m_commands != null)
            {
                IEnumerable<IDsCommand> cmds = m_commands.Where(c => c.ToString() == nameSpace);
                if (cmds != null && cmds.Count() > 0)
                {
                    return cmds.First();
                }
            }
            if (m_tools != null)
            {
                IEnumerable<IDsTool> tools = m_tools.Where(c => c.ToString() == nameSpace);
                if (tools != null && tools.Count() > 0)
                {
                    return tools.First();
                }
            }
            if (m_pluginPanels != null)
            {
                IEnumerable<IDsPanel> pnls = m_pluginPanels.Where(c => c.ToString() == nameSpace);
                if (pnls != null && pnls.Count() > 0)
                {
                    return pnls.First();
                }
            }

            return null;
        }

        private void InitializeUI()
        {
            #region 根据XML配置文件生成Ribbon
            MainRibbonConfigXML mainRibbonCfg = new MainRibbonConfigXML();
            MainRibbonDef mainRibbonDef = mainRibbonCfg.CreateMainRibbonDef();

            Ribbon mainRibbon = new Ribbon();

            //mainRibbon.Height = 200;
            
            mainRibbon.SetValue(Grid.RowProperty, 0);
            mainRibbon.SetValue(Grid.ColumnProperty, 0);
            mainRibbon.SetValue(Grid.ColumnSpanProperty, 3);

            foreach (MainRibbonTabDef tabDef in mainRibbonDef.RibbonTabs)
            {
                RibbonTab ribbonTab = new RibbonTab() { Header = tabDef.Header };
                mainRibbon.Items.Add(ribbonTab);
                //ribbonTab.Margin = new Thickness(0, 0, 0, -125);

                foreach (MainRibbonGroupDef groupDef in tabDef.RibbonGroups)
                {
                    RibbonGroup ribbonGroup = new RibbonGroup() { Header = groupDef.Header };
                    ribbonTab.Items.Add(ribbonGroup);
                    //ribbonGroup.Margin = new Thickness(0, 0, 0, -100);

                    foreach (MainRibbonComponentDef comDef in groupDef.RibbonComponents)
                    {
                        IDsPlugin plugin = GetPluginByNameSapce(comDef.NameSpace);
                        if (plugin != null)
                        {
                            if (plugin is IDsCommand)
                            {
                                IDsCommand cmd = plugin as IDsCommand;
                                cmd.OnCreate(m_application);

                                RibbonButton rbtn = new RibbonButton();
                                rbtn.Label = comDef.Label;
                                rbtn.Tag = cmd;

                                if (comDef.width > 0)
                                {
                                    rbtn.Width = comDef.width;
                                }

                                switch (comDef.ImageType)
                                {
                                    case ImageType.itSmall:
                                        rbtn.SmallImageSource = CreateBitmapImageSource(cmd.SmallBitmap);
                                        break;
                                    case ImageType.itLarge:
                                        rbtn.LargeImageSource = CreateBitmapImageSource(cmd.LargeBitmap);
                                        break;
                                    default:
                                        break;
                                }

                                rbtn.Click += delegate (object sender, RoutedEventArgs e)
                                {
                                    (rbtn.Tag as IDsCommand).OnActivate();
                                };

                                ribbonGroup.Items.Add(rbtn);
                            }
                            else if (plugin is IDsTool)
                            {
                                IDsTool tool = plugin as IDsTool;
                                tool.OnCreate(m_application);

                                RibbonButton rbtn = new RibbonButton();
                                rbtn.Label = comDef.Label;
                                rbtn.Tag = tool;

                                if (comDef.width > 0)
                                {
                                    rbtn.Width = comDef.width;
                                }
                                
                                switch (comDef.ImageType)
                                {
                                    case ImageType.itSmall:
                                        rbtn.SmallImageSource = CreateBitmapImageSource(tool.SmallBitmap);
                                        break;
                                    case ImageType.itLarge:
                                        rbtn.LargeImageSource = CreateBitmapImageSource(tool.LargeBitmap);
                                        break;
                                    default:
                                        break;
                                }
                                rbtn.Click += delegate (object sender, RoutedEventArgs e)
                                {
                                    (rbtn.Tag as IDsTool).OnActivate();
                                };

                                ribbonGroup.Items.Add(rbtn);
                            }
                            else if (plugin is IDsPanel)
                            {
                                IDsPanel pluginPnl = plugin as IDsPanel;
                                pluginPnl.OnCreate(m_application);

                                ribbonGroup.Items.Add(pluginPnl.PluginPanel);
                            }
                        }
                    }
                }
            }

            mainRibbon.Items.Add(CreateTestStyleTab());

            mainGrid.Children.Add(mainRibbon);
            #endregion

            #region GIS Controls

            GISControlsConfigXML gisCtrlsCfg = new GISControlsConfigXML();
            GISControlsDef gisCtrlsDef = gisCtrlsCfg.CreateGISControlsDef();
            if (gisCtrlsDef != null && m_gisCtrls != null)
            {
                IEnumerable<IDsGISControls> items = from g in m_gisCtrls
                           where g.ToString() == gisCtrlsDef.NameSpace
                           select g;

                if (items.Count() > 0)
                {
                    IDsGISControls gisCtrls = items.First();

                    if (gisCtrls != null)
                    {
                        gisCtrls.InitializeControls(m_application);

                        legendCtrlGrid.Children.Add(gisCtrls.LegendControl);
                        mapCtrlGrid.Children.Add(gisCtrls.MapControl);
                    }
                }
            }
            #endregion

        }
    }
}
