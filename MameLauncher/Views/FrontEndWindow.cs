using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcadeOScfg;
namespace MameLauncher.Views
{
    public class FrontEndWindow : WindowController
    {
        Cfg config;
        public FrontEndWindow(string Name, bool Active, IntPtr hwn) : base(Name, Active, hwn)
        {
            config = StateManager.AppConfig;
        }

        public override void ActivateWindow()
        {
            //check if prcoess running already 
            var Start = new ProcessStartInfo("cmd.exe");
            Start.WorkingDirectory = @"c:\OS_AppFiles\VHSFrontEnd";
            Start.Arguments = @"/c cd c:\OS_AppFiles\VHSFrontEnd";
            Start.Arguments = @"/c FrontEnd.exe";
            Start.CreateNoWindow = true;

            var FeProc = Process.GetProcessesByName(Name).FirstOrDefault();
            if (FeProc == null)
            {
                //if not running then start up
                var proc = Process.Start(Start);

            }
            else
            {
                //if runnin kill the start moving already running FE to focus is causing an issue
                FeProc.Kill();
                var proc = Process.Start(Start);
            }
            base.ActivateWindow();
        }
    }
}
