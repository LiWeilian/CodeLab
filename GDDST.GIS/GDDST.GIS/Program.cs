using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GDDST.GIS
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            GDDST.GIS.App app = new GDDST.GIS.App();
            app.ShutdownMode = System.Windows.ShutdownMode.OnMainWindowClose;
            app.InitializeComponent();
            MainWindow windows = new MainWindow();
            app.MainWindow = windows;
            app.Run();
        }
    }
}
