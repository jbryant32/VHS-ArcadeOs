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
        static string ExitData;
        static string ErrorData;


        static void Main(string[] args)
        {
            Console.Title = "Arcade Launcher";
            StateManager.Instance.Run();
        }
    }
}
