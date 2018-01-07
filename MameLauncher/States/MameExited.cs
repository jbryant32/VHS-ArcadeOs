using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MameLauncher.States
{
    public class MameExited : IState
    {
        StateManager stateManager;
        public MameExited(StateManager stateM)
        {
            this.stateManager = stateM;
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
            //reopen FE
            Console.WriteLine("State : Mame Exited");
           
            RunInteropService.Instance.SetWindowFocus(RunInteropService.WindowSelection.FrontEnd);
            
            var ForeGroundSet = RunInteropService.Instance.SetForeGroundWindw(RunInteropService.WindowSelection.FrontEnd);
            Thread.Sleep(500);
            var RestoreWind = RunInteropService.Instance.ShowWindow(RunInteropService.WindowSelection.FrontEnd);

            RunInteropService.Instance.SetFullScreen();
            stateManager.SetState<Waiting>();
        }
    }
}
 
 
