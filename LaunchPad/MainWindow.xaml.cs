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
using LaunchPad.ViewModel;
using System.Diagnostics;
using LaunchPad.UIHook;

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
            this.KeyUp += MainWindow_KeyDown;

            this.Loaded += MainWindow_Loaded;
            this.Cursor = Cursors.None;
        }
      
        private void MainWindow_Loaded(object sender, RoutedEventArgs e)
        {
            WindowUIHook.Attach(this, StackPanelLaunchPadControls);
        }


        private void MainWindow_KeyDown(object sender, KeyEventArgs e)
        {
             
        }

        private void StartArcade_Click(object sender, RoutedEventArgs e)
        {
            var file = File.Open(@"C:\OScfg\FrontEndAppFiles\Shell.vhs", FileMode.Open);
            StreamWriter writer = new StreamWriter(file);
            writer.WriteLine("true");
            writer.Close();
            file.Close();
        }

        private void ButtonConfigureArcade_Click(object sender, RoutedEventArgs e)
        {

            this.DataContext = new ViewModelOptions();
        }

        private void ButtonShutDownArcade_Click(object sender, RoutedEventArgs e)
        {
            var startInfo = new ProcessStartInfo("cmd", "/c shutdown /f /p");
            startInfo.CreateNoWindow = true;
            startInfo.UseShellExecute = false;
            var proc = Process.Start(startInfo);
        }
    }
}
