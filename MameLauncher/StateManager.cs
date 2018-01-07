using MameLauncher.States;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MameLauncher
{
    public class StateManager
    {
        IState FeExited;
        IState OpeningFE;
        IState FEOpened;
        IState Waiting;
        IState GameSelected;
        IState MameOpened;
        IState MameExited;
        IState RestoreFE;
        IState FeExitedMameRunning;
        IState FeExitedMameNotRunning;
        static StateManager _instance;
        public static StateManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new StateManager();
                    return _instance;
                }
                return _instance;
            }
        }

        public IState CurrentState { get; private set; }

        public StateManager()
        {
            OpeningFE = new OpeningFE(this);
            FeExited = new FrontEndExited(this);
            FEOpened = new FEOpened(this);
            Waiting = new Waiting(this);
            GameSelected = new GameSelected(this);
            MameExited = new MameExited(this);
            MameOpened = new MameOpened(this);
            RestoreFE = new RestoreFE(this);
            FeExitedMameNotRunning = new FeExitedMameNotRunning(this);
            FeExitedMameRunning = new FeExitedMameRunning(this);
            CurrentState = new OpeningFE(this);
        }

        public void SetState<T>()
        {
            if (typeof(T) == typeof(OpeningFE))
            {
                CurrentState = OpeningFE;
            }
            if (typeof(T) == typeof(FEOpened))
            {
                CurrentState = FEOpened;
            }
            if (typeof(T) == typeof(Waiting))
            {
                Waiting.Init();
                CurrentState = Waiting;

            }
            if (typeof(T) == typeof(MameOpened))
            {
                CurrentState = MameOpened;
            }
            if (typeof(T) == typeof(MameExited))
            {
                CurrentState = MameExited;
            }
            if (typeof(T) == typeof(RestoreFE))
            {
                CurrentState = RestoreFE;
            }

            if (typeof(T) == typeof(FeExitedMameRunning))
            {
                CurrentState = FeExitedMameRunning;
            }

            if (typeof(T) == typeof(FeExitedMameNotRunning))
            {
                CurrentState = FeExitedMameNotRunning;
            }
            if (typeof(T) == typeof(GameSelected))
            {
                GameSelected.Init();
                CurrentState = GameSelected;
            }
        }


        public void Run()
        {
           
            do
            {
                CurrentState.UpdateState();
               
                Thread.Sleep(500);
            } while (true);
        }
    }

}
