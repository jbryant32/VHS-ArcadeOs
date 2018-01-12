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
            Console.Title = "Arcade Launcher";
           RunInteropService.Instance.ChangeDisplay(800, 600, 32);
           StateManager.Instance.Run();
          
        }
        
    }
}
