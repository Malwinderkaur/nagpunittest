using BrokerApp.Shared;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerApp.Test
{
    public class FixedUtility : IUtility
    {
        private readonly bool _isWithinLimit;
        public FixedUtility(bool isWithinLimit)
        {
            _isWithinLimit = isWithinLimit;
        }
        public bool CheckIfWithinTimeLimits()
        {
            return _isWithinLimit;
        }
    }
}
