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
using System.Windows.Shapes;

namespace GDDST.GIS
{
    /// <summary>
    /// Window1.xaml 的交互逻辑
    /// </summary>
    public partial class WinConnectToSDE : Window
    {
        public string Server { get { return txtServer.Text; } }
        public string Instance { get { return txtInstance.Text; } }
        public string Database { get { return txtDatabase.Text; } }
        public string User { get { return txtUser.Text; } }
        public string Password { get { return txtPassword.Password; } }
        public string Version { get { return txtVersion.Text; } }
        public WinConnectToSDE()
        {
            InitializeComponent();
        }

        private void btnOK_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = false;
        }
    }
}
