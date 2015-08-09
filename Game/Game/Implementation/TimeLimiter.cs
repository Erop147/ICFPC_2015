using System;
using System.Diagnostics;
using System.Threading;

namespace ICFPC2015.GameLogic.Implementation
{
    public class TimeLimiter
    {
        private static TimeLimiter Instance { get; set; }

        public static void Start(TimeSpan limit)
        {
            Instance = new TimeLimiter(limit);
        }
        public static bool NeedStop()
        {
            return Instance.IsStopRequested();
        }

        private bool stopRequested;
        private TimeSpan allowedExecutionTime;
        private readonly Thread checkerThread;
        private readonly Stopwatch checkerStopwatch;

        private TimeLimiter(TimeSpan limit)
        {
            stopRequested = false;
            if (limit > TimeSpan.FromTicks(0))
            {
                allowedExecutionTime = limit - TimeSpan.FromMilliseconds(300);
                checkerStopwatch = Stopwatch.StartNew();
                checkerThread = new Thread(SendStopSignal);
                checkerThread.Start(allowedExecutionTime);
            }
        }

        private void SendStopSignal(object allowedExecutionTimeObj)
        {
            var allowedExecutionTime = (TimeSpan) allowedExecutionTimeObj;
            while (checkerStopwatch.ElapsedMilliseconds < allowedExecutionTime.TotalMilliseconds)
            {
                Thread.Sleep(100);
            }
            stopRequested = true;
            checkerStopwatch.Stop();
        }

        public bool IsStopRequested()
        {
            return stopRequested;
        }
    }
}
