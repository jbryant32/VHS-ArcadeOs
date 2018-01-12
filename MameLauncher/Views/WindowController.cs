using MameLauncher.Tools;
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
        private bool Active;
        private bool CurrentState;
        private bool PreviousState;
        public bool IsActive { get { return Active; } }
        public bool SetState { get { return Active; } set { Active = value; } }
        public IntPtr GetHwn { get { return Hwn; } }
        internal IntPtr Hwn;
        private Process process;
        public WindowController(string Name, bool Active, IntPtr hwn)
        {
            this.Name = Name;
            this.Active = Active;
            this.Hwn = hwn;
            this.process = Process.GetProcessesByName(Name).FirstOrDefault();
            CurrentState = false;
        }



        /// <summary>
        /// if the process is running window will have it intptr handle read
        /// </summary>
        /// <param name="nameOfAppToAcitvate"></param>
        public virtual void ActivateWindow()
        {
            PreviousState = CurrentState;
            Active = true;
            CurrentState = Active;

            do
            {

                Thread.Sleep(1000);
                Console.WriteLine($"Active Window: {this.Name} handle Not Ready...");

            } while (Process.GetProcessesByName(Name).FirstOrDefault() == null);


            do
            {
                Console.Clear();
                Thread.Sleep(100);
                Console.WriteLine($"Active Window: {this.Name} handle Not Ready...");
                if (Process.GetProcessesByName(Name).FirstOrDefault() != null)
                {
                    this.Hwn = Process.GetProcessesByName(Name).FirstOrDefault().MainWindowHandle;
                }
            } while (this.Hwn == IntPtr.Zero);//if active we should not have a 0x0000 pointer

            // SetupWindow();
        }


        private unsafe void SetupWindow()
        {
            var WS_SETUP = new IntPtr((void*)(uint)WindowStyles.WS_SETUP);//convert to a ptr memmory address from the enum


            SetWindowLongPtr(new HandleRef(process, Hwn), (int)WindowLongFlags.GWL_STYLE, WS_SETUP);//set the windows style no bordrs 
            SetWindowPos(Hwn, (int)SpecialWindowHandles.HWND_TOPMOST, 0, 0, 800, 600, (int)SetWindowPosFlags.SWP_FRAMECHANGED | (int)SetWindowPosFlags.SWP_SHOWWINDOW);

            //MoveWindow(Hwn, 0, 0, 800, 600, true);
        }

        public void Update()
        {

            if (PreviousState != CurrentState)
            {
                if (CurrentState == true)
                {
                    //bring window to front
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
        }
    }
}
