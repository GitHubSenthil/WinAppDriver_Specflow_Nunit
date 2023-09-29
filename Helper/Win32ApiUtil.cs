using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace TeamsWindowsApp.Helper
{
    /// <summary>
    /// The Platform invoke methods from user32.dll, for example   
    /// </summary>
    public partial class Win32ApiUtil
    {
        #region Windows
        public delegate bool EnumWindowsProc(IntPtr hwnd, IntPtr lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool EnumWindows(EnumWindowsProc lpEnumFunc, IntPtr lParam);

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        public static extern uint GetWindowThreadProcessId(IntPtr hWnd, out int lpdwProcessId);


        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int GetWindowTextLength(IntPtr hWnd);


        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder strText, int maxCount);


        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetForegroundWindow(IntPtr hWnd);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", CharSet = CharSet.Auto, ExactSpelling = true)]
        public static extern IntPtr GetDesktopWindow();

        [StructLayout(LayoutKind.Sequential)]
        private struct Rect
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        [DllImport("user32.dll")]
        private static extern IntPtr GetWindowRect(IntPtr hWnd, ref Rect rect);

        public static Image CaptureDesktop()
        {
            return CaptureWindow(GetDesktopWindow());
        }

        public static Bitmap CaptureActiveWindow()
        {
            return CaptureWindow(GetForegroundWindow());
        }

        public static Bitmap CaptureWindow(IntPtr handle)
        {
            var rect = new Rect();
            GetWindowRect(handle, ref rect);
            var bounds = new Rectangle(rect.Left, rect.Top, rect.Right - rect.Left, rect.Bottom - rect.Top);
            var result = new Bitmap(bounds.Width, bounds.Height);

            using (var graphics = Graphics.FromImage(result))
            {
                graphics.CopyFromScreen(new Point(bounds.Left, bounds.Top), Point.Empty, bounds.Size);
            }

            return result;
        }

        #endregion Windows

        #region Message
        public static class SendMessageFlags
        {
            public const int WM_CLOSE = 0x0010;
            public const int WM_QUIT = 0x12;
            public const int WM_PASTE = 0x0302;
            public const int WM_INPUTLANGCHANGEREQUEST = 0x0050;
            public const int HWND_BROADCAST = 0xffff;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool PostMessage(IntPtr hhwnd, int msg, IntPtr wparam, IntPtr lparam);

        #endregion Message

        #region KeyboardLayout

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode,
            EntryPoint = "LoadKeyboardLayout", SetLastError = true, ThrowOnUnmappableChar = false)]
        public static extern IntPtr LoadKeyboardLayout(string pwszKLID, uint flags);

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode,
            EntryPoint = "GetKeyboardLayout", SetLastError = true, ThrowOnUnmappableChar = false)]
        public static extern uint GetKeyboardLayout(uint idThread);

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall, CharSet = CharSet.Unicode,
            EntryPoint = "ActivateKeyboardLayout", SetLastError = true, ThrowOnUnmappableChar = false)]
        public static extern uint ActivateKeyboardLayout(uint hkl, uint Flags);

        public static class KeyboardLayoutFlags
        {            
            public const uint KLF_ACTIVATE = 0x00000001;
            public const uint KLF_SETFORPROCESS = 0x00000100;
        }
        #endregion KeyboardLayout

        #region ShowWindow


        /**
         * Refer to
         * https://docs.microsoft.com/en-us/windows/win32/api/winuser/nf-winuser-showwindow
         */
        [DllImport("user32.dll")]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);

        public static class ShowWindowFlags
        {
            public const int SW_HIDE = 0;
            public const int SW_SHOWNORMAL = 1;
            public const int SW_SHOWMINIMIZED = 2;
            public const int SW_SHOWMAXIMIZED = 3;
            public const int SW_SHOWNOACTIVATE = 4;
            public const int SW_SHOW = 5;
            public const int SW_MINIMIZE = 6;
            public const int SW_SHOWMINNOACTIVE = 7;
            public const int SW_SHOWNA = 8;
            public const int SW_RESTORE = 9;
            public const int SW_SHOWDEFAULT = 10;
            public const int SW_FORCEMINIMIZE = 11;
        }
        #endregion ShowWindow
    }
}
