using Renci.SshNet;
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

namespace Shelltest1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            using (SshClient linux = new SshClient("192.168.0.2", "mdeering", "Lolpops123"))
            {
                linux.Connect();
                linux.Disconnect();
                if (!linux.IsConnected)
                {
                    // Display error
                    MessageBox.Show("failure");
                }
                else
                {
                    MessageBox.Show("Success!");
                }
            }
        }
    }
}
