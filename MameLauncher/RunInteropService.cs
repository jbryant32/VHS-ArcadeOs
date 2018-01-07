using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
namespace MameLauncher
{
    public  partial class RunInteropService
    {

        static RunInteropService _instance;
        public static RunInteropService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new RunInteropService();
                    return _instance;
                }
                else { return _instance; }
            }
        }
        [Flags()]
        private enum WindowStyles : uint
        {
            WS_EX_ACCEPTFILES = 0x00000010,
            WS_EX_APPWINDOW = 0x00040000,
            WS_EX_CLIENTEDGE = 0x00000200,
            WS_EX_COMPOSITED = 0x02000000,
            WS_EX_CONTEXTHELP = 0x00000400,
            WS_EX_CONTROLPARENT = 0x00010000,
            WS_EX_DLGMODALFRAME = 0x00000001,
            WS_EX_LAYERED = 0x00080000,
            WS_EX_LAYOUTRTL = 0x00400000,
            WS_EX_LEFT = 0x00000000,
            WS_EX_LEFTSCROLLBAR = 0x00004000,
            WS_EX_LTRREADING = 0x00000000,
            WS_EX_MDICHILD = 0x00000040,
            WS_EX_NOACTIVATE = 0x08000000,
            WS_EX_NOINHERITLAYOUT = 0x00100000,
            WS_EX_NOPARENTNOTIFY = 0x00000004,
            WS_EX_OVERLAPPEDWINDOW = WS_EX_WINDOWEDGE | WS_EX_CLIENTEDGE,
            WS_EX_PALETTEWINDOW = WS_EX_WINDOWEDGE | WS_EX_TOOLWINDOW | WS_EX_TOPMOST,
            WS_EX_RIGHT = 0x00001000,
            WS_EX_RIGHTSCROLLBAR = 0x00000000,
            WS_EX_RTLREADING = 0x00002000,
            WS_EX_STATICEDGE = 0x00020000,
            WS_EX_TOOLWINDOW = 0x00000080,
            WS_EX_TOPMOST = 0x00000008,
            WS_EX_TRANSPARENT = 0x00000020,
            WS_EX_WINDOWEDGE = 0x00000100,
            WS_BORDER = 0x800000,
            WS_CAPTION = 0xc00000,
            WS_CHILD = 0x40000000,
            WS_CLIPCHILDREN = 0x2000000,
            WS_CLIPSIBLINGS = 0x4000000,
            WS_DISABLED = 0x8000000,
            WS_DLGFRAME = 0x400000,
            WS_GROUP = 0x20000,
            WS_HSCROLL = 0x100000,
            WS_MAXIMIZE = 0x1000000,
            WS_MAXIMIZEBOX = 0x10000,
            WS_MINIMIZE = 0x20000000,
            WS_MINIMIZEBOX = 0x20000,
            WS_OVERLAPPED = 0x0,
            WS_OVERLAPPEDWINDOW = WS_OVERLAPPED | WS_CAPTION | WS_SYSMENU | WS_SIZEFRAME | WS_MINIMIZEBOX | WS_MAXIMIZEBOX,
            WS_POPUP = 0x80000000u,
            WS_POPUPWINDOW = WS_POPUP | WS_BORDER | WS_SYSMENU,
            WS_SIZEFRAME = 0x40000,
            WS_SYSMENU = 0x80000,
            WS_TABSTOP = 0x10000,
            WS_VISIBLE = 0x10000000,
            WS_VSCROLL = 0x200000,
            WS_SETUP = WS_POPUP | WS_SYSMENU | WS_CLIPCHILDREN | WS_CLIPSIBLINGS | WS_VISIBLE
        }
        enum WindowLongFlags : int
        {
            GWL_EXSTYLE = -20,
            GWLP_HINSTANCE = -6,
            GWLP_HWNDPARENT = -8,
            GWL_ID = -12,
            GWL_STYLE = -16,
            GWL_USERDATA = -21,
            GWL_WNDPROC = -4,
            DWLP_USER = 0x8,
            DWLP_MSGRESULT = 0x0,
            DWLP_DLGPROC = 0x4
        }
        public enum WindowSelection { MAME, FrontEnd }
        enum ShowWindowCommands
        {
            /// <summary>
            /// Hides the window and activates another window.
            /// </summary>
            Hide = 0,
            /// <summary>
            /// Activates and displays a window. If the window is minimized or 
            /// maximized, the system restores it to its original size and position.
            /// An application should specify this flag when displaying the window 
            /// for the first time.
            /// </summary>
            Normal = 1,
            /// <summary>
            /// Activates the window and displays it as a minimized window.
            /// </summary>
            ShowMinimized = 2,
            /// <summary>
            /// Maximizes the specified window.
            /// </summary>
            Maximize = 3, // is this the right value?
                          /// <summary>
                          /// Activates the window and displays it as a maximized window.
                          /// </summary>       
            ShowMaximized = 3,
            /// <summary>
            /// Displays a window in its most recent size and position. This value 
            /// is similar to <see cref="Win32.ShowWindowCommand.Normal"/>, except 
            /// the window is not activated.
            /// </summary>
            ShowNoActivate = 4,
            /// <summary>
            /// Activates the window and displays it in its current size and position. 
            /// </summary>
            Show = 5,
            /// <summary>
            /// Minimizes the specified window and activates the next top-level 
            /// window in the Z order.
            /// </summary>
            Minimize = 6,
            /// <summary>
            /// Displays the window as a minimized window. This value is similar to
            /// <see cref="Win32.ShowWindowCommand.ShowMinimized"/>, except the 
            /// window is not activated.
            /// </summary>
            ShowMinNoActive = 7,
            /// <summary>
            /// Displays the window in its current size and position. This value is 
            /// similar to <see cref="Win32.ShowWindowCommand.Show"/>, except the 
            /// window is not activated.
            /// </summary>
            ShowNA = 8,
            /// <summary>
            /// Activates and displays the window. If the window is minimized or 
            /// maximized, the system restores it to its original size and position. 
            /// An application should specify this flag when restoring a minimized window.
            /// </summary>
            Restore = 9,
            /// <summary>
            /// Sets the show state based on the SW_* value specified in the 
            /// STARTUPINFO structure passed to the CreateProcess function by the 
            /// program that started the application.
            /// </summary>
            ShowDefault = 10,
            /// <summary>
            ///  <b>Windows 2000/XP:</b> Minimizes a window, even if the thread 
            /// that owns the window is not responding. This flag should only be 
            /// used when minimizing windows from a different thread.
            /// </summary>
            ForceMinimize = 11
        }

        [DllImport("user32.dll")]
        static extern bool BlockInput(bool fBlockIt);

        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr SetFocus(IntPtr hWnd);

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        private static extern bool ShowWindowAsync(IntPtr hWnd, int nCmdShow);

        public static IntPtr SetWindowLongPtr(HandleRef hWnd, int nIndex, IntPtr dwNewLong)
        {
            if (IntPtr.Size == 8)
                return SetWindowLongPtr64(hWnd, nIndex, dwNewLong);
            else
                return new IntPtr(SetWindowLong32(hWnd, nIndex, dwNewLong.ToInt32()));
        }

        [DllImport("user32.dll", EntryPoint = "SetWindowLong")]
        private static extern int SetWindowLong32(HandleRef hWnd, int nIndex, int dwNewLong);

        [DllImport("user32.dll", EntryPoint = "SetWindowLongPtr")]
        private static extern IntPtr SetWindowLongPtr64(HandleRef hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll")]
        static extern bool AdjustWindowRect(ref Rect lpRect, uint dwStyle,
   bool bMenu, uint dwExStyle);

        [DllImport("user32.dll", SetLastError = true)]
        internal static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);



        public bool ShowWindow(WindowSelection windowSelection)
        {
            bool shown = false;
            switch (windowSelection)
            {
                case WindowSelection.MAME:
                    var procM = Process.GetProcessesByName("mame").FirstOrDefault();
                    if (procM!=null)
                    {
                        IntPtr intPtrM = procM.MainWindowHandle;
                        shown = ShowWindowAsync(intPtrM, (int)ShowWindowCommands.Restore); 
                    }
                    break;
                case WindowSelection.FrontEnd:
                    var procFE = Process.GetProcessesByName("FrontEnd").FirstOrDefault();
                    if (procFE!=null)
                    {
                        IntPtr intPtrFE = procFE.MainWindowHandle;
                        shown = ShowWindowAsync(intPtrFE, (int)ShowWindowCommands.Restore); 
                    }
                    break;
                default:
                    break;
            }
            return shown;
        }

        public IntPtr CurrentWindowsFocus()
        {
            var CurrentWindowfocus = GetForegroundWindow();

            return CurrentWindowfocus;
        }
        public bool SetForeGroundWindw(WindowSelection windowSelection)
        {
            switch (windowSelection)
            {
                case WindowSelection.MAME:
                    var procM = Process.GetProcessesByName("mame").FirstOrDefault();
                    IntPtr intPtrM = IntPtr.Zero;
                    if (procM != null)
                    {
                        intPtrM = procM.MainWindowHandle;

                    }
                    return SetForegroundWindow(intPtrM);
                case WindowSelection.FrontEnd:
                    var procFE = Process.GetProcessesByName("FrontEnd").FirstOrDefault();
                    IntPtr intPtrFE = IntPtr.Zero;
                    if (procFE!=null)
                    {
                        intPtrFE = procFE.MainWindowHandle;
                    }
                    return SetForegroundWindow(intPtrFE);
                default:
                    break;
            }

            return false;
        }
        public void SetWindowFocus(WindowSelection windowSelection)
        {
            switch (windowSelection)
            {
                case WindowSelection.MAME:
                    var MameProc = Process.GetProcessesByName("mame").FirstOrDefault();
                    if (MameProc != null)
                    {
                        IntPtr intPtrM = MameProc.MainWindowHandle;
                        SetFocus(intPtrM);
                    }

                    break;
                case WindowSelection.FrontEnd:

                    var FeProc = Process.GetProcessesByName("FrontEnd").FirstOrDefault();
                    if (FeProc != null)
                    {
                        IntPtr intPtrFE = FeProc.MainWindowHandle;
                        SetFocus(intPtrFE);
                    }
                    break;
                default:
                    break;
            }
        }
        public bool BlockUserInPut(bool block)
        {
           return BlockInput(block);
        }

        public unsafe void SetFullScreen()
        {
            GetCurrentDisplay();
            var win = Process.GetProcessesByName("FrontEnd");
            if (win != null)
            {
                var intPtr = win.FirstOrDefault().MainWindowHandle;

                var WS_SETUP = new IntPtr((void*)(uint)WindowStyles.WS_SETUP);//convert to a ptr memmory address

                SetWindowLongPtr(new HandleRef(win, intPtr), (int)WindowLongFlags.GWL_STYLE, WS_SETUP);//set the windows style no bordrs and stuff

                

                Thread.Sleep(500);

                MoveWindow(intPtr, 0, 0, 800, 600, true);//change the size of the window
            }
            ChangeDisplay(800, 600, 32);//change the display size to fit the window
        }
    }
}
