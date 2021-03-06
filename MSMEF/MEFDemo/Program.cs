﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;

namespace MEFDemo
{
    class Program
    {
        private CompositionContainer m_container;
        
        static void Main(string[] args)
        {
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
    }
}
