using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MameLauncher.States
{
    public class Waiting : IState
    {
        StateManager StateManager;
        string mameCmd { get; set; }
        bool FileReady { get; set; }
        DateTime CurrentFileWriteTime;
        DateTime PreviouseFileWriteTime;
        public Waiting(StateManager stateM)
        {
            StateManager = stateM;
            ArcadeEventsHandler.Instance.MameClosed += Instance_MameClosed;
            ArcadeEventsHandler.Instance.GameSelected += Instance_GameSelected;
            ArcadeEventsHandler.Instance.FrontEndCrashedMameRunning += Instance_FrontEndExitedMameRunning;
            ArcadeEventsHandler.Instance.FrontEndCrashedNoMameRunning += Instance_FrontEndExitedNoMameRunning;
            ArcadeEventsHandler.Instance.FrontEndExitedIntentional += Instance_FrontEndExitedIntentional;
            ArcadeEventsHandler.Instance.StartAracde += Instance_StartAracde;
        }

        private void Instance_StartAracde(object sender, EventArgs e)
        {
            Console.WriteLine("Start Arcade");
            StateManager.SetTransition("FrontEnd");
        }

        private void Instance_FrontEndExitedIntentional(object sender, EventArgs e)
        {
            Console.WriteLine("FrontEnd Closed Intentional");
        }

        private void Instance_FrontEndExitedNoMameRunning(object sender, EventArgs e)
        {
            Console.WriteLine("FrontEnd Closed Mame Not Running....Shall We Try Again????");
        }

        private void Instance_FrontEndExitedMameRunning(object sender, EventArgs e)
        {
            Console.WriteLine("FrontEnd Closed While Mame Running :(");
        }

        private void Instance_GameSelected(object sender, EventArgs e)
        {
            StateManager.SetTransition("mame");
            Console.WriteLine("Game Selected!");
        }

        private void Instance_MameClosed(object sender, EventArgs e)
        {
            Console.WriteLine("mame closed!");
            try
            {
                var Mproc = Process.GetProcessesByName("mame").ToList();
                
               
                foreach (var proc in Mproc)
                {
                    proc.Kill();
                }
                StateManager.SetTransition("FontEnd");
                Console.WriteLine("why is mame still open  ? you said you quit ??");
            }
            catch (Exception)
            {


            }
            StateManager.SetTransition("FrontEnd");
        }

        public void Init()
        { //iniitalize the file watcher current state
            var Path = @"C:\OScfg\FrontEndAppFiles\mameCmd.vhs";
            PreviouseFileWriteTime = File.GetLastWriteTime(Path);
            CurrentFileWriteTime = File.GetLastWriteTime(Path);
        }

        public void Init(string Value)
        {
            throw new NotImplementedException();
        }
       
        public void UpdateState()
        {
            ArcadeEventsHandler.Instance.Update();

            Console.WriteLine("Waiting Room....");
            Thread.Sleep(500);
        }


    }
}
