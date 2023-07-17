using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Drawing;

namespace AutomaticCapture.ETC
{
    public class CsWindowPosition
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string class_name, string window_name);

        [DllImport("user32.dll")]
        internal static extern bool GetWindowPlacement(IntPtr hWnd, ref WINDOWPLACEMENT lpwndpl);

        private string WinHandleName = null;

        internal struct WINDOWPLACEMENT
        {
            public int length;
            public int flags;
            public ShowWindowCommands showCmd;
            public System.Drawing.Point ptMinPosition;
            public System.Drawing.Point ptMaxPosition;
            public System.Drawing.Rectangle rcNormalPosition;
        }
         
        internal enum ShowWindowCommands : int
        {
            Hide = 0,
            Normal = 1,
            Minimized = 2,
            Maximized = 3,
        }
         
        internal enum WNDSTATE : int
        {
            SW_HIDE = 0,
            SW_SHOWNORMAL = 1,
            SW_NORMAL = 1,
            SW_SHOWMINIMIZED = 2,
            SW_MAXIMIZE = 3,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOW = 5,
            SW_MINIMIZE = 6,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_RESTORE = 9,
            SW_SHOWDEFAULT = 10,
            SW_MAX = 10
        }

        public IntPtr GetWinAscHandle()
        {
            this.InitWinHandleName();
            //return FindWindow(null, "제목 없음 - Windows 메모장");
            //return FindWindow(null, "선우정아 - 도망가자 [유희열의 스케치북] 20191213 - YouTube - Chrome");
            return FindWindow(null, WinHandleName);
        }

        public void GetWindowPos(IntPtr hwnd, ref Point point, ref Size size)
        {
            WINDOWPLACEMENT placement = new WINDOWPLACEMENT();
            placement.length = System.Runtime.InteropServices.Marshal.SizeOf(placement);

            GetWindowPlacement(hwnd, ref placement);

            size = new Size(placement.rcNormalPosition.Right - (placement.rcNormalPosition.Left * 2), placement.rcNormalPosition.Bottom - (placement.rcNormalPosition.Top * 2));
            point = new Point(placement.rcNormalPosition.Left, placement.rcNormalPosition.Top);
        }

        private void InitWinHandleName()
        {
            string WinHandleInfoPath = @"../Config\WinHandleInfoFile.txt";
            WinHandleName = System.IO.File.ReadAllText(WinHandleInfoPath);
        }
    }
}
