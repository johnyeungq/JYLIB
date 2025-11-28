using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JYLIB;

using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;


namespace JYLIB
{
    internal class input
    {
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        private static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint dwData, IntPtr dwExtraInfo);

        [DllImport("user32.dll")]
        private static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, UIntPtr dwExtraInfo);
        private const uint MOUSEEVENTF_LEFTDOWN = 0x0002;
        private const uint MOUSEEVENTF_LEFTUP = 0x0004;
        private const uint MOUSEEVENTF_RIGHTDOWN = 0x0008;
        private const uint MOUSEEVENTF_RIGHTUP = 0x0010;
        private const uint MOUSEEVENTF_WHEEL = 0x0800;

        private const int KEYEVENTF_KEYDOWN = 0x0000;
        private const int KEYEVENTF_KEYUP = 0x0002;

        internal static async Task ClickAtScreenAsync(int x, int y, int delayMs = 100)
        {
            SetCursorPos(x, y);
            mouse_event(MOUSEEVENTF_LEFTDOWN, (uint)x, (uint)y, 0, IntPtr.Zero);
            await Task.Delay(delayMs);
            mouse_event(MOUSEEVENTF_LEFTUP, (uint)x, (uint)y, 0, IntPtr.Zero);
        }
        internal static async Task RightClickAtScreenAsync(int x, int y, int delayMs = 100)
        {
            SetCursorPos(x, y);
            mouse_event(MOUSEEVENTF_RIGHTDOWN, (uint)x, (uint)y, 0, IntPtr.Zero);
            await Task.Delay(delayMs);
            mouse_event(MOUSEEVENTF_RIGHTUP, (uint)x, (uint)y, 0, IntPtr.Zero);
        }

        internal static void Scroll(int amount)
        {
            mouse_event(MOUSEEVENTF_WHEEL, 0, 0, (uint)amount, IntPtr.Zero);
        }
        internal static void PressKey(byte virtualKey)
        {
            keybd_event(virtualKey, 0, KEYEVENTF_KEYDOWN, UIntPtr.Zero);
            keybd_event(virtualKey, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
        }
        internal static void PressKeyCombo(byte modifierKey, byte key)
        {
            keybd_event(modifierKey, 0, KEYEVENTF_KEYDOWN, UIntPtr.Zero);
            keybd_event(key, 0, KEYEVENTF_KEYDOWN, UIntPtr.Zero);
            keybd_event(key, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
            keybd_event(modifierKey, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
        }
        internal static void HoldKey(byte virtualKey, int holdDurationMs)
        {
            keybd_event(virtualKey, 0, KEYEVENTF_KEYDOWN, UIntPtr.Zero);
            Task.Delay(holdDurationMs).Wait();
            keybd_event(virtualKey, 0, KEYEVENTF_KEYUP, UIntPtr.Zero);
        }
        
    }
}
