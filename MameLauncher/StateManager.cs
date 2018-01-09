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
        IState OpenLaunchPad;
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

        static List<WindowController> Windows = new List<WindowController>() {
            { new WindowController("mame",false,IntPtr.Zero) },
            { new WindowController("FrontEnd",false,IntPtr.Zero) },
            { new WindowController("LaunchPad",false,IntPtr.Zero)}
        };
        WindowController CurrentWindow;
        WindowController PreviousWindow;


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
            OpenLaunchPad = new OpenLaunchPad(this);
            SetState<OpenLaunchPad>();


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
            if (typeof(T) == typeof(OpenLaunchPad))
            {
                OpenLaunchPad.Init();
                CurrentState = OpenLaunchPad;
            }
        }

        public void SetActiveWindow(string name)
        {
            var WindowToActivate = Windows.Where((wind) => { return wind.Name == name; }).FirstOrDefault();
            WindowToActivate.Active = true;
            CurrentWindow = WindowToActivate;
            if (PreviousWindow != CurrentWindow)
            {
                PreviousWindow = CurrentWindow;
                //deactive all windows that arent now the newly set window
                foreach (var wind in Windows)
                {
                    if (wind.Name != CurrentWindow.Name)
                    {
                        wind.Active = false;
                    }
                }
            }
            CurrentWindow.SetPtr(Process.GetProcessesByName(name).FirstOrDefault().MainWindowHandle);
            Thread.Sleep(200);
            CurrentWindow.SetupWindow();
        }
        public void AssignWindowPtr(string name, IntPtr Hwnd)
        {

        }

        void UpdateWindow()
        {
           
        }

        public void Run()
        {

            do
            {
                foreach (var wind in Windows)
                {
                    wind.Update();
                }
                CurrentState.UpdateState();

                Thread.Sleep(500);
            } while (true);
        }
    }

}
