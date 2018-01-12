using MameLauncher.States;
using MameLauncher.Views;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        IState LaunchPadFocused;
        IState FeExitedMameRunning;
        IState FeExitedMameNotRunning;
        IState Transition;


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

        public List<WindowController> Windows = new List<WindowController>() {
            { new MameWindow("mame",false,IntPtr.Zero) },
            { new FrontEndWindow("FrontEnd",false,IntPtr.Zero) },
            { new LaunchWindow("LaunchPad",false,IntPtr.Zero)},
            { new LoadingWindow("LoadingWindow",false,IntPtr.Zero)},
        };
        WindowController CurrentActiveWindow;
        WindowController PreviouAcivesWindow;


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
            LaunchPadFocused = new LaunchPadFocused(this);
            Transition = new Transitioning(this);
            this.SetTransition("LaunchPad");

        }
        public void SetTransition(string TransitioningTo)
        {
            this.Transition.Init(TransitioningTo);//start transition
         

        }
        public void SetState<T>()
        {
            if (typeof(T) == typeof(OpeningFE))
            {
                OpeningFE.Init();
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
            if (typeof(T) == typeof(LaunchPadFocused))
            {
                LaunchPadFocused.Init();
                CurrentState = LaunchPadFocused;
            }
        }

        //gets the request window from the list of windows creates its process if not already created and acitves its window
        public bool SetActiveWindow(string name)
        {
            var WindowToActivate = Windows.Where((wind) => { return wind.Name == name; }).FirstOrDefault();

            WindowToActivate.ActivateWindow();//note there is a loop here that will block the thread untill window is created

            CurrentActiveWindow = WindowToActivate;
            if (PreviouAcivesWindow != CurrentActiveWindow)
            {
                PreviouAcivesWindow = CurrentActiveWindow;
                //deactive all windows that arent now the newly set window only one windo active at a time
                foreach (var wind in Windows)
                {
                    if (wind.Name != CurrentActiveWindow.Name)
                    {
                        wind.SetState = false;
                    }
                }
            }
            Thread.Sleep(200);
            return true;
        }
        public void AssignWindowPtr(string name, IntPtr Hwnd)
        {

        }

        void UpdateWindows()
        {
            foreach (var wind in Windows)
            {
                wind.Update();
            }
        }

        public void Run()
        {

            do
            {

                this.UpdateWindows();

                CurrentState.UpdateState();

                Thread.Sleep(500);
            } while (true);
        }
    }

}
