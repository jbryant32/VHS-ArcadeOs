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
            // start the loadwindow process this is used to cover over the workings in the background like window setups
            if (Process.GetProcessesByName("LoadingWindow").FirstOrDefault() == null)
            {
#if DEBUG || RELEASE
                var proc = Process.Start(@"C:\OS_AppFiles\VHSLoadingScreen\LoadingWindow.exe");

#endif
#if LOCALDEBUG
                var proc = Process.Start(@"C:\OS_AppFiles\VHSLoadingScreen\LoadingWindow.exe");
#endif
            }

            base.ActivateWindow();
        }
    }
}
