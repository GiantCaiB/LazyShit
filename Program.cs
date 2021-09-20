using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Quartz;

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
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);
        //Mouse actions
        private const int MouseEventLeftDown = 0x02;
        private const int MouseEventLeftUp = 0x04;
        private const int MouseEventRightDown = 0x08;
        private const int MouseEventRightUp = 0x10;

        static async Task Main(string[] args)
        {
            var handle = Process.GetCurrentProcess().MainWindowHandle;
            ShowWindow(handle, 6);
            while (true)
            {
                var delayTask = Task.Delay(60000);
                if (IsWorkTime(DateTime.Now))
                {
                    Console.WriteLine($"{DateTime.Now}: {MakeMove()}");
                    DoMouseClick();
                }
                await delayTask;
            }
        }

        private static int MakeMove()
        {
            int randomNumber = GetRandomInteger();
            GetCursorPos(out var lpPoint);
            SetCursorPos(lpPoint.X + randomNumber, lpPoint.Y + randomNumber);
            return randomNumber;
        }

        private static void DoMouseClick()
        {
            GetCursorPos(out var lpPoint);
            var x = (uint)lpPoint.X;
            var y = (uint)lpPoint.Y;
            mouse_event(MouseEventRightDown | MouseEventRightUp, x , y, 0, 0);
        }

        private static int GetRandomInteger()
        {
            var random = new Random();
            return random.Next(-50, 50);
        }

        private static bool IsWorkTime(DateTime dateTime)
        {
            var workTimeCronExpression = new CronExpression("* * 9,10,11,13,14,15,16,17 ? * MON,TUE,WED,THU,FRI *");
            return workTimeCronExpression.IsSatisfiedBy(dateTime);
        }
    }
}
