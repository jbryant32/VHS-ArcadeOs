using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MameLauncher.States
{
    public class FeExitedMameRunning : IState
    {
        StateManager stateManager;
        public FeExitedMameRunning(StateManager state)
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
            Console.WriteLine("fe Exited Mame Not Running");
        }
    }
}
