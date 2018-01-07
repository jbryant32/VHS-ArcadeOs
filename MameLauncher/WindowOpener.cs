using MameLauncher.States;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MameLauncher
{
    //launches Window Proces
    public class WindowOpener
    {
        private static WindowOpener _instance;
        public static WindowOpener Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new WindowOpener();
                    return _instance;
                }
                return _instance;
            }
        }
        public event EventHandler FrontEndExited;

        int CheckProcessRunning(string Proc)
        {
            var Count = Process.GetProcessesByName(Proc);
            return Count.Count(); ;

        }
        bool StartMame(string Game)
        {
            try
            {
                var proc = Process.Start("cmd.exe", @"d:\mame\mame.exe");
                return true;
            }
            catch (Exception)
            {

                return false;
            }


        }
    
        private void FrontEnd_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            Console.WriteLine("Error with FrontEnd");
        }
        private void FrontEnd_Exited(object sender, EventArgs e)
        {
            RunInteropService.Instance.SetDisplayBackToOriginal();
            Console.WriteLine("Front End Closed");
        }
        public bool OpenMame(string game)
        {
            if (CheckProcessRunning("mame") == 0)
            {
                return StartMame("alien");
            }

            return false;
        }
       

        public bool MameChecked()
        {
            if (CheckProcessRunning("mame")==1)
            {
                return true;
            }

            return false;
        }


       

        
    }
}
