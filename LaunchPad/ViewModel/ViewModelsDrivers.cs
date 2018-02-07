using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Management;
using System.IO;
using System.Runtime.InteropServices;
using System.Diagnostics;

namespace LaunchPad.ViewModel
{
    public class ViewModelsDrivers
    {


        public static ManagementObjectCollection GetNetworInformation()
        {
            var Query = new ManagementObjectSearcher("SELECT * FROM Win32_NetworkAdapterConfiguration");
            return Query.Get();
        }
        public static ManagementObjectCollection GetVideoCardInformation()
        {

            var Query = new ManagementObjectSearcher("SELECT * FROM Win32_VideoController");
            return Query.Get();

        }
        public static ManagementObjectCollection GetAudioCardInfo()
        {
            var Query = new ManagementObjectSearcher("SELECT * FROM Win32_SoundDevice");

            return Query.Get();
        }
        public static ManagementObjectCollection GetDiskDriveInfo()
        {
            var MangageObj = new ManagementObjectSearcher("SELECT * FROM   Win32_DiskDrive");
            return MangageObj.Get();
        }
        public static void SetNetWorkInfo(bool isDHCP, string IpAddress, string Subnet, string GateWay)
        {

        }

        /// <summary>
        /// Installs Drivers from folder structure
        /// /Drivers
        ///     /Mobo
        ///     /GraphicsDevice
        ///     /AudioDevice
        ///     /NetWorkDevice
        /// </summary>
        public static void InstallDrivers()
        {
            var ActiveDrive = @"d:\";
          //  var MoboInf = Directory.GetFiles($"{ActiveDrive}\\Mobo", "*.inf")[0];
            var GraphicsDevice = Directory.GetFiles($"{ActiveDrive}\\GraphicsDevice", "*.inf")[0];
            var StartInfo = new ProcessStartInfo("cmd");
            StartInfo.UseShellExecute = false;
            StartInfo.Arguments = $"/c C:\\Windows\\System32\\InfDefaultInstall.exe {GraphicsDevice}";
            var Proc = Process.Start(StartInfo);
            Proc.WaitForExit();
          
        }
        [DllImport("Setupapi.dll", EntryPoint = "InstallHinfSection", CallingConvention = CallingConvention.StdCall)]
        public static extern void InstallHinfSection(
    [In] IntPtr hwnd,
    [In] IntPtr ModuleHandle,
    [In, MarshalAs(UnmanagedType.LPWStr)] string CmdLineBuffer,
    int nCmdShow);
    }
}
