using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MameLauncher.Views
{
    public class LoadingWindow : WindowController
    {
        public LoadingWindow(string Name, bool Active, IntPtr hwn) : base(Name, Active, hwn)
        {

        }

        public override void ActivateWindow()
        {
            if (Process.GetProcessesByName("LoadingWindow").FirstOrDefault() == null)
            { var proc = Process.Start(@"C:\Share\BuildLoading\Release\LoadingWindow.exe"); }
           
                base.ActivateWindow();
        }
    }
}
