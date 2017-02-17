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
            catch (Exception)
            {

                throw;
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
        }

    }
}
