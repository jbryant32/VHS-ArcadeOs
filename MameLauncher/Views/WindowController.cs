using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MameLauncher.Views
{
    public partial class WindowController
    {
        public readonly string Name;
        public bool Active;
        IntPtr Hwn;
        private Process process;
        public WindowController(string Name, bool Active, IntPtr hwn)
        {
            this.Name = Name;
            this.Active = Active;
            this.Hwn = hwn;
            this.process = Process.GetProcessesByName(Name).FirstOrDefault();
        }

        public void SetPtr(IntPtr intPtr)
        {
            this.Hwn = intPtr;
        }

        public unsafe void SetupWindow()
        {
            var WS_SETUP = new IntPtr((void*)(uint)WindowStyles.WS_SETUP);//convert to a ptr memmory address

            SetWindowLongPtr(new HandleRef(process, Hwn), (int)WindowLongFlags.GWL_STYLE, WS_SETUP);//set the windows style no bordrs 
            
            
            SetWindowPos(Hwn, (int)SpecialWindowHandles.HWND_TOP, 0, 0, 800, 600, (int)SetWindowPosFlags.SWP_FRAMECHANGED | (int)SetWindowPosFlags.SWP_SHOWWINDOW);
            //MoveWindow(Hwn, 0, 0, 800, 600, true);
        }

        public void Update()
        {
            if (this.Active)
            {
                do
                {
                    Console.Clear();
                    Console.WriteLine($"Active Window: {this.Name} handle Not Ready...");   
                } while (this.Hwn == IntPtr.Zero);
            }
        }
    }
}
