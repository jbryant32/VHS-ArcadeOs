using MameLauncher.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using ArcadeOScfg;
namespace MameLauncher.Views
{
    public class MameWindow : WindowController
    {
        Cfg Configs;
        public MameWindow(string Name, bool Active, IntPtr hwn) : base(Name, Active, hwn)
        {
            Configs = StateManager.AppConfig;
        }

      

        //create the process
        public override void ActivateWindow()
        {
            var FeProc = Process.GetProcessesByName("FrontEnd").FirstOrDefault();
            var VpProc = Process.GetProcessesByName("videoPlayer").FirstOrDefault();
            if (FeProc!=null)
            {
                FeProc.Kill();
            }
            if (VpProc != null)
            {
                VpProc.Kill();
            }

            ProcessStartInfo processStart = new ProcessStartInfo("cmd");
            processStart.WorkingDirectory = @"c:\Emulators\mame";
            processStart.Arguments = $"/C mame {GetMameCmd()}";
            Process.Start(processStart);
            base.ActivateWindow();
        }

        #region Mame Setup

        string GetMameCmd()
        {
            var FilePath = @"C:\OScfg\FrontEndAppFiles\mameCmd.vhs";
            var cmd = string.Empty;
            WaitForFileWriteCompletion();//wait for file to close
            using (var read = File.OpenText(FilePath))//read the  cmd to be sent to mame from the FE game to be launched
            {
                cmd = read.ReadToEnd();
                read.Close();
            }
            return cmd;
        }
        void WaitForFileWriteCompletion()
        {
            StreamReader fileStream = null;
            FileInfo fileInfo = new FileInfo(@"C:\OScfg\FrontEndAppFiles\mameCmd.vhs");
            do
            {
                try
                {
                    fileStream = fileInfo.OpenText();//checl

                    Console.WriteLine("File Ready");

                }
                catch (Exception ex)
                {

                    Console.WriteLine("File error: " + ex.Message);
                    fileStream = null;

                }

            } while (fileStream == null);
            fileStream.Close();
        }
      
        #endregion
    }
}
