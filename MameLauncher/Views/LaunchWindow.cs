using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MameLauncher.Views
{
    public class LaunchWindow : WindowController
    {
        public LaunchWindow(string Name, bool Active, IntPtr hwn) : base(Name, Active, hwn)
        {
        }

        public override void ActivateWindow()
        {
            if (Process.GetProcessesByName("LaunchPad").FirstOrDefault() == null)
            {
                var proc = Process.Start(@"C:\Share\BuildLaunchPad\bin\Debug\LaunchPad.exe");
                
            }
            base.ActivateWindow();
        }
    }
}
