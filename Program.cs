using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace LazyAss
{
    class Program
    {
        [DllImport("User32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowWindow([In] IntPtr hWnd, [In] int nCmdShow);
        [DllImport("user32.dll")]
        private static extern bool SetCursorPos(int X, int Y);
        [DllImport("user32.dll")]
        private static extern bool GetCursorPos(out Point lpPoint);
        static async Task Main(string[] args)
        {
            IntPtr handle = Process.GetCurrentProcess().MainWindowHandle;
            ShowWindow(handle, 6);
            while (true)
            {
                var delayTask = Task.Delay(60000);
                Console.WriteLine(DateTime.Now);
                MakeMove();
                await delayTask;
            }
        }

        private static void MakeMove()
        {
            Random random = new Random();
            int randomNumber = random.Next(-10, 10);
            GetCursorPos(out Point lpPoint);
            SetCursorPos(lpPoint.X + randomNumber, lpPoint.Y + randomNumber);
        }
    }
}
