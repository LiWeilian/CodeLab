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
        [Import]
        private IDsMapControl m_mapCtrl;
        [Import]
        private IDsGISControls m_gisCtrls;

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
        #endregion

        #region 指针操作
        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            //使用插件前先调用此方法
            InitializeComponentModel();

            InitializeGISComponents();

            InitializeUI();
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
                    DeleteObject(ptr);
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
                        DeleteObject(ptr);
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

            mainRibbon.SetValue(Grid.RowProperty, 0);
            mainRibbon.SetValue(Grid.ColumnProperty, 0);
            mainRibbon.SetValue(Grid.ColumnSpanProperty, 3);

            foreach (MainRibbonTabDef tabDef in mainRibbonDef.RibbonTabs)
            {
                RibbonTab ribbonTab = new RibbonTab() { Header = tabDef.Header };
                mainRibbon.Items.Add(ribbonTab);

                foreach (MainRibbonGroupDef groupDef in tabDef.RibbonGroups)
                {
                    RibbonGroup ribbonGroup = new RibbonGroup() { Header = groupDef.Header };
                    ribbonTab.Items.Add(ribbonGroup);

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
                                rbtn.SmallImageSource = CreateBitmapImageSource(cmd.SmallBitmap);

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
                                rbtn.SmallImageSource = CreateBitmapImageSource(tool.SmallBitmap);
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

            #region Ribbon
            /*
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
                        rbtn.SmallImageSource = CreateBitmapImageSource(tool.SmallBitmap);
                        dataConnGrp.Items.Add(rbtn);
                        break;
                    case "视图操作":
                        rbtn = new RibbonButton();
                        rbtn.Label = tool.Caption;
                        rbtn.SmallImageSource = CreateBitmapImageSource(tool.SmallBitmap);
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
                        rbtn.SmallImageSource = CreateBitmapImageSource(cmd.SmallBitmap);
                        dataConnGrp.Items.Add(rbtn);
                        break;
                    case "视图操作":
                        rbtn = new RibbonButton();
                        rbtn.Label = cmd.Caption;
                        if (cmd.LargeBitmap != null)
                        {
                            rbtn.LargeImageSource = CreateBitmapImageSource(cmd.LargeBitmap);
                        } else
                        {
                            rbtn.SmallImageSource = CreateBitmapImageSource(cmd.SmallBitmap);
                        }
                        mapNavGrp.Items.Add(rbtn);
                        break;
                    default:
                        break;
                }
            }

            mainRibbon.Items.Add(dataConnTab);
            mainRibbon.Items.Add(mapNavTab);

            mainRibbon.Items.Add(CreateTestUITab());

            mainRibbon.Items.Add(CreateTestStyleTab());

            mainRibbon.Items.Add(CreateUserControlTab());

            //mainRibbon.Items.Add(CreatePluginPanelTab());

            mainRibbon.SetValue(Grid.RowProperty, 0);
            mainRibbon.SetValue(Grid.ColumnProperty, 0);
            mainRibbon.SetValue(Grid.ColumnSpanProperty, 3);

            mainGrid.Children.Add(mainRibbon);
            */
            #endregion

            #region GIS Controls
            /*
            AxMapControl mapCtrl = new AxMapControl();
            mapCtrl.BeginInit();
            mapCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            
            mapHost.Child = mapCtrl;
            mapCtrl.OnMouseDown += delegate (object sender, IMapControlEvents2_OnMouseDownEvent e) {
                MessageBox.Show(string.Format("{0},{1}", e.x, e.y));
            };
            mapCtrl.EndInit();

            AxTOCControl tocCtrl = new AxTOCControl();
            tocCtrl.BeginInit();
            tocCtrl.Dock = System.Windows.Forms.DockStyle.Fill;
            tocHost.Child = tocCtrl;
            tocCtrl.EndInit();
            tocCtrl.SetBuddyControl(mapCtrl);
            */
            /*
            if (m_mapCtrl != null)
            {
                // Not OK 只能放置在0，0位置
                (m_mapCtrl.DsMapControl as GDDST.GIS.Controls.UCMapControl).SetValue(Grid.RowProperty, 1);
                (m_mapCtrl.DsMapControl as GDDST.GIS.Controls.UCMapControl).SetValue(Grid.ColumnProperty, 1);
                
                mainGrid.Children.Add((m_mapCtrl.DsMapControl as GDDST.GIS.Controls.UCMapControl));
                //

                //OK
                //mainGrid.Children.Add(m_mapCtrl.CreateMapControl(1, 1));


                //OK
                //mapCtrlGrid.Children.Add(m_mapCtrl.DsMapControl);    

            }
            */

            if (m_gisCtrls != null)
            {
                m_gisCtrls.InitializeControls(m_application);

                m_application.MapControl = m_gisCtrls.MapControlCore;
                m_application.LegendControl = m_gisCtrls.LegendControlCore;

                mapCtrlGrid.Children.Add(m_gisCtrls.MapControl);
                legendCtrlGrid.Children.Add(m_gisCtrls.LegendControl);
            }
            #endregion

        }
    }
}
