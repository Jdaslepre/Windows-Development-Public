using System;
using System.Runtime.InteropServices;

// ...



namespace ShellApp.ShellUtilities
{
    public class ShellManager
    {

        [DllImport("user32.dll")]
        private static extern IntPtr FindWindow(string className, string windowName);

        [DllImport("user32.dll")]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint msg, IntPtr wParam, IntPtr lParam);
        [DllImport("shell32.dll", SetLastError = true)]
        private static extern bool ShellExecute(IntPtr hwnd, string lpOperation, string lpFile, string lpParameters, string lpDirectory, int nShowCmd);


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


        public static void OpenTaskView()
        {
            // Replace this eventually

        }



        [DllImport("user32.dll")]
        private static extern bool IsWindowVisible(IntPtr hWnd);

        // Taskbar stuff

        [DllImport("user32.dll")]
        private static extern int ShowWindow(int hwnd, int command);

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 5;


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









        private const int SWP_SHOWWINDOW = 0x0040;
        private const int SWP_HIDEWINDOW = 0x0080;
        private const int GWL_STYLE = -16;
        private const int WS_VISIBLE = 0x10000000;
        private const int WS_BORDER = 0x00800000;
        private const int WS_CAPTION = 0x00C00000;
        private const int WS_SYSMENU = 0x00080000;
        private const int WS_MINIMIZEBOX = 0x00020000;
        private const int WS_MAXIMIZEBOX = 0x00010000;
        private const int WS_POPUP = 0x8000000;//0;

        private static readonly IntPtr HWND_BOTTOM = new IntPtr(1);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);


        public static void EnableAutohide()
        {
            var handle = FindWindow("Shell_TrayWnd", null);

            SetWindowLong(handle, GWL_STYLE, WS_VISIBLE | WS_POPUP);

            SetWindowPos(handle, HWND_BOTTOM, 0, 0, 0, 0, SWP_HIDEWINDOW);
        }

        public static void DisableAutohide()
        {
            var handle = FindWindow("Shell_TrayWnd", null);

            SetWindowLong(handle, GWL_STYLE, WS_VISIBLE | WS_BORDER | WS_CAPTION | WS_SYSMENU | WS_MINIMIZEBOX | WS_MAXIMIZEBOX);

            SetWindowPos(handle, HWND_BOTTOM, 0, 0, 0, 0, SWP_SHOWWINDOW);
        }







        // Program manager stuff


        [DllImport("user32.dll")]
        private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int x, int y, int cx, int cy, uint uFlags);

        //private const uint SWP_HIDEWINDOW = 0x0080;
        //private const uint SWP_SHOWWINDOW = 0x0040;
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