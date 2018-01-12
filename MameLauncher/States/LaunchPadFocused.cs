using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MameLauncher.States
{
    public class LaunchPadFocused : IState
    {
        StateManager stateManager;
        MyFileWatcher watcher;
        public LaunchPadFocused(StateManager state)
        {
            this.stateManager = state;
            this.watcher = new MyFileWatcher(@"C:\FrontEndAppFiles\Shell.vhs");
            this.watcher.FileChanged += Watcher_FileChanged;
        }

        private void Watcher_FileChanged(object sender, EventArgs e)
        {
            Console.WriteLine("Launching Arcade...");
            stateManager.SetTransition("FrontEnd");
         }

        public void Init()
        {
            
         
        }

        public void Init(string Value)
        {
            throw new NotImplementedException();
        }

        public void UpdateState()
        {
            Console.Clear();
            Console.WriteLine("Waiting In LaunchPad");
            watcher.Update();
        }
    }
}
