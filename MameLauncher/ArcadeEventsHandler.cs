using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MameLauncher
{
    public class WindowClosedArgs : EventArgs
    {
        string NameOfWindow { get; set; }
        public WindowClosedArgs(string nameOfwindow)
        {
            this.NameOfWindow = nameOfwindow;
        }
    }
    public class ArcadeEventsHandler
    {
        static ArcadeEventsHandler _instance;
        public static ArcadeEventsHandler Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new ArcadeEventsHandler();
                    return _instance;
                }
                return _instance;
            }
        }
        private string MameCmdFilePath { get { return @"C:\FrontEndAppFiles\mameCmd.vhs"; } }
        private string LauchFilePath { get { return @"C:\FrontEndAppFiles\Shell.vhs"; } }
        private MyFileWatcher WatchMameCmdFile;
        private MyFileWatcher WatchLaunchFile;
        public event EventHandler MameClosed;
        public event EventHandler GameSelected;
        public event EventHandler FrontEndCrashedMameRunning;
        public event EventHandler FrontEndCrashedNoMameRunning;
        public event EventHandler FrontEndExitedIntentional;
        public event EventHandler CurrentActiveWindowLostFocus;
        public event EventHandler StartAracde;
        public ArcadeEventsHandler()
        {
            WatchMameCmdFile = new MyFileWatcher(MameCmdFilePath);
            WatchLaunchFile = new MyFileWatcher(LauchFilePath);
            WatchMameCmdFile.FileChanged += WatchMameCmdFile_FileChanged;
            WatchLaunchFile.FileChanged += WatchLaunchFile_FileChanged;
        }

        private void WatchLaunchFile_FileChanged(object sender, EventArgs e)
        {
            if (StartAracde != null)
            {
                StartAracde(this, new EventArgs());
            }
          
        }

        private void WatchMameCmdFile_FileChanged(object sender, EventArgs e)
        {
            _GameSelected();
        }
        #region Mame
        bool MameCurrentOpenClosedState;
        bool MamePreviousOpenClosedState;
        private void _MameClosed()
        {

            var mWin = StateManager.Instance.Windows.Where((win) => { return win.Name == "mame"; }).Select((win) => { return win; }).First();
            MamePreviousOpenClosedState = MameCurrentOpenClosedState;//active 
            MameCurrentOpenClosedState = mWin.IsActive;//closed  inactive now

            if(!mWin.IsActive)//only check when mame is inactive and starts are different means we 
            if (MamePreviousOpenClosedState != MameCurrentOpenClosedState)//states changed we were active now were inactive
            {
                MamePreviousOpenClosedState = MameCurrentOpenClosedState;
                if (MameClosed != null)
                {
                    MameClosed(this, new EventArgs());//fire event
                }
            }
        }
        #endregion
        private void _GameSelected()
        {
            var mWin = StateManager.Instance.Windows.Where((win) => { return win.Name == "mame"; }).Select((win) => { return win; }).First();
            if (mWin.IsActive == false)
            {
                if (GameSelected != null)
                {
                    GameSelected(this, new EventArgs());
                }
            }
        }
        bool FrontEndCurrentOpenClosedState;
        bool FrontEndPreviousOpenClosedState;
        private void _FrontEndExited()
        {
            var FEwin = StateManager.Instance.Windows.Where((win) => { return win.Name == "FrontEnd"; }).FirstOrDefault();
            var FeProc = Process.GetProcessesByName("FrontEnd").FirstOrDefault();
            var Mproc = Process.GetProcessesByName("mame").FirstOrDefault();

            FrontEndPreviousOpenClosedState = FrontEndCurrentOpenClosedState;
            FrontEndCurrentOpenClosedState = FEwin.IsActive;
            if(!FEwin.IsActive)//this is here if we swtich back to launch pad after meaning to close the FE
            if (FrontEndPreviousOpenClosedState != FrontEndCurrentOpenClosedState)
            {
                FrontEndPreviousOpenClosedState = FrontEndCurrentOpenClosedState;
                FrontEndExitedIntentional(this, new EventArgs());
            }

            if (FEwin.IsActive && FeProc==null)
            {
                  //we crashed this window was not properly shut down
                    FrontEndCrashedNoMameRunning(this, new EventArgs());
                
               
            }
            if (FEwin.IsActive == false && Mproc !=null)//if crashed while mame is running
            {
                
                FrontEndCrashedMameRunning(this,new EventArgs());
            }

        }
        private void _CurrentActiveWindowLostFocus()
        {

        }

        public void Update()
        {
            WatchLaunchFile.Update();
            WatchMameCmdFile.Update();
                
            _MameClosed();
            _FrontEndExited();
            _CurrentActiveWindowLostFocus();
        }
    }
}
