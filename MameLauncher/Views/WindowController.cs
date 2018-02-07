using MameLauncher.Tools;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MameLauncher.Views
{
    public partial class WindowController
    {
        public readonly string Name;
        private bool Active;
        private bool CurrentState;
        private bool PreviousState;
        public bool IsActive { get { return Active; } }
        public bool SetState { get { return Active; } set { Active = value; } }
        //TODO finish setting up exe's paths
        public string ExecutablesPath
        {
            get
            {
                if (Name != string.Empty)
                {
                    if (Name == "FrontEnd")
                    {
                        var path = string.Empty;
                        path = @"c:\Share\BuildFE\Debug\FrontEnd.exe";
#if LOCALDEBUG
                        path = @"C:\Users\jbryant64\source\repos\FrontEnd\FrontEnd\bin\x86\LocalDebug\FrontEnd.exe";
#endif
                        return path;
                    }
                    if (Name == "mame")
                    {
                        var path = string.Empty;
                        path = @"c:\mame";
#if LOCALDEBUG
                        path = @"d:\Mame";
#endif
                        return path;
                    }
                    if (Name == "LaunchPad")
                    {
                        var path = string.Empty;
#if LOCALDEBUG
                        path = @"d:\Share\BuildFE\Debug\FrontEnd.exe";
#endif
                        return path;
                    }

                    if (Name == "LoadScreen")
                    {
                        var path = string.Empty;
#if LOCALDEBUG
                        path = @"d:\Share\BuildFE\Debug\FrontEnd.exe";
#endif
                        return path;
                    }

                }
                return string.Empty;
            }
        }
        public IntPtr GetHwn { get { return Hwn; } }
        internal IntPtr Hwn;

        public event EventHandler WindowStateChange;

        public WindowController(string Name, bool Active, IntPtr hwn)
        {
            this.Name = Name;
            this.Active = Active;
            this.Hwn = hwn;
            CurrentState = false;
        }



       
        public virtual void ActivateWindow()
        {
            Active = true;
            PreviousState = CurrentState;
            CurrentState = IsActive;
            Cursor cursor = Cursors.None;

            do
            {

                Thread.Sleep(200);
                Console.WriteLine($"Active Window: {this.Name} handle Not Ready...");

            } while (Process.GetProcessesByName(Name).FirstOrDefault() == null);//wait untill process is created 


            do
            {
                Console.Clear();
                Thread.Sleep(100);
                Console.WriteLine($"Active Window: {this.Name} handle Not Ready...");
                if (Process.GetProcessesByName(Name).FirstOrDefault() != null)
                {
                    this.Hwn = Process.GetProcessesByName(Name).FirstOrDefault().MainWindowHandle;
                }
            } while (this.Hwn == IntPtr.Zero);//if active we should not have a 0x0000 pointer wait untill window is created 

            SetupWindow();//window is created to we can setup our default setting
        }


        private unsafe void SetupWindow()
        {
            SetForegroundWindow(Hwn);

            var WS_SETUP = new IntPtr((void*)(uint)WindowStyles.WS_SETUP);//convert to a ptr memmory address from the enum
            var process = Process.GetProcessesByName(Name).FirstOrDefault();

            SetWindowLongPtr(new HandleRef(process, Hwn), (int)WindowLongFlags.GWL_STYLE, WS_SETUP);//set the windows style no bordrs 
            SetWindowPos(Hwn, (int)SpecialWindowHandles.HWND_TOPMOST, 0, 0, 800, 600, (int)SetWindowPosFlags.SWP_FRAMECHANGED | (int)SetWindowPosFlags.SWP_SHOWWINDOW);
            ShowWindowAsync(Hwn, (int)ShowWindowCommands.Show);

        }

        public void Update()
        {
            //fires off event if our window state has change if there is an need to do something when this happens
            //the event is available and tested working
            if (PreviousState != CurrentState)
            {
                if (CurrentState == true)
                {
                    if (WindowStateChange != null)
                    {
                        WindowStateChange(this, new EventArgs());
                    }
                }
                PreviousState = CurrentState;
            }
            if (Process.GetProcessesByName(Name).FirstOrDefault() == null)
            {
                if (this.IsActive)
                {
                    //fail safe just incase the window crashes with and active status this wiil deactivate the window
                    this.Active = false;
                }

            }
            if (this.IsActive)
            {
                EnableWindow(Hwn, true);

            }
            else
            {
                EnableWindow(Hwn, false);

            }
        }
    }
}
