using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MameLauncher.States
{
    public class FeExitedMameNotRunning : IState
    {
        StateManager stateManager;
        public FeExitedMameNotRunning(StateManager state)
        {
            this.stateManager = state;
        }

        public void Init()
        {
            throw new NotImplementedException();
        }

        public void Init(string Value)
        {
            throw new NotImplementedException();
        }

        public void UpdateState()
        {
            var FProc = Process.GetProcessesByName("FrontEnd.exe") ;

            if (FProc.Count()==0)
            {
                Console.WriteLine("fe Exited Mame Not Running");
                var Dir = @"C:\Share\BuildFE\Debug\FrontEnd.exe";
                var Fproc = Process.Start(Dir);
                do
                {

                } while (Fproc.MainWindowHandle==IntPtr.Zero);
                RunInteropService.Instance.SetFullScreen();
                stateManager.SetState<Waiting>();
            }
        }
    }
}
