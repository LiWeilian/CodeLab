using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using AvalonDock.Layout.Serialization;
using System.IO;

namespace AvalonDockTest
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //保存布局
            var serializer = new XmlLayoutSerializer(DManager);
            using (var stream = new StreamWriter("lay.txt"))
                serializer.Serialize(stream);
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            //恢复布局
            var serializer = new XmlLayoutSerializer(DManager);
            using (var stream = new StreamReader("lay.txt"))
                serializer.Deserialize(stream);

        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(0);
            //this.Close();
        }

        private void TextBox_SelectionChanged(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("-----test-----");
        }
    }
}
