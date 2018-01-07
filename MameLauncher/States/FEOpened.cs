using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MameLauncher.States
{
    public class FEOpened:IState
    {
        StateManager stateManager;
        public FEOpened(StateManager stateManager)
        {
            this.stateManager = stateManager;
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
            Console.WriteLine("Setting FrontEnd Focus");

        

           
            Console.WriteLine("Set");

          
        }
    }
}
