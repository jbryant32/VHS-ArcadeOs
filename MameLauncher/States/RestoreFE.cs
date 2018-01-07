using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MameLauncher.States
{
    public class RestoreFE : IState
    {
        StateManager stateManager;
        public RestoreFE(StateManager stateM)
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
            Console.WriteLine("State :Restoring FE");
            //RunInteropService.Instance.SetWindowFocus(RunInteropService.WindowSelection.FrontEnd);
            //Thread.Sleep(2000);
            //var foreGroundSet = RunInteropService.Instance.SetForeGroundWindw(RunInteropService.WindowSelection.FrontEnd);
            //Thread.Sleep(1000);
            //RunInteropService.Instance.ShowWindow(RunInteropService.WindowSelection.FrontEnd);

        }
    }
}
