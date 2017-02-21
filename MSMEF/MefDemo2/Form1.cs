using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

using MefPluginDemo;

namespace MefDemo2
{
    public partial class Form1 : Form
    {
        private CompositionContainer m_container;

        [ImportMany]
        private IEnumerable<IPluginIntf> m_plugins;
        [ImportMany]
        private IEnumerable<IMefCommand> m_cmds;
        [ImportMany]
        private IEnumerable<IMefTool> m_tools;
        public Form1()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            AggregateCatalog catalog = new AggregateCatalog();
            string pluginPath = string.Format("{0}plugins\\", AppDomain.CurrentDomain.BaseDirectory);
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

        private void Form1_Load(object sender, EventArgs e)
        {
            ToolStripMenuItem mi = new ToolStripMenuItem("插件");
            menuStrip1.Items.Add(mi);
            foreach (IPluginIntf plugin in m_plugins)
            {
                ToolStripMenuItem submi = new ToolStripMenuItem(plugin.Text);
                submi.Click += (s, args) => { plugin.Do(); };
                mi.DropDownItems.Add(submi);
            }

            ToolStripMenuItem mi2 = new ToolStripMenuItem("Cmd插件");
            menuStrip1.Items.Add(mi2);
            foreach (IMefCommand cmd in m_cmds)
            {
                ToolStripMenuItem submi = new ToolStripMenuItem(cmd.Text);
                submi.Click += (s, args) => { cmd.OnClick(); };
                mi2.DropDownItems.Add(submi);
            }

            ToolStripMenuItem mi3 = new ToolStripMenuItem("Tool插件");
            menuStrip1.Items.Add(mi3);
            foreach (IMefTool tool in m_tools)
            {
                ToolStripMenuItem submi = new ToolStripMenuItem(tool.Text);
                submi.Click += (s, args) => { tool.OnClick(); };
                mi3.DropDownItems.Add(submi);
            }
        }

    }
}
