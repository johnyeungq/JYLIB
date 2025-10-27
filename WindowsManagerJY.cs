using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace JYLIB
{
    internal class WindowsManagerJY


    {
   
            // Import COM interface for Shell
            [ComImport]
            [Guid("13709620-C279-11CE-A49E-444553540000")]
            private class ShellDispatch { }

            [ComImport]
            [Guid("00000000-0000-0000-C000-000000000046")]
            [InterfaceType(ComInterfaceType.InterfaceIsIDispatch)]
            internal interface IShellDispatch
            {
                void MinimizeAll();
                void UndoMinimizeAll();
            }
            internal void MinimizeAllWindows()
            {

                ToggleDesktop();


            }
            internal void ToggleDesktop()
            {
                IShellDispatch shell = (IShellDispatch)new ShellDispatch();

                // Windows + D toggles between minimize all and restore
                // We can use MinimizeAll followed by checking state, but simpler to just use ToggleMinimizeAll
                shell.MinimizeAll();  // This mimics the first Windows + D press

                // Note: To fully replicate toggle behavior, you'd typically check state first,
                // but MinimizeAll alone mimics the basic Windows + D functionality
                // For full toggle, see alternative below
            }

            // Alternative with true toggle behavior
            [DllImport("user32.dll")]
            private static extern IntPtr GetForegroundWindow();

            [DllImport("user32.dll")]
            private static extern bool IsIconic(IntPtr hWnd);  // Checks if window is minimized

            internal void ToggleDesktopFull()
            {
                IShellDispatch shell = (IShellDispatch)new ShellDispatch();
                IntPtr foregroundWindow = GetForegroundWindow();

                // Check if current foreground window is minimized
                if (IsIconic(foregroundWindow))
                {
                    // If minimized, restore windows
                    shell.UndoMinimizeAll();
                }
                else
                {
                    // If not minimized, show desktop
                    shell.MinimizeAll();
                }
            }

            [DllImport("user32.dll")]
            private static extern bool SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter,
                int X, int Y, int cx, int cy, uint uFlags);

            [DllImport("user32.dll")]
            private static extern bool ShowWindow(IntPtr hWnd, int nCmdShow);

            private static readonly IntPtr HWND_TOPMOST = new IntPtr(-1);
            private const uint SWP_NOMOVE = 0x0002;
            private const uint SWP_NOSIZE = 0x0001;
            private const uint SWP_SHOWWINDOW = 0x0040;
            private const int SW_MAXIMIZE = 3;


            internal async  Task MakeProcessWindowTopMostAndMax(string processName)
            {
                const int maxRetries = 10;
                const int delayMilliseconds = 2000;

                for (int attempt = 1; attempt <= maxRetries; attempt++)
                {
                    Process[] processes = Process.GetProcessesByName(processName);
                    if (processes.Length > 0)
                    {
                        IntPtr hWnd = processes[0].MainWindowHandle;
                        if (hWnd != IntPtr.Zero)
                        {
                            // Maximize the window
                            ShowWindow(hWnd, SW_MAXIMIZE);

                            // Set window as topmost
                            SetWindowPos(hWnd, HWND_TOPMOST, 0, 0, 0, 0,
                                SWP_NOMOVE | SWP_NOSIZE | SWP_SHOWWINDOW);

                            Console.WriteLine($"Window of '{processName}' set to topmost and maximized.");
                            return;
                        }
                    }

                    Console.WriteLine($"Attempt {attempt}: Process '{processName}' not ready. Retrying in 2 seconds...");
                    await Task.Delay(delayMilliseconds);
                }

                Console.WriteLine($"Failed to set window of '{processName}' after {maxRetries} attempts.");
            }

        

    }
}
