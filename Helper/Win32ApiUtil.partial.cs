using System;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;
using System.Globalization;

namespace TeamsWindowsApp.Helper
{

    /// <summary>
    /// Put the wrapper class of Win32 API here     
    /// </summary>
    public partial class Win32ApiUtil
    {
        public static void CloseWindow(IntPtr hWnd)
        {
            SendMessage(hWnd, SendMessageFlags.WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="hWnd">Window handler</param>
        public static void KillProcess(IntPtr hWnd)
        {
            int processId;
            GetWindowThreadProcessId(hWnd, out processId);
            Process p = Process.GetProcessById(processId);
            p.Kill();
        }

        public static void Paste(IntPtr hWnd)
        {
            SendMessage(hWnd, SendMessageFlags.WM_PASTE, IntPtr.Zero, IntPtr.Zero);
        }
    }
}
