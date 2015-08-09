using System;
using System.Threading;

namespace ICFPC2015.ConsoleRunner
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
        private readonly Timer stopExecutionTimer;

        private TimeLimiter(TimeSpan limit)
        {
            stopRequested = false;
            if (limit > TimeSpan.FromTicks(0))
            {
                var allowedExecutionTime = limit - TimeSpan.FromMilliseconds(200);
                stopExecutionTimer = new Timer(SendStopSignal, null, allowedExecutionTime, TimeSpan.FromMilliseconds(-1));
            }
        }

        private void SendStopSignal(object smth)
        {
            stopRequested = true;
        }

        public bool IsStopRequested()
        {
            return stopRequested;
        }
    }
}
