using MameLauncher.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
   
        public void UpdateState()
        {
            Console.WriteLine("State : OpeningFE");
            var Fproc = Process.GetProcessesByName("FrontEnd").FirstOrDefault();
            if (Fproc!= null)
            {
                if (Fproc.MainWindowHandle != IntPtr.Zero)
                {
                    stateManager.SetState<Waiting>();
                }
            }
        }

        public void Init()
        {

            stateManager.SetTransition("FrontEnd");
           
        }

        public void Init(string Value)
        {
            throw new NotImplementedException();
        }
    }

}
