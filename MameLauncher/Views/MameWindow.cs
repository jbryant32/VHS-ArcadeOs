using MameLauncher.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MameLauncher.Views
{
    public class MameWindow : WindowController
    {
        public MameWindow(string Name, bool Active, IntPtr hwn) : base(Name, Active, hwn)
        {
        }

        void CreateProc()
        {
            string Executable = @"c:\mame\mame.exe";
            try
            {
                //wrapped inside a try because mame might open and then crash if no game present
                do
                {
                    Console.Clear();
                    Console.WriteLine("mame openinig");
                } while (MameLaunchSetupAndRun().MainWindowHandle == IntPtr.Zero);
            }
            catch (Exception)
            {

                Console.WriteLine("No gAME !");
            }

            var Mproc = Process.GetProcessesByName("mame").FirstOrDefault();


            if (Mproc != null)//just incase it crashes or somethin hwn is not null
                Hwn = Process.GetProcessesByName("mame").FirstOrDefault().MainWindowHandle;

            else
                Hwn = IntPtr.Zero;

            var proc = Process.GetProcessesByName("mame").FirstOrDefault();
            if (proc == null)  // if process not already running do this 
            {
                var Fproc = Process.Start(Executable);

                do
                {
                    //wait for proc to create
                    Console.Clear();
                    Thread.Sleep(100);
                    Console.WriteLine("Opening Mame....");
                } while (Process.GetProcessesByName("mame").FirstOrDefault() == null);

                do
                {
                    //wait for Window Handle to be Created
                    Console.Clear();
                    Thread.Sleep(100);
                    Console.WriteLine($"Opening  Mame....");
                } while (Process.GetProcessesByName("mame").FirstOrDefault().MainWindowHandle == IntPtr.Zero);//wait for window creation

                Hwn = Fproc.MainWindowHandle;
            }
            else
            {//process already running thats a problem kill that mofo
                Console.WriteLine("There can be only one ....mame running");
                Process.GetProcessesByName("mame").FirstOrDefault().Kill();
                Hwn = IntPtr.Zero;
            }
        }

        //create the process
        public override void ActivateWindow()
        {
            //CreateProc();
            ProcessStartInfo processStart = new ProcessStartInfo("cmd");
            processStart.WorkingDirectory = @"c:\mame";
            processStart.Arguments = "/C mame dstlk";
            Process.Start(processStart);
            base.ActivateWindow();
        }

        #region Mame Setup

        string GetMameCmd()
        {
            var FilePath = @"C:\FrontEndAppFiles\mameCmd.vhs";
            var cmd = string.Empty;
            WaitForFileWriteCompletion();//wait for file to close
            using (var read = File.OpenText(FilePath))//read the  cmd to be sent to mame from the FE game to be launched
            {
                cmd = read.ReadToEnd();
                Console.WriteLine("Command" + cmd);
                read.Close();
            }
            return cmd;
        }
        void WaitForFileWriteCompletion()
        {
            StreamReader fileStream = null;
            FileInfo fileInfo = new FileInfo(@"C:\FrontEndAppFiles\mameCmd.vhs");
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
        private Process MameLaunchSetupAndRun()
        {
            //is mame open if move focuse off fe and keep checking if focus changew
            var mameProc = Process.GetProcessesByName("mame").FirstOrDefault();
            if (mameProc == null)//if mame is not already running start mame
            {
                Console.WriteLine("Starting Game");
                ProcessStartInfo startInfo = new ProcessStartInfo("cmd");
                startInfo.RedirectStandardInput = true;
                startInfo.UseShellExecute = false;
                var proc = Process.Start(startInfo);
                proc.StandardInput.WriteLine(GetMameCmd());
                return mameProc;
            }
            else { return mameProc; }
        }
        #endregion
    }
}
