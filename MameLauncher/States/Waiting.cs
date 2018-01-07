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
        event EventHandler MameCmdFileChanged;
        public Waiting(StateManager stateM)
        {
            StateManager = stateM;
            MameCmdFileChanged += OnMameCmdFileChanged;

        }
        public void Init()
        { //iniitalize the file watcher current state
            var Path = @"C:\FrontEndAppFiles\mameCmd.vhs";
            PreviouseFileWriteTime = File.GetLastWriteTime(Path);
            CurrentFileWriteTime = File.GetLastWriteTime(Path);
        }

        public void Init(string Value)
        {
            throw new NotImplementedException();
        }
        //this watches the mameCmd file that gets update once the user selects the game they want to launch
        private void OnMameCmdFileChanged(object sender, EventArgs e)
        {
            StateManager.SetState<GameSelected>();
            Console.WriteLine("Game Selected");
        }




        bool FileChanged()
        {

            Thread.Sleep(500);
            var Path = @"C:\FrontEndAppFiles\mameCmd.vhs";
            PreviouseFileWriteTime = CurrentFileWriteTime;
            CurrentFileWriteTime = File.GetLastWriteTime(Path);
            if (PreviouseFileWriteTime != CurrentFileWriteTime)
            {
                PreviouseFileWriteTime = CurrentFileWriteTime;
                Console.WriteLine("file change Time: " + CurrentFileWriteTime.ToLongTimeString());

                if (MameCmdFileChanged != null)
                    MameCmdFileChanged(this, new EventArgs());

                return true;
            }
            return false;
        }

        //This method checks if the file is being written to if not it opens the file and reads the command file
        void CheckFileReady()
        {
            StreamReader fileStream = null;
            FileInfo fileInfo = new FileInfo(@"C:\FrontEndAppFiles\mameCmd.vhs");
            do
            {
                try
                {
                    fileStream = fileInfo.OpenText();//checl

                    Console.WriteLine("File Ready");

                }
                catch (Exception ex)
                {

                    Console.WriteLine("File error: " + ex.Message);
                    fileStream = null;

                }

            } while (fileStream == null);
            fileStream.Close();
        }


        //watch for file change if so new game launch requested by front end

        //checks the frontends states and sets the appropriate state response
        void HandleFECrash()
        {
            var Mproc = Process.GetProcessesByName("mame");
            var FProc = Process.GetProcessesByName("FrontEnd");
            if (Mproc.Count() == 1)
            {
                if (FProc.Count() == 0)
                {
                    Console.WriteLine("I think Fe Crashed !");

                    StateManager.SetState<FeExitedMameRunning>();
                    return;
                }
            }

            if (Mproc.Count() == 0)
            {
                if (FProc.Count() == 0)
                {
                    Console.WriteLine("I think Fe Crashed!");
                    StateManager.SetState<FeExitedMameNotRunning>();
                }
            }

        }
        void HandleFEFocus()
        {
            RunInteropService.Instance.GetCurrentDisplay();
        }
        public void UpdateState()
        {

            FileChanged();
            HandleFECrash();
            Console.WriteLine("Waiting....");
            Thread.Sleep(500);
        }

         
    }
}
