using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MameLauncher.States
{
    public class FrontEndExited : IState
    {
        StateManager StateManager;
        public FrontEndExited(StateManager stateManager)
        {
            this.StateManager = stateManager;
        }
        void RunExitStrat( )
        {
         
        }
        public void UpdateState()
        {
            Console.WriteLine("FrontEnd Exited");
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
