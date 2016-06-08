using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Shelltest1
{
    /// <summary>
    /// Interaction logic for AddServerWindow.xaml
    /// </summary>
    public partial class AddServerWindow : Window
    {
        public AddServerWindow()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            var file = new FileInfo("settings.bbq");
            if (!file.Exists) file.Create().Close();

            var settingString = $"{tbHostname.Text}:{tbUsername.Text}:{pbPassword.Password}\n";

            File.AppendAllText(file.FullName, settingString);
        }
    }
}
