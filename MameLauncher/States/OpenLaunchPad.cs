using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MameLauncher.States
{
    public class OpenLaunchPad : IState
    {
        StateManager stateManager;
        MyFileWatcher watcher;
        public OpenLaunchPad(StateManager state)
        {
            this.stateManager = state;
            this.watcher = new MyFileWatcher(@"C:\FrontEndAppFiles\Shell.vhs");
            this.watcher.FileChanged += Watcher_FileChanged;
        }

        private void Watcher_FileChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Launching Arcade...");
            stateManager.SetState<OpeningFE>();
         }

        public void Init()
        {
            var proc = Process.GetProcessesByName("LaunchPad").FirstOrDefault();
            if (proc == null)
            {
                proc =  Process.Start(@"C:\Share\BuildLaunchPad\bin\Debug\LaunchPad.exe");
 
               
            }

            do
            {
             
                Thread.Sleep(500);
            } while (proc == null);//wait for process to start


            do
            {
                Console.WriteLine("Launch Pad Opening");
                Thread.Sleep(500);
            } while (proc.MainWindowHandle == IntPtr.Zero);//wait for window creation
            stateManager.SetActiveWindow(proc.ProcessName);
        }

        public void Init(string Value)
        {
            throw new NotImplementedException();
        }

        public void UpdateState()
        {
            Console.Clear();
            Console.WriteLine("Waiting In LaunchPad");
            watcher.Update();
        }
    }
}
