using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Microsoft.Win32;

// ...



namespace CustomShellTesting.ShellUtilities
{
    public class ShellManager
    {

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string className, string windowName);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);

        private const uint WM_SYSCOMMAND = 0x0112;
        private const uint SC_TASKLIST = 0xf130;
        private const uint SC_CLOSE = 0xF060;
        private const uint SC_TASKVIEW = 0x002C;
        private const uint SC_ACTIONCENTER = 0x7F;
        private const uint SC_NOTIFICATION_CENTER = 0x00C8;
        //Taskbar
        private const int WM_GETTEXTLENGTH = 0x000E;
        private const int WM_GETTEXT = 0x000D;
        private const int TB_BUTTONCOUNT = 0x0418;
        private const int TB_GETBUTTON = 0x0417;
        private const int TB_GETBUTTONINFO = 0x0441;
        private const int TBIF_IMAGE = 0x00000001;
        private const int TBIF_TEXT = 0x00000002;

        public static bool OpenStartMenu()
        {
            IntPtr hWnd = FindWindow("Shell_TrayWnd", null);
            SendMessage(hWnd, WM_SYSCOMMAND, (IntPtr)SC_TASKLIST, IntPtr.Zero);
            return IsStartMenuOpen();
        }

        public static bool CloseStartMenu()
        {
            IntPtr hWnd = FindWindow("Shell_TrayWnd", null);
            SendMessage(hWnd, WM_SYSCOMMAND, (IntPtr)SC_CLOSE, IntPtr.Zero);
            return !IsStartMenuOpen();
        }

        public static bool IsStartMenuOpen()
        {
            IntPtr hWnd = FindWindow("Windows.UI.Core.CoreWindow", null);
            return hWnd != IntPtr.Zero && IsWindowVisible(hWnd);
        }

        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        // Taskbar stuff

        [DllImport("user32.dll")]
        private static extern int ShowWindow(int hwnd, int command);

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 1;

        public static void HideTaskbar()
        {
            int hwndTaskbar = (int)FindWindow("Shell_TrayWnd", "");
            ShowWindow(hwndTaskbar, SW_HIDE);
        }

        public static void ShowTaskbar()
        {
            int hwndTaskbar = (int)FindWindow("Shell_TrayWnd", "");
            ShowWindow(hwndTaskbar, SW_SHOW);
        }

        // Custom taskbar stuff


        // Program manager stuff


        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        private const uint SWP_HIDEWINDOW = 0x0080;
        private const uint SWP_SHOWWINDOW = 0x0040;
        private static readonly IntPtr HWND_DESKTOP = IntPtr.Zero;

        public static void HideProgramManager()
        {
            IntPtr hwnd = FindWindow("Progman", "Program Manager");
            SetWindowPos(hwnd, HWND_DESKTOP, 0, 0, 0, 0, SWP_HIDEWINDOW);
        }

        public static void ShowProgramManager()
        {
            IntPtr hwnd = FindWindow("Progman", "Program Manager");
            SetWindowPos(hwnd, HWND_DESKTOP, 0, 0, 0, 0, SWP_SHOWWINDOW);
        }

        // Program manager stuff - part 2 :P

    }
}
