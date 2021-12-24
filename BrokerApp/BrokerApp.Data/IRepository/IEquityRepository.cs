using BrokerApp.Data.DatabaseModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BrokerApp.Data.IRepository
{
    public interface IEquityRepository
    {
        Equity GetEquityById(int equityId);
        void UpdateEquity(Equity equity);
    }
}
