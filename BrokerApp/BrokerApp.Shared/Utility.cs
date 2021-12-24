using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerApp.Shared
{
    public class Utility : IUtility
    {
        public bool CheckIfWithinTimeLimits()
        {
            return TimeChecker.CheckIfWithinTimeLimits();
        }
    }
}
