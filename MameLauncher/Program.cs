using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading;

namespace MameLauncher
{
    class Program
    {
        
        static void Main(string[] args)
        {
#if DEBUG || RELEASE

              //kills of explorer on loading 
            var Eproc = Process.GetProcessesByName("explorer").FirstOrDefault();
            if (Eproc != null)
            {
                Eproc.Kill();
            }
            RunInteropService.Instance.ChangeDisplay(800, 600, 32);
#endif
           
            Console.Title = "Arcade Launcher";
            Thread.Sleep(10000);
            StateManager.Instance.Run();

        }

    }

}
