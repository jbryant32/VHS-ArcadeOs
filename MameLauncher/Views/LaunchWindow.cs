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
            var Lproc = Process.GetProcessesByName("LaunchPad").ToList();
            if (Lproc != null)
            {
                foreach (var proc in Lproc)
                {
                    proc.Kill();
                }
            }
            //start the launchpad process the base activateWindow will handle the rest
            if (Process.GetProcessesByName("LaunchPad").FirstOrDefault() == null)
            {
                var proc = Process.Start(@"C:\OS_AppFiles\VHSLaunchPad\LaunchPad.exe");

            }
            base.ActivateWindow();
        }
    }
}
