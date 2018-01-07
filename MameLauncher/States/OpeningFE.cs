using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MameLauncher.States
{
    public class OpeningFE : IState
    {
        StateManager stateManager;
        public OpeningFE(StateManager stateM)
        {
            this.stateManager = stateM;
        }
        void FEStartup()
        {
       
            var dir = @"C:\Share\BuildFE\Debug\FrontEnd.exe";

            try
            {
                ProcessStartInfo processStartInfo = new ProcessStartInfo(dir);
                processStartInfo.RedirectStandardInput = true;
                processStartInfo.RedirectStandardOutput = true;
                processStartInfo.RedirectStandardError = true;
                processStartInfo.UseShellExecute = false;
                processStartInfo.WorkingDirectory = Environment.CurrentDirectory;
                var FrontEnd = Process.Start(processStartInfo);
                FrontEnd.EnableRaisingEvents = true;


            }
            catch (Exception ex)
            {
                Console.WriteLine("Fatal Error" + ex.Message);

            }
        }
        //waits untill the front end has been opened before continuing then switch to fullscreen
        void WaitForFEStart()
        {
            Console.WriteLine("Opening Arcade....");
            FEStartup();
            Process frontOpened;
            do
            {
                frontOpened = Process.GetProcessesByName("FrontEnd").FirstOrDefault();
                Console.WriteLine("Waiting For FrontEnd");
                Thread.Sleep(500);
            } while (frontOpened.MainWindowHandle == IntPtr.Zero);//wait for window to be made
            Console.WriteLine("Success!");
            RunInteropService.Instance.SetFullScreen();
        }
        //wait until the frontend windows foucs has been set
        void WaitForWindowFocus()
        {
            var Procs = Process.GetProcessesByName("FrontEnd");
            if (Procs.FirstOrDefault() != null)
            {

                var TargetIntPtr = Procs.FirstOrDefault().MainWindowHandle;
                var CurrentIntPtr = RunInteropService.Instance.CurrentWindowsFocus();

                do
                {
                    TargetIntPtr = Procs.FirstOrDefault().MainWindowHandle;
                    CurrentIntPtr = RunInteropService.Instance.CurrentWindowsFocus();
                    RunInteropService.Instance.SetWindowFocus(RunInteropService.WindowSelection.FrontEnd);
                    Console.WriteLine("Waiting For Window Focus");
                    Thread.Sleep(500);
                } while (CurrentIntPtr != TargetIntPtr);//do while fe is not the focus

                Console.WriteLine("Green Across the board!");

                stateManager.SetState<Waiting>();

            }
        }


        public void UpdateState()
        {
            //check if proc opened if not
            Console.WriteLine("State : OpeningFE");
            WaitForFEStart();
            WaitForWindowFocus();

        }

        public void Init()
        {
            throw new NotImplementedException();
        }

        public void Init(string Value)
        {
            throw new NotImplementedException();
        }
    }

}
