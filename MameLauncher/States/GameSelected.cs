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
    public class GameSelected : IState
    {
        StateManager stateManager;
        string cmd;
        public GameSelected(StateManager state)
        {
            stateManager = state;
        }

        public void Init()
        {
            var FilePath = @"C:\FrontEndAppFiles\mameCmd.vhs";

            WaitForFileWriteCompletion();//wait for file to close
            using (var read = File.OpenText(FilePath))//read the  cmd to be sent to mame from the FE game to be launched
            {
                cmd = read.ReadToEnd();
                Console.WriteLine("Command" + cmd);
                read.Close();
            }

            OpenMame(cmd);

            stateManager.SetState<Waiting>();
            Console.WriteLine("Game Opened");
        }

        public void Init(string Value)
        {

        }

        public void UpdateState()
        {
            Console.WriteLine("Game Selected State");
            WaitForMameToOpen();
        }

        //checks if mame is running if not starts mame process
        void OpenMame(string args)
        {
            //is mame open if move focuse off fe and keep checking if focus changew
            var mameProc = Process.GetProcessesByName("mame").FirstOrDefault();
            if (mameProc == null)//if mame is not already running start mame
            {
                Console.WriteLine("Starting Game");
                ProcessStartInfo startInfo = new ProcessStartInfo("cmd");
                startInfo.RedirectStandardInput = true;
                startInfo.UseShellExecute = false;
                var proc = Process.Start(startInfo);
                proc.StandardInput.WriteLine(args);
            }

        }

        void WaitForMameToOpen()
        {
            var mameProc = Process.GetProcessesByName("mame").FirstOrDefault();
            var TimeOut = 0;
            do
            {
                mameProc = Process.GetProcessesByName("mame").FirstOrDefault();
                Console.WriteLine("Waiting For Mame to Start");
                if (mameProc != null)
                {
                    if (mameProc.HasExited)
                    {
                        Console.WriteLine("Mame Opened And Closed No Game :(");
                        stateManager.SetState<Waiting>();
                    }
                }
            } while (mameProc == null);

            if (mameProc != null)
            {
                //wating for the window to open up :)
                do
                {
                    Console.WriteLine("Waiting For Mame to Open Up!");
                    mameProc = Process.GetProcessesByName("mame").FirstOrDefault();
                    if (mameProc.HasExited)//mame no game found
                    {
                        Console.WriteLine("Mame Opened And Closed No Game :(");
                        stateManager.SetState<Waiting>();
                    }
                } while (mameProc.MainWindowHandle == IntPtr.Zero);


            }

            Console.WriteLine("Games Away!!");
            stateManager.SetState<Waiting>();
        }

        //Blocks the current thread untill the file has finished writing 
        void WaitForFileWriteCompletion()
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
    }
}
