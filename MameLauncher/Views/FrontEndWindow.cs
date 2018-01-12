using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MameLauncher.Views
{
    public class FrontEndWindow : WindowController
    {
        public FrontEndWindow(string Name, bool Active, IntPtr hwn) : base(Name, Active, hwn)
        {
        }

        public override void ActivateWindow()
        {
            var ProcCount = Process.GetProcessesByName(Name).FirstOrDefault();
            if (ProcCount == null)
            {
                var proc = Process.Start(@"c:\Share\BuildFE\Debug\FrontEnd.exe");
            }
            
            base.ActivateWindow();
        }
    }
}
