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
using System.IO;

namespace LaunchPad.Views
{
    /// <summary>
    /// Interaction logic for ControlsDrivers.xaml
    /// </summary>
    public partial class ControlsDrivers : UserControl
    {
        public ControlsDrivers()
        {
            InitializeComponent();

        }

        private void ButtonGetSysInfo_Click(object sender, RoutedEventArgs e)
        {
            
            StringBuilder @string = new StringBuilder();
            var queryCollection = ViewModel.ViewModelsDrivers.GetNetworInformation();

            var VideCardQueryCollection = ViewModel.ViewModelsDrivers.GetVideoCardInformation();

            var MotherBoardOnBoardAudioCollection = ViewModel.ViewModelsDrivers.GetAudioCardInfo();

            var DiskDriveCollection = ViewModel.ViewModelsDrivers.GetDiskDriveInfo();
            try
            {
                foreach (var Qobj in queryCollection)
                {
                    if ((bool)Qobj["IPEnabled"])
                    {
                        @string.AppendLine($"Index: {Qobj["Index"]}");
                        @string.AppendLine($"Description: {Qobj["Description"]}");
                        @string.AppendLine($"DHCPEnabled: {Qobj["DHCPEnabled"]}");
                        var IP = (string[])Qobj["IPAddress"];
                        @string.AppendLine($"IPAddress: {IP[0]}");
                    }
                }

                foreach (var Qobj in VideCardQueryCollection)
                {
                    @string.AppendLine($"\n Video Card Info");
                    @string.AppendLine($"Name: {Qobj["Name"]}");
                    @string.AppendLine($"Description: {Qobj["Description"]}");
                    @string.AppendLine($"Status: {Qobj["Status"]}");
                    @string.AppendLine($"StatusInfo: {Qobj["StatusInfo"]}");
                    @string.AppendLine($"SystemName: {Qobj["SystemName"]}");
                    @string.AppendLine($"DriverDate: {Qobj["DriverDate"]}");

                }
                foreach (var Qobj in MotherBoardOnBoardAudioCollection)
                {
                    @string.AppendLine("\n Audio Device");
                    @string.AppendLine($"Name: {Qobj["Name"]}");
                    @string.AppendLine($"Description: {Qobj["Description"]}");
                    @string.AppendLine($"Manufacturer: {Qobj["Manufacturer"]}");
                    @string.AppendLine($"Status: {Qobj["Status"]}");
                    @string.AppendLine($"StatusInfo: {Qobj["StatusInfo"]}");
                    @string.AppendLine($"SystemName: {Qobj["SystemName"]}");
                }




            }
            catch (Exception ex)
            {

                TextBlockDrivers.Text = string.Format("Error!: {0} ", ex.Message);

            }
            TextBlockDrivers.Text = @string.ToString();
        }
    }
}
