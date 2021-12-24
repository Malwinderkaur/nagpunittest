using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerApp.Shared
{
    public class TimeChecker
    {
        public static bool CheckIfWithinTimeLimits()
        {
            var currentTime = DateTime.Now;
            if (currentTime.Hour < 9 && currentTime.Hour > 15)
            {
                return false;
            }
            if (currentTime.DayOfWeek == DayOfWeek.Sunday || currentTime.DayOfWeek == DayOfWeek.Saturday)
            {
                return false;
            }
            return true;
        }
    }
}
