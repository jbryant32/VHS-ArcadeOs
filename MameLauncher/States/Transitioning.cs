using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MameLauncher.States
{
    public class Transitioning : IState
    {
        StateManager stateManager;
        public string TransTo { get; set; }
        bool WindowReady { get; set; }
        public Transitioning(StateManager state)
        {
            stateManager = state;
        }



        public void Init()
        {
            throw new NotImplementedException();
        }

        public void Init(string Value)
        {
            stateManager.SetState<Transitioning>();
            Console.WriteLine($"Transitioning to {Value}");
            TransTo = Value;
            //activate the loading screen until the other tranistion to app is ready
            var Ready = stateManager.SetActiveWindow("LoadingWindow");
            Thread.Sleep(2000);
            if (TransTo == "FrontEnd")
            {
                WindowReady = stateManager.SetActiveWindow("FrontEnd");
                stateManager.SetState<Waiting>();
            }
            if (TransTo == "mame")
            {
                WindowReady = stateManager.SetActiveWindow("mame");
                stateManager.SetState<Waiting>();
            }
            if (TransTo == "LaunchPad")
            {
                WindowReady = stateManager.SetActiveWindow("LaunchPad");
                stateManager.SetState<Waiting>();
            }
            stateManager.SetState<Waiting>();
        }

        public void UpdateState()
        {
             
        }
    }
}
