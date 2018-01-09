using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Automation.Peers;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace LaunchPad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        ButtonAutomationPeer AutomationPeer;
        public MainWindow()
        {
            InitializeComponent();
            AutomationPeer = new ButtonAutomationPeer(ButtonStartArcade);
            this.KeyDown += MainWindow_KeyDown;
        }

        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Up)
            {
               
            }
           
        }

        private void StartArcade_Click(object sender, RoutedEventArgs e)
        {
            var file = File.Open(@"C:\FrontEndAppFiles\Shell.vhs", FileMode.Open);
            StreamWriter writer = new StreamWriter(file);
            writer.WriteLine("true");
            writer.Close();
            file.Close();
        }
    }
}
