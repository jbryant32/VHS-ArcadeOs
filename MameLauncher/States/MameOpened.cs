using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MameLauncher.States
{
    public class MameOpened : IState
    {
        StateManager stateManager;
        public MameOpened(StateManager state)
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
            //check for mame closure here 
            var Mame = Process.GetProcessesByName("mame").FirstOrDefault();
            if (Mame == null)
            {
                stateManager.SetState<MameExited>();
            }
            Console.WriteLine("State :mameOpened");
        }
    }
}
