using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
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
            LoadServers();
        }

        private void button_Click(object sender, RoutedEventArgs e)
        {
            var currentServer = ServerSettingses.FirstOrDefault(x => x.Hostname == cmbServers.Text);

            using (SshClient linux = new SshClient(currentServer?.Hostname, currentServer?.Username, currentServer?.Password))
            {
                linux.Connect();
                if (!linux.IsConnected)
                {
                    // Display error
                    MessageBox.Show("failure");
                }
                else
                {
                    MessageBox.Show("Success!");
                    linux.RunCommand("cd /home/mdeering");
                    linux.RunCommand("./update.sh");
                }
                linux.Disconnect();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            var currentServer = ServerSettingses.FirstOrDefault(x => x.Hostname == cmbServers.Text);

            using (SshClient linux = new SshClient(currentServer?.Hostname, currentServer?.Username, currentServer?.Password))
            {
                linux.RunCommand("cd /home/mdeering");
                linux.RunCommand("./update.sh");
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            new AddServerWindow().ShowDialog();
            LoadServers();
        }

        public List<ServerSettings> ServerSettingses { get; set; }


        private void LoadServers()
        {
            if (!File.Exists("settings.bbq")) File.Create("settings.bbq").Close();
            var serversText = File.ReadAllText("settings.bbq");
            var servers = serversText.Split('\n');

            if (ServerSettingses == null) ServerSettingses = new List<ServerSettings>();

            cmbServers.Items.Clear();

            foreach (var server in servers)
            {
                if (string.IsNullOrEmpty(server)) continue;

                cmbServers.Items.Add(server.Split(':')[0]);
                ServerSettingses.Add(new ServerSettings
                {
                    Hostname = server.Split(':')[0],
                    Username = server.Split(':')[1],
                    Password = server.Split(':')[2],
                });
            }
        }
    }
}
