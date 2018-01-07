using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MameLauncher
{

    public class WindowSwitching
    {
        private static WindowSwitching _instance;
        public static WindowSwitching Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new WindowSwitching();
                    return _instance;
                }
                else
                {
                    return _instance;
                }
            }
        }

        enum Switching
        {
            ToFrontEnd,
            ToMame
        }
        enum Window
        {
            FrontEnd,
            Mame
        }
        Switching switching;
        Switching CurrntWindow;
        Switching PreviousWindow;
        bool WeSwitching;
        private const int SW_SHOWNORMAL = 1;
        private const int SW_SHOWMINIMIZED = 2;
        private const int SW_SHOWMAXIMIZED = 3;

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        public void Update()
        {
            var mame = Process.GetProcessesByName("mame").FirstOrDefault();
            var frontend = Process.GetProcessesByName("FrontEnd").FirstOrDefault();
            
        }
    }
}
